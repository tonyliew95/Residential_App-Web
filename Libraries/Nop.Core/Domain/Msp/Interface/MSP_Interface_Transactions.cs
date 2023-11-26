using System;

namespace Nop.Core.Domain.Msp.Interface
{
    public partial class MSP_Interface_Transactions : BaseEntity
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public MSP_Interface_Transactions()
        {
        }

        /// <summary>
        /// Gets or sets the PlatformID
        /// </summary>
        public int PlatformID { get; set; }

        /// <summary>
        /// Gets or sets the PlatformCode
        /// </summary>
        public string PlatformCode { get; set; }

        /// <summary>
        /// Gets or sets the DistributionTrxID
        /// </summary>
        public string DistTrxID { get; set; }

        /// <summary>
        /// Gets or sets the GlobalUserID
        /// </summary>
        public string GlobalUserID { get; set; }

        /// <summary>
        /// Gets or sets the OrderID
        /// </summary>
        public string OrderID { get; set; }

        /// <summary>
        /// Gets or sets the OrderDateTimeUTC
        /// </summary>
        public DateTime OrderDateTimeUTC { get; set; }

        /// <summary>
        /// Gets or sets the OrderAmount
        /// </summary>
        public decimal OrderAmount { get; set; }

        /// <summary>
        /// Gets or sets the GlobalMerchantID
        /// </summary>
        public string GlobalMerchantID { get; set; }

        /// <summary>
        /// Gets or sets the MerchantName
        /// </summary>
        public string MerchantName { get; set; }

        /// <summary>
        /// Gets or sets the MerchantAmount
        /// </summary>
        public decimal MerchantAmount { get; set; }

        /// <summary>
        /// Gets or sets the CreatedOnUTC
        /// </summary>
        public DateTime CreatedOnUTC { get; set; }

        /// <summary>
        /// Gets or sets the IsProcessed
        /// </summary>
        public bool IsProcessed { get; set; }
    }
}
