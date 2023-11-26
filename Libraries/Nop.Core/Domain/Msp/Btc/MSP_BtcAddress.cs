using Nop.Core.Enumeration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Msp.Btc
{
	/// <summary>
	/// Represents a MSP_BtcAddress
	/// </summary>
	public partial class MSP_BtcAddress : BaseEntity
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public MSP_BtcAddress()
        {
            IsSync = false; //initialize default value when there is new/update record into BtcAddress //WilliamHo 20181001
        }

		/// <summary>
		/// Gets or sets the BTC Address Id
		/// </summary>
		public int AddressId { get; set; }

		/// <summary>
		/// Gets or sets the BTC Address 
		/// </summary>
		public string BtcAddress { get; set; }

        /// <summary>
        /// Gets or sets the date and time of created 
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets User Id of the user this address is assigned to
        /// </summary>
        public int? UserId { get; set; }

        /// <summary>
        /// Gets or sets the date and time of this address assigned to a user
        /// </summary>
        public DateTime? UserIdOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the number of times this btc address has been used
        /// </summary>
        public int UsedCount { get; set; }

		/// <summary>
		/// Gets or sets the date and time of this address last used by the user
		/// </summary>
		public DateTime? LastUsedOnUtc { get; set; }

        /// <summary>
		/// Gets or sets the IsSync for syncronization between DBLive env and DBAudit env
		/// </summary>
        /// WilliamHo 20181001
		public bool IsSync { get; set; }
    }
}
