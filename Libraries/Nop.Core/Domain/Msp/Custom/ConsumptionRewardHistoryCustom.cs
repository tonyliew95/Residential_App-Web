using Nop.Core.Configuration;
using System;

namespace Nop.Core.Domain.Msp
{
    /// <summary>
    /// TeamConsumptionRewardCustom
    /// </summary>
    public class ConsumptionRewardHistoryCustom //wailiang 20180906 MSP-98
    {
        ///// <summary>
        ///// Gets or sets the Customer Id
        ///// </summary>
        //public int CustomerID { get; set; }

        /// <summary>
        /// Gets or sets the Platform Id
        /// </summary>
        public string PlatformName { get; set; }

        /// <summary>
        /// Gets or sets the Date
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the TotalDepositReturned
        /// </summary>
        public decimal DepositReturned { get; set; }

        /// <summary>
        /// Gets or sets the TotalMembershipReward
        /// </summary>
        public decimal MembershipReward { get; set; }

        /// <summary>
        /// Gets or sets the TotalConsumptionReward
        /// </summary>
        public decimal ConsumptionReward { get; set; }

        /// <summary>
        /// Gets or sets the HonoraryCitizenReward
        /// </summary>
        public decimal HonoraryCitizenReward { get; set; }

        /// <summary>
        /// Gets or sets the TotalReward
        /// </summary>
        public decimal TotalReward { get; set; }
    }
}
