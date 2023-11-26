using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Core.Domain.Msp.Member;
using Nop.Services.Customers;
using Nop.Services.Helpers;
using Nop.Services.Msp.MemberTree;
using Nop.Services.Security;
using Nop.Web.Areas.Admin.Models.MemberTree;
using Nop.Web.Framework.Kendoui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Areas.Admin.Controllers
{
    public partial class MemberTreeController : BaseAdminController
    {
        private readonly IMemberTreeServices _memberTreeServices;
        private readonly IPermissionService _permissionService;
        private readonly IMspHelper _mspHelper;
        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly ICustomerService _customerService;

        public MemberTreeController
            (
            IMemberTreeServices memberTreeServices,
            IPermissionService permissionService,
            IMspHelper mspHelper,
            IDateTimeHelper dateTimeHelper,
            ICustomerService customerService
            )
        {
            this._memberTreeServices = memberTreeServices;
            this._permissionService = permissionService;
            this._mspHelper = mspHelper;
            this._dateTimeHelper = dateTimeHelper;
            this._customerService = customerService;
        }

        #region GetMemberTreeList RW 20181214 MDT-139
        [HttpGet]
        public virtual IActionResult MemberTreeList()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageMemberTree))
                return AccessDeniedView();

            MemberTreeModel model = new MemberTreeModel();

            return View(model);
        }

        [HttpPost]
        public virtual IActionResult MemberTreeList(DataSourceRequest command, MemberTreeModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageMemberTree))
                return AccessDeniedKendoGridJson();
            //var model = new TxnTopUpListViewModel();

            var gridModel = new DataSourceResult();

            //if (!model.DateFrom.HasValue || !model.DateTo.HasValue) //RW 20181227 MSP-608
            //{
            //    return Json(gridModel);
            //}

            //var dtTo = _mspHelper.ToDate(model.DateTo.Value); //RW 20181227 MSP-608

            var list = _memberTreeServices.GetMemberTreeList(
               Username: model.Username
               //, DateFrom: _dateTimeHelper.ConvertToUtcTime(model.DateFrom.Value) //RW 20181227 MSP-608
               //, DateTo: _dateTimeHelper.ConvertToUtcTime(dtTo) //RW 20181227 MSP-608
               , GlobalGuid: model.GlobalGuid
               , IntroducerGuid: model.IntroducerGlobalGuid
               , pageIndex: command.Page - 1
               , pageSize: command.PageSize
               );

            gridModel = new DataSourceResult
            {
                Data = list.Select(o => new MemberTreeModel.MemberTree
                {
                    CustomerId = o.CustomerID,
                    Username = _customerService.GetCustomerUsernameById(o.CustomerID),
                    CreatedOnUtc = o.CreatedOnUtc,
                    GlobalGuid = o.GlobalGUID.ToUpper(),
                    IntroducerGlobalGuid = o.IntroducerGlobalGUID.ToUpper(),
                }),
                Total = list.TotalCount
            };

            return Json(gridModel);
        }
        #endregion

        //#region //RW 20181221 MDT-139
        //[HttpGet]
        //public virtual IActionResult GetCustomerDownLineList(int Id, int Level)
        //{
        //    if (!_permissionService.Authorize(StandardPermissionProvider.ManageMemberTree))
        //        return AccessDeniedKendoGridJson();

        //    var model = new MemberTreeModel();

        //    var list = _memberTreeServices.GetCustomerMemberTreeList(
        //        customerId: Id
        //        ,level: Level
        //        );

        //    int? nullable = null;

        //    model.CustomerMemberTreeList = list.Select(o => new MemberTreeModel.CustomerMemberTree
        //    {
        //        CustomerId = o.CustomerID,
        //        ParentId = o.ParentID ?? nullable,
        //        Username = o.Username
        //    }).ToList();

        //    model.CustomerId = Id;

        //    return View(model);
        //}
        //#endregion

        public virtual IActionResult GetCustomerDownLineList(int Id, int Level)
        //public JsonResult LazyGet(int parentId, int Level)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageMemberTree))
                return AccessDeniedKendoGridJson();

            var model = new MemberTreeModel();

            var list = _memberTreeServices.GetCustomerMemberTreeList(
                customerId: Id
                , level: Level
                );

            //List<Location> locations;
            //List<DownlineDTO> records;
            //using (ApplicationDbContext context = new ApplicationDbContext())
            //{
            //locations = context.Locations.ToList();

            //model.DownlineMemberTreeList = list//.Where(l => l.ParentID == Id)//
            //.OrderBy(l => l.CustomerID)
            //    .Select(l => new MemberTreeModel.DownlineDTO
            //    {
            //        id = l.CustomerID,
            //        text = l.Username+": "+ l.CustomerID+" ParentID: "+ l.ParentID,
            //        //@checked = l.Checked,
            //        //population = l.Population,
            //        //flagUrl = l.FlagUrl,
            //        hasChildren = list.Any(l2 => l2.ParentID == l.CustomerID)
            //    }).ToList();

            int? nullable = null;

            model.CustomerMemberTreeList = list.Select(o => new MemberTreeModel.CustomerMemberTree
            {
                CustomerId = o.CustomerID,
                ParentId = o.ParentID ?? nullable,
                Username = o.Username
            }).ToList();

            //var CustomerMemberTreeList = list.Select(o => new MemberTreeModel.CustomerMemberTree
            //{
            //    CustomerId = o.CustomerID,
            //    ParentId = o.ParentID ?? nullable,
            //    Username = o.Username
            //}).ToList();

            //model.DownlineMemberTreeList = list.Where(l => l.ParentID == null).OrderBy(l => l.CustomerID)
            //        .Select(l => new MemberTreeModel.DownlineDTO
            //        {
            //            id = l.CustomerID,
            //            //text = l.Username + ": " + l.CustomerID + " ParentID: " + l.ParentID,
            //            text = l.Username,
            //            //@checked = l.Checked,
            //            //population = l.Population,
            //            //flagUrl = l.FlagUrl,
            //            children = GetChildren(CustomerMemberTreeList, l.CustomerID)
            //        }).ToList();
            //}

            return View(model);

            //return this.Json(records, JsonRequestBehavior.AllowGet);
        }

        private List<MemberTreeModel.DownlineDTO> GetChildren(List<MemberTreeModel.CustomerMemberTree> locations, int parentId)
        {
            return locations.Where(l => l.ParentId == parentId).OrderBy(l => l.CustomerId)
                .Select(l => new MemberTreeModel.DownlineDTO
                {
                    id = l.CustomerId,
                    //text = l.Username + ": " + l.CustomerId + " ParentID: " + l.ParentId,
                    text = l.Username,
                    //population = l.Population,
                    //flagUrl = l.FlagUrl,
                    //@checked = l.Checked,
                    children = GetChildren(locations, l.CustomerId)
                }).ToList();
        }

        #region GetCustomerDetail //RW 20181221 MDT-139
        public virtual IActionResult GetCustomerDetail(int Id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageMemberTree))
                return AccessDeniedKendoGridJson();

            var model = new MemberTreeModel();

            var details = _memberTreeServices.GetCustomerMemberTreeDetails(
                customerId: Id
                );

            var customerClass = new MemberTreeModel.CustomerDetails()
            {
                Username = details.Username,
                UserGuid = details.UserGuid,
                Score_Y = details.Score_Y,
                ScorePct_Y = details.ScorePct_Y,
                Score_Z = details.Score_Z,
                ScorePct_Z = details.ScorePct_Z,
                DepositWalletAddress = details.DepositWalletAddress,
                MemberQuantity = details.MemberQuantity,
                Contribution = details.Contribution,

                AvailableBalance = details.AvailableBalance,
                LockedEarningWalletBalance = details.LockedEarningWalletBalance,
                AgencyFeeAmount = details.AgencyFeeAmount,
                AgencyFeeReturned = details.AgencyFeeReturned,
                AgencyFeeReward = details.AgencyFeeReward,
                AgentReward = details.AgentReward,
                TaskReward = details.TaskReward,
                MerchantReferralReward = details.MerchantReferralReward,
                AgencyFeeRewardTask = details.AgencyFeeRewardTask,//Tony Liew 20190102 MSP-635 
            };

            return Json(customerClass);
        }
        #endregion
    }
}
