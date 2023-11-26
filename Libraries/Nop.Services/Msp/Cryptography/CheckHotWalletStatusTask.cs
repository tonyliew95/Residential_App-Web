using ImageResizer.Configuration.Logging;
using Nop.Core.Data;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Msp.Setting;
using Nop.Services.Enumeration;
using Nop.Services.Helpers;
using Nop.Services.Messages;
using Nop.Services.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Nop.Services.Msp.Cryptography
{
    public class CheckHotWalletStatusTask : IScheduleTask
    {
        //private readonly IQueuedEmailService _queuedEmailService;
        //private readonly IEmailSender _emailSender;
        private readonly IWorkflowMessageService _workflowMessageService;
        //private readonly ILogger _logger;
        private readonly IRepository<MSP_Setting> _settingRepository;
        private readonly IRepository<Customer> _customerRepository;
        private readonly IMspHelper _mspHelper;

        public CheckHotWalletStatusTask
        (
            //IQueuedEmailService queuedEmailService,
            //IEmailSender emailSender,
            //ILogger logger,
            IWorkflowMessageService workflowMessageService,
            IRepository<MSP_Setting> settingRepository,
            IRepository<Customer> customerRepository,
            IMspHelper mspHelper
        )
        {
            //_queuedEmailService = queuedEmailService;
            ////_emailSender = emailSender;
            //_logger = logger;
            _workflowMessageService = workflowMessageService;
            _settingRepository = settingRepository;
            _customerRepository = customerRepository;
            _mspHelper = mspHelper;
        }

        /// <summary>
        /// Execute a task
        /// </summary>
        public virtual void Execute()
        {
            //Check function
            string EmailAddressToITSupport = _mspHelper.GetSettingValueByKey(GlobalSettingEnum.MSPEmailAddressToITSupport, "");
            string ThresholdTime = _mspHelper.GetSettingValueByKey(GlobalSettingEnum.MSPThresholdTimeSendEmailToITSupport, "");
            string WalletStatus = _mspHelper.GetSettingValueByKey(GlobalSettingEnum.MSPEmailAddressToITSupport, "");
            string flag = _mspHelper.GetSettingValueByKey(GlobalSettingEnum.MSPSentWalletSvcNotification, "");

            DateTime.TryParse(WalletStatus, out DateTime getDateTime);
            TimeSpan maxTime = TimeSpan.FromMinutes(Convert.ToInt32(ThresholdTime));

            var IT_support = new Tuple<string>(EmailAddressToITSupport);


            if ((DateTime.UtcNow).Subtract(getDateTime) >= maxTime && flag == "FALSE")
            {
                
                _workflowMessageService.SendNotificationToITSupport(IT_support, 1);
                var updateFlag = _settingRepository.Table.AsQueryable().Where(o => o.SettingKey == "MSP_SentWalletSvcNotification").Select(o => o).FirstOrDefault();
                updateFlag.SettingValue = "TRUE";
                _settingRepository.Update(updateFlag);
                //    try
                //    {
                //        // _emailSender.SendEmail(queuedEmail.EmailAccount,
                //        //queuedEmail.Subject,
                //        //queuedEmail.Body,
                //        //queuedEmail.From,
                //        //queuedEmail.FromName,
                //        //queuedEmail.To,
                //        //queuedEmail.ToName,
                //        //queuedEmail.ReplyTo,
                //        //queuedEmail.ReplyToName,
                //        //bcc,
                //        //cc,
                //        //queuedEmail.AttachmentFilePath,
                //        //queuedEmail.AttachmentFileName,
                //        //queuedEmail.AttachedDownloadId);

                //        //queuedEmail.SentOnUtc = DateTime.UtcNow;
                //        //var customer = _customerRepository.Table.AsQueryable().Where(o => o.Id == 1526).Select(p => p).FirstOrDefault();
                //        //_workflowMessageService.SendCustomerPasswordRecoveryMessage(customer, 1);

                //        //var updateFlag = _settingRepository.Table.AsQueryable().Where(o => o.SettingKey == "MSP_SentWalletSvcNotification").Select(o => o).FirstOrDefault();
                //        //updateFlag.SettingValue = "TRUE";
                //        //_settingRepository.Update(updateFlag);
                //    }
                //    catch (Exception exc)
                //    {
                //        _logger.Error($"Error sending e-mail. {exc.Message}", exc);
                //    }
                //    finally
                //    {
                //        queuedEmail.SentTries = queuedEmail.SentTries + 1;
                //        _queuedEmailService.UpdateQueuedEmail(queuedEmail);
                //    }
                //}



            }
        }
    }

   
}
