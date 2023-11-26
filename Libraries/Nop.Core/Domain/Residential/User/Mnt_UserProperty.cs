using System;

// WKK 20190322 RDT-163 [API] Login - profile dto
namespace Nop.Core.Domain.Residential.User
{
    public class Mnt_UserProperty : BaseEntity
    {
        /// <summary>
        /// Gets or Sets the UserOrgId
        /// </summary>
        public int UserOrgId { get; set; }

        /// <summary>
        /// Gets or Sets the BlockId
        /// </summary>
        public int BlockId { get; set; }

        /// <summary>
        /// Gets or Sets the LevelId
        /// </summary>
        public int LevelId { get; set; }

        /// <summary>
        /// Gets or Sets the UnitNumber
        /// </summary>
        public string UnitNumber { get; set; }

        /// <summary>
        /// Gets or Sets the OwnershipTypeId
        /// </summary>
        public int OwnershipTypeId { get; set; }

        /// <summary>
        /// Gets or Sets the CreatedBy
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// Gets or Sets the CreatedOnUtc
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or Sets the UpdatedOnUtc
        /// </summary>
        public DateTime UpdatedOnUtc { get; set; }

        /// <summary>
        /// Gets or Sets the UpdatedBy
        /// </summary>
        public int UpdatedBy { get; set; }

    }
}
