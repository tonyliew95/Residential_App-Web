using Nop.Core;
using Nop.Core.Data;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Msp.Member;
using System;
using System.Collections.Generic;
using System.Linq;
using Nop.Core.Domain.Msp.Custom;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Domain.Msp.Wallet;
using Nop.Services.Helpers;

namespace Nop.Services.Msp.MemberTree
{
    public partial class MemberTreeServices : IMemberTreeServices
    {
        private readonly IRepository<MSP_MemberTree> _memberTreeRepository;
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<MSP_Wallet> _walletRepository;

        private readonly IMspHelper _mspHelper;

        public IMspHelper MspHelper => _mspHelper;

        public MemberTreeServices(IRepository<MSP_MemberTree> Msp_MemberTreeRepository
            , IRepository<Customer> CustomerRepository
            , IRepository<MSP_Wallet> walletRepository
            , IMspHelper MspHelper)
        {
            this._memberTreeRepository = Msp_MemberTreeRepository;
            this._customerRepository = CustomerRepository;
            this._walletRepository = walletRepository;
            this._mspHelper = MspHelper;
        }

        #region MDT-139:Backend Admin Panel > Member tree hierarchy screen
        public IPagedList<MSP_MemberTree> GetMemberTreeList(string Username, string GlobalGuid, string IntroducerGuid
            //, DateTime? DateFrom, DateTime? DateTo //RW 20181227 MSP-608
            , int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var CustomerId = _customerRepository.Table.Where(o => o.Username == Username).Select(o => o.Id).FirstOrDefault();

            var MSP_MemberTree_Repo = _memberTreeRepository.Table.Where(o => o.CustomerID > 0).AsQueryable(); //RW 20181227 MSP-612

            #region filter
            if (!string.IsNullOrEmpty(Username)) //RW MSP-189 20181002
            {
                MSP_MemberTree_Repo = MSP_MemberTree_Repo.Where(o => o.CustomerID == CustomerId);
            }

            if (!string.IsNullOrEmpty(GlobalGuid))
            {
                MSP_MemberTree_Repo = MSP_MemberTree_Repo.Where(o => o.GlobalGUID == GlobalGuid);
            }

            if (!string.IsNullOrEmpty(IntroducerGuid))
            {
                MSP_MemberTree_Repo = MSP_MemberTree_Repo.Where(o => o.IntroducerGlobalGUID == IntroducerGuid);
            }

            #region unfilter date //RW 20181227 MSP-608
            //if (DateFrom.HasValue)
            //{
            //    MSP_MemberTree_Repo = MSP_MemberTree_Repo.Where(o => o.CreatedOnUtc >= DateFrom.Value);
            //}

            //if (DateTo.HasValue)
            //{
            //    MSP_MemberTree_Repo = MSP_MemberTree_Repo.Where(o => o.CreatedOnUtc <= DateTo.Value);
            //}
            #endregion

            MSP_MemberTree_Repo = MSP_MemberTree_Repo.OrderByDescending(o => o.CreatedOnUtc);

            #endregion

            var list = new PagedList<MSP_MemberTree>(MSP_MemberTree_Repo, pageIndex, pageSize);
            return list;
        }
        #endregion

        #region GetCustomerMemberTree
        public IList<CustomerMemberTreeCustom> GetCustomerMemberTreeList(int customerId, int level)
        {
            string searchKeyword = "," + customerId + ",";

            List<CustomerMemberTreeCustom> list = new List<CustomerMemberTreeCustom>();

            if (level > 0)
            {
                list = (from member in _memberTreeRepository.Table
                        join customer in _customerRepository.Table
                        on member.CustomerID equals customer.Id
                        into m
                        from subcust in m.DefaultIfEmpty()
                        where ("," + member.RecommendIDs + ",").Contains(searchKeyword)
                        && member.Level == level
                        select new CustomerMemberTreeCustom
                        {
                            Username = subcust.Username,
                            CustomerID = member.CustomerID,
                            ParentID = member.ParentID
                        }).ToList();
            }
            else
            {
                list = (from member in _memberTreeRepository.Table
                        join customer in _customerRepository.Table
                        on member.CustomerID equals customer.Id
                        into m
                        from subcust in m.DefaultIfEmpty()
                        where ("," + member.RecommendIDs + ",").Contains(searchKeyword)
                        select new CustomerMemberTreeCustom
                        {
                            Username = subcust.Username,
                            CustomerID = member.CustomerID,
                            ParentID = member.ParentID
                        }).ToList();

                var querySelf = (from customer in _customerRepository.Table
                                 join member in _memberTreeRepository.Table
                                 on customer.Id equals member.CustomerID
                                 join wallet in _walletRepository.Table
                                 on member.CustomerID equals wallet.CustomerID
                                 where member.CustomerID == customerId
                                 select new CustomerMemberTreeCustom
                                 {
                                     Username = customer.Username,
                                     CustomerID = member.CustomerID,
                                     ParentID = member.ParentID,
                                 }).FirstOrDefault();

                list.Add(new CustomerMemberTreeCustom
                {
                    Username = querySelf.Username,
                    CustomerID = customerId,
                    ParentID = null,
                });
            }

            list.OrderBy(o => o.CustomerID).ThenBy(o => o.ParentID);

            return list;
        }

        #endregion

        public CustomerMemberTreeDetailsCustom GetCustomerMemberTreeDetails(int customerId)
        {
            string searchKeyword = "," + customerId + ",";
            //int memberCount = 0;

            var list =
            (from customer in _customerRepository.Table
             join member in _memberTreeRepository.Table
             on customer.Id equals member.CustomerID
             join wallet in _walletRepository.Table
             on member.CustomerID equals wallet.CustomerID
             where member.CustomerID == customerId
             select new CustomerMemberTreeDetailsCustom
             {
                 Username = customer.Username,
                 DepositWalletAddress = member.DepositWalletAddress.ToString(),
                 Score_Y = wallet.Score_Y.ToString(),
                 ScorePct_Y = wallet.ScorePct_Y.ToString(),
                 Score_Z = wallet.Score_Z.ToString(),
                 ScorePct_Z = wallet.ScorePct_Z.ToString(),
                 MemberQuantity = (from member1 in _memberTreeRepository.Table where ("," + member1.RecommendIDs + ",").Contains("," + member.CustomerID + ",") select new { member1.Id }).Count(),
                 Contribution = wallet.Profit_Total.ToString(),
                 UserGuid = member.GlobalGUID.ToString(),

                 AvailableBalance = wallet.Mbtc.ToString(),
                 LockedEarningWalletBalance = wallet.Profit_DP_Float.ToString(),
                 AgencyFeeAmount = wallet.Deposit_Total.ToString(),
                 AgencyFeeReturned = wallet.Deposit_Return_Total.ToString(),
                 AgencyFeeReward = wallet.Profit_DP.ToString(),
                 AgentReward = (wallet.Profit_CP + wallet.Profit_DP).ToString(),
                 TaskReward = wallet.Guaranteed_Total.ToString(),
                 MerchantReferralReward = wallet.MerchantReferral_Total.ToString(),
                 AgencyFeeRewardTask = (wallet.Profit_CP_Self + wallet.Profit_CP).ToString()//Tony Liew 20190102 MSP-635 

             }).FirstOrDefault();

            decimal score_YPct = Convert.ToDecimal(list.ScorePct_Y);
            list.ScorePct_Y = _mspHelper.TruncateDecimal_Pct(score_YPct);

            decimal score_ZPct = Convert.ToDecimal(list.ScorePct_Z);
            list.ScorePct_Z = _mspHelper.TruncateDecimal_Pct(score_ZPct);

            decimal score_Y = Convert.ToDecimal(list.Score_Y);
            list.Score_Y = _mspHelper.TruncateBEDecimal_Score(score_Y);

            decimal score_Z = Convert.ToDecimal(list.Score_Z);
            list.Score_Z = _mspHelper.TruncateBEDecimal_Score(score_Z);

            decimal contribution = Convert.ToDecimal(list.Contribution);
            list.Contribution = _mspHelper.TruncateBEDecimal_Score(contribution);

            decimal availableBal = Convert.ToDecimal(list.AvailableBalance);
            list.AvailableBalance = _mspHelper.TruncateBEDecimal_Score(availableBal);

            decimal lockedearningwalletBal = Convert.ToDecimal(list.LockedEarningWalletBalance);
            list.LockedEarningWalletBalance = _mspHelper.TruncateBEDecimal_Score(lockedearningwalletBal);

            decimal agencyFeeAmt = Convert.ToDecimal(list.AgencyFeeAmount);
            list.AgencyFeeAmount = _mspHelper.TruncateBEDecimal_Score(agencyFeeAmt);

            decimal agencyFeeReturned = Convert.ToDecimal(list.AgencyFeeReturned);
            list.AgencyFeeReturned = _mspHelper.TruncateBEDecimal_Score(agencyFeeReturned);

            decimal agencyFeeReward = Convert.ToDecimal(list.AgencyFeeReward);
            list.AgencyFeeReward = _mspHelper.TruncateBEDecimal_Score(agencyFeeReward);

            decimal agentReward = Convert.ToDecimal(list.AgentReward);
            list.AgentReward = _mspHelper.TruncateBEDecimal_Score(agentReward);

            decimal taskReward = Convert.ToDecimal(list.TaskReward);
            list.TaskReward = _mspHelper.TruncateBEDecimal_Score(taskReward);

            decimal merchatntReferralReward = Convert.ToDecimal(list.MerchantReferralReward);
            list.MerchantReferralReward = _mspHelper.TruncateBEDecimal_Score(merchatntReferralReward);

            decimal agencyFeeRewardTask = Convert.ToDecimal(list.AgencyFeeRewardTask);//Tony Liew 20190102 MSP-635
            list.AgencyFeeRewardTask = _mspHelper.TruncateBEDecimal_Score(agencyFeeRewardTask);//Tony Liew 20190102 MSP-635

            return list;
        }

        #region comment
        //public MyNetworkDto GetMyNetworkByCustomerId(int customerId, MyNetwork_FilterParam param) //Jerry 20180802 MSP-11
        //{
        //    string searchKeyword = "," + customerId + ",";
        //    int memberCount = 0;

        //    #region Comment Code
        //    //Atiqah 20181019 MSP-360 \/

        //    //var memberCount =
        //    //    (from member in _memberTreeRepository.Table
        //    //     where ("," + member.RecommendIDs + ",").Contains(searchKeyword)
        //    //     select new
        //    //     {
        //    //         member.Id
        //    //     }).Count();


        //    //MyNetworkDto dto =
        //    //    (from customer in _customerRepository.Table
        //    //     join member in _memberTreeRepository.Table
        //    //     on customer.Id equals member.CustomerID
        //    //     join wallet in _walletRepository.Table
        //    //     on member.CustomerID equals wallet.CustomerID
        //    //     where member.CustomerID == customerId
        //    //     select new MyNetworkDto()
        //    //     {
        //    //         Username = customer.Username,
        //    //         DepositWalletAddress = member.DepositWalletAddress.ToString(),
        //    //         Score_Y = wallet.Score_Y.ToString(),
        //    //         ScorePct_Y = Math.Floor(wallet.ScorePct_Y * 100).ToString(),
        //    //         Score_Z = wallet.Score_Z.ToString(),
        //    //         ScorePct_Z = Math.Floor(wallet.ScorePct_Z * 100).ToString(),
        //    //         MemberQuantity = memberCount,
        //    //         Contribution = wallet.Profit_Total.ToString(),
        //    //         UserGuid = member.GlobalGUID.ToString(), //Atiqah 20181018 MSP-360
        //    //     }).FirstOrDefault();
        //    //Atiqah 20181019 MSP-360 /\
        //    #endregion Comment Code

        //    //Atiqah 20181019 MSP-360 \/
        //    MyNetworkDto dto =
        //                    (from customer in _customerRepository.Table
        //                     join member in _memberTreeRepository.Table
        //                     on customer.Id equals member.CustomerID
        //                     join wallet in _walletRepository.Table
        //                     on member.CustomerID equals wallet.CustomerID
        //                     where ("," + member.RecommendIDs + ",").Contains(searchKeyword) && (customer.Username == param.Username || member.GlobalGUID == param.UserGuid)
        //                     select new MyNetworkDto()
        //                     {
        //                         Username = customer.Username,
        //                         DepositWalletAddress = member.DepositWalletAddress.ToString(),
        //                         Score_Y = wallet.Score_Y.ToString(),
        //                         ScorePct_Y = Math.Floor(wallet.ScorePct_Y * 100).ToString(),
        //                         Score_Z = wallet.Score_Z.ToString(),
        //                         ScorePct_Z = Math.Floor(wallet.ScorePct_Z * 100).ToString(),
        //                         MemberQuantity = memberCount,
        //                         Contribution = wallet.Profit_Total.ToString(),
        //                         UserGuid = member.GlobalGUID.ToString(),
        //                     }).FirstOrDefault();

        //    MyNetworkDto list = new MyNetworkDto();
        //    list.MemberQuantity = 0;

        //    if (dto != null)
        //    {
        //        Customer cust = _customerService.GetCustomerByUsername(dto.Username);
        //        string searchKeywordDownline = "," + cust.Id + ",";
        //        var memberQuantity =
        //            (from member in _memberTreeRepository.Table
        //             where ("," + member.RecommendIDs + ",").Contains(searchKeywordDownline)
        //             select new
        //             {
        //                 member.Id
        //             }).Count();
        //        dto.MemberQuantity = memberQuantity;
        //    }

        //    if (dto == null) return list;
        //    //Atiqah 20181019 MSP-360 /\

        //    decimal score_Y = Convert.ToDecimal(dto.Score_Y);
        //    dto.Score_Y = _utilityHelper.TruncateDecimalToString_MBTC(score_Y);

        //    decimal score_Z = Convert.ToDecimal(dto.Score_Z);
        //    dto.Score_Z = _utilityHelper.TruncateDecimalToString_MBTC(score_Z);

        //    decimal contribution = Convert.ToDecimal(dto.Contribution);
        //    dto.Contribution = _utilityHelper.TruncateDecimalToString_MBTC(contribution);

        //    return dto;
        //}
        #endregion
    }
}
