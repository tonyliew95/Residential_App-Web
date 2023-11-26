using System;

namespace Nop.Core.Domain.Msp
{
    public class ConsumptionRewardSelfCustom //JK 20180926 MSP-97
    {
        public string Username { get; set; }

        public DateTime Date { get; set; }

        public string PlatformName { get; set; }
        
        public decimal DepositReturned { get; set; }

        public decimal MembershipReward { get; set; }

        public decimal ConsumptionReward { get; set; }

        public decimal MerchantReferralReward { get; set; }

        public decimal TotalReward { get; set; }

        public decimal HonoraryCitizenReward { get; set; }
    }
}
