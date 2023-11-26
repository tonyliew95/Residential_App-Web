using System;

// Tony Liew 20190401 RDT-65 \/
namespace Nop.Core.Domain.Residential.Organization
{
    public class Mnt_OrgUnitProperty: BaseEntity
    {
        /// <summary>
        /// Gets and Sets the OrganizationId
        /// </summary>
        public int OrganizationId { get; set; }

        /// <summary>
        /// Gets and Sets the BlockId
        /// </summary>
        public int BlockId { get; set; }

        /// <summary>
        /// Gets and Sets the LevelId
        /// </summary>
        public int LevelId { get; set; }

        /// <summary>
        /// Gets and Sets the UnitNumber
        /// </summary>
        public string UnitNumber { get; set; }

        /// <summary>
        /// Gets and Sets the CreatedBy
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// Gets and Sets the CreatedOnUtc
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets and Sets the UpdatedBy
        /// </summary>
        public int UpdatedBy { get; set; }

        /// <summary>
        /// Gets and Sets the UpdatedOnUtc
        /// </summary>
        public DateTime UpdatedOnUtc { get; set; }
    }
}
// Tony Liew 20190401 RDT-65 /\