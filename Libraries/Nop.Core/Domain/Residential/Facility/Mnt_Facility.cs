using System;

//Tony Liew 20190417 RDT-202 \/
namespace Nop.Core.Domain.Residential.Facility
{
    public class Mnt_Facility : BaseEntity
    {
        /// <summary>
        /// Gets or Sets the OrganizationId
        /// </summary>
        public int OrganizationId {get;set;}

        /// <summary>
        /// Gets or Sets the Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets the Desc
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// Gets or Sets the IsRequireDeposit
        /// </summary>
        public bool IsRequireDeposit { get; set; }

        /// <summary>
        /// Gets or Sets the DepositAmount
        /// </summary>
        public decimal DepositAmount { get; set; }

        /// <summary>
        /// Gets or Sets the IsAutoApprove
        /// </summary>
        public bool IsAutoApprove { get; set; }

        /// <summary>
        /// Gets or Sets the Pic_Name
        /// </summary>
        public string Pic_Name { get; set; }

        /// <summary>
        /// Gets or Sets the Pic_Msisdn
        /// </summary>
        public string Pic_Msisdn { get; set; }

        /// <summary>
        /// Gets or Sets the Pic_Email
        /// </summary>
        public string Pic_Email { get; set; }

        /// <summary>
        /// Gets or Sets the Unit
        /// </summary>
        public Int16 Unit { get; set; }

        /// <summary>
        /// Gets or Sets the Status
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or Sets the CreatedBy
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// Gets or Sets the CreatedOnUtc
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or Sets the UpdatedBy
        /// </summary>
        public int UpdatedBy { get; set; }

        /// <summary>
        /// Gets or Sets the UpdatedOnUtc
        /// </summary>
        public DateTime UpdatedOnUtc { get; set; }
    }
}
//Tony Liew 20190417 RDT-202 /\