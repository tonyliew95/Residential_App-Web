using System;

namespace Nop.Core.Domain.Residential.Visitor
{
    public class Trx_Visitor_RecordTimestamp : BaseEntity
    {
        /// <summary>
        /// Gets or Sets the [TrxVisitorId]
        /// </summary>
        public int TrxVisitorId { get; set; }

        /// <summary>
        /// Gets or Sets the [ClockType]
        /// </summary>
        public string ClockType { get; set; }

        /// <summary>
        /// Gets or Sets the [TimestampOnUtc]
        /// </summary>
        public DateTime TimestampOnUtc { get; set; }

        /// <summary>
        /// Gets or Sets the CreatedBy
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// Gets or Sets the CreatedOnUtc
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }
    }
}
