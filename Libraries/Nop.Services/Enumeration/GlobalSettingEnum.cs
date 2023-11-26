using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Enumeration
{
    #region GlobalSettingEnum
    public sealed class GlobalSettingEnum
    {
        public const string MSPEmailAddressToITSupport = "MSP_EmailAddressToITSupport"; //Tony 20181003 MSP-151

        public const string MSPThresholdTimeSendEmailToITSupport = "MSP_ThresholdTimeSendEmailToITSupport"; //Tony 20181003 MSP-151   

        public const string MSPHotWalletSvcStatus = "MSP_HotWalletSvcStatus"; //Tony 20181003 MSP-151   

        public const string MSPSentWalletSvcNotification = "MSP_SentWalletSvcNotification"; //Tony 20181003 MSP-151   

        public const string MSPPrepareCustomerSyncTypeBackOffice = "BackOffice"; //LeeChurn 20181031 MSP-409

        public const string MSPPrepareCustomerSyncTypeFirstTimeCreate = "BackOfficeFirstCreate"; //LeeChurn 20181031 MSP-409
    }
}
    #endregion
