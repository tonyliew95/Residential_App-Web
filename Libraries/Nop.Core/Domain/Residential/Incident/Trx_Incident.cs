using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Core.Domain.Residential.Incident
{
    public class Trx_Incident : BaseEntity
    {
        public Trx_Incident() { }

        /// <summary>
        /// Gets or sets the Inc_TypeId
        /// </summary>
        public int Inc_TypeId { get; set; }

        /// <summary>
        /// Gets or sets the Inc_CategoryId
        /// </summary>
        public int Inc_CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the Inc_ItemId
        /// </summary>
        public int? Inc_ItemId { get; set; }

        /// <summary>
        /// Gets or sets the Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the Desc
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// Gets or sets the Inc_Date
        /// </summary>
        public DateTime Inc_DateTime { get; set; }

        /// <summary>
        /// Gets or sets the Latitude
        /// </summary>
        public decimal? Latitude { get; set; }

        /// <summary>
        /// Gets or sets the Longitude
        /// </summary>
        public decimal? Longitude { get; set; }

        /// <summary>
        /// Gets or sets the Location
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Gets or sets the LocationType
        /// </summary>
        public bool? LocationType { get; set; }

        /// <summary>
        /// Gets or sets the Unit_Number
        /// </summary>
        public string Unit_Number { get; set; }

        /// <summary>
        /// Gets or sets the ReportedBy
        /// </summary>
        public int ReportedBy { get; set; }

        /// <summary>
        /// Gets or sets the AssignedTo
        /// </summary>
        public int? AssignedTo { get; set; }

        /// <summary>
        /// Gets or sets the AllowShare
        /// </summary>
        public bool AllowShare { get; set; }

        /// <summary>
        /// Gets or sets the Organization_Id
        /// </summary>
        public int Organization_Id { get; set; }

        /// <summary>
        /// Gets or sets the Property_Id
        /// </summary>
        public int Property_Id { get; set; }

        /// <summary>
        /// Gets or sets the Status
        /// </summary>
        public string Status { get; set; }

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
