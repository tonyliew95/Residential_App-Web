using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Tony Liew 20190403 RDT-175 \/
namespace Nop.Core.Domain.Residential.User
{
    public class Mnt_FamilyTenant : BaseEntity
    {
        /// <summary>
        /// Gets or Sets the UserAccId
        /// </summary>
        public int UserAccId { get; set; }

        /// <summary>
        /// Gets or Sets the OrganizationId
        /// </summary>
        public int OrganizationId { get; set; }

        /// <summary>
        /// Gets or Sets the OrgUnitPropertyId
        /// </summary>
        public int OrgUnitPropertyId { get; set; }

        /// <summary>
        /// Gets or Sets the MasterUserId
        /// </summary>
        public int MasterUserId { get; set; }

        /// <summary>
        /// Gets or Sets the FamilyTenantUserId
        /// </summary>
        public int FamilyTenantUserId { get; set; }

        /// <summary>
        /// Gets or Sets the Emergency
        /// </summary>
        public bool Emergency { get; set; }

        /// <summary>
        /// Gets or Sets the Deleted
        /// </summary>
        public bool Deleted { get; set; }

        /// <summary>
        /// Gets or Sets the Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets the Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or Sets the CountryCode
        /// </summary>
        public string CountryCode { get; set; }

        /// <summary>
        /// Gets or Sets the Msisdn
        /// </summary>
        public string Msisdn { get; set; }

        /// <summary>
        /// Gets or Sets the Info
        /// </summary>
        public string Info { get; set; }

        /// <summary>
        /// Gets or Sets the OccupyStart
        /// </summary>
        public DateTime? OccupyStart { get; set; }

        /// <summary>
        /// Gets or Sets the OccupyEnd
        /// </summary>
        public DateTime? OccupyEnd { get; set; }

        /// <summary>
        /// Gets or Sets the Reminder
        /// </summary>
        public DateTime? Reminder { get; set; }

        /// <summary>
        /// Gets or Sets the Filename
        /// </summary>
        public string Filename { get; set; }

        /// <summary>
        /// Gets or Sets the File_URI
        /// </summary>
        public string File_URI { get; set; }

        /// <summary>
        /// Gets or Sets the FileType
        /// </summary>
        public string FileType { get; set; }

        /// <summary>
        /// Gets or sets the CreatedBy
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the CreatedOnUtc
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the UpdatedBy
        /// </summary>
        public int UpdatedBy { get; set; }

        /// <summary>
        /// Gets or sets the UpdatedOnUtc
        /// </summary>
        public DateTime UpdatedOnUtc { get; set; }
    }
}
//Tony Liew 20190403 RDT-175 /\