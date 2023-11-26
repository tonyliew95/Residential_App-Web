using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Core;
using Nop.Core.Data;
using Nop.Core.Domain.Msp.Security;
using Nop.Core.Enumeration;
using Nop.Services.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Security
{
    public partial class SecurityQuestionManagementServices : ISecurityQuestionManagementService
    {
        //private readonly IRepository<MSP_SecurityQuestion> _securityQuestionRepository; //Atiqah 20190131 MDT-205
        private readonly IRepository<MSP_SecurityQuestionCustom> _securityQuestionCustomRepository;
        private readonly IEventPublisher _eventPublisher;

        public SecurityQuestionManagementServices
            (
                //IRepository<MSP_SecurityQuestion> securityQuestionRepository //Atiqah 20190131 MDT-205
                IRepository<MSP_SecurityQuestionCustom> securityQuestionCustomRepository
               ,IEventPublisher eventPublisher
            )
            {
            //_securityQuestionRepository = securityQuestionRepository; //Atiqah 20190131 MDT-205
            _securityQuestionCustomRepository = securityQuestionCustomRepository;
                _eventPublisher = eventPublisher;
            }

        #region MSP-47 Backend Function: Security Question management 
        //Atiqah 20190131 MDT-205 \/
        //public IPagedList<MSP_SecurityQuestion> SecurityQuestionsList(string option , int pageIndex = 1, int pageSize = int.MaxValue) //Tony Liew 20180828 MSP-47
        //{
        //    var active = MSP_SecurityQuestion_Status.Active.ToValue<MSP_SecurityQuestion_Status>();
        //    var inactive = MSP_SecurityQuestion_Status.Inactive.ToValue<MSP_SecurityQuestion_Status>();
        //    var systemQuestion = _securityQuestionRepository.Table.AsQueryable().ToList();
        //    systemQuestion.Select(s => s.Question);
        //    systemQuestion.Select(s => s.Status);

        //    if (option == active)
        //        systemQuestion = systemQuestion.AsQueryable().Where(s => s.Status == active).ToList();
        //    else if (option == inactive)
        //        systemQuestion = systemQuestion.AsQueryable().Where(s => s.Status == inactive).ToList();

        //    var list = new PagedList<MSP_SecurityQuestion>(systemQuestion, pageIndex, pageSize);

        //    return list;
        //}

        //public void UpdateSecurityQuestion(MSP_SecurityQuestion question) //Tony Liew 20180828 MSP-47
        //{

        //    if (question == null)
        //        throw new ArgumentNullException(nameof(question));

        //    _securityQuestionRepository.Update(question);

        //    //event notification
        //    _eventPublisher.EntityUpdated(question);
        //}

        //public virtual MSP_SecurityQuestion GetQuestionID(int questionID) //Tony Liew 20180903 MSP-47
        //{
        //    if (questionID == 0)
        //        return null;

        //    var result = (from table in _securityQuestionRepository.Table
        //                  where table.Id == questionID
        //                  select table).FirstOrDefault();

        //    return result;
        //}


        //public void AddSecuirityQuestion(MSP_SecurityQuestion newQuestion) //Tony Liew 20180903 MSP-47
        //{
        //    if(newQuestion == null || newQuestion.Question.Length > 200)
        //        throw new ArgumentNullException(nameof(newQuestion));

        //    _securityQuestionRepository.Insert(newQuestion);
        //    //event notification
        //    _eventPublisher.EntityInserted(newQuestion);
        //}
        //Atiqah 20190131 MDT-205 /\


        #endregion
    }
}
