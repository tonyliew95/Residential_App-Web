using Nop.Core.Domain.Residential.Incident;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nop.Core.Domain.Residential.Custom
{
    [NotMapped]
    public class AdminIncidentCustom : Trx_Incident //wailiang 20190320 RDT-127
    {
        /// <summary>
        /// Title Column
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Desc Column
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// Inc_DateTime Column
        /// </summary>
        public DateTime Inc_DateTime { get; set; }

        /// <summary>
        /// CreateOnUtc Column
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Inc_Category Column
        /// </summary>
        public string Inc_Category { get; set; }

        /// <summary>
        /// Inc_Type Column
        /// </summary>
        public string Inc_Type { get; set; }

        /// <summary>
        /// Location Column
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// LocationType Column
        /// </summary>
        public string LocationType { get; set; }

        /// <summary>
        /// Unit_Number Column
        /// </summary>
        public string Unit_Number { get; set; }

        /// <summary>
        /// AllowShare Column
        /// </summary>
        public bool AllowShare { get; set; }

        /// <summary>
        /// ReportedBy Column
        /// </summary>
        public int ReportedBy { get; set; }

        /// <summary>
        /// Organization Column
        /// </summary>
        public string Organization { get; set; }

        /// <summary>
        /// AssignedTo Column
        /// </summary>
        public string AssignedTo { get; set; }

        /// <summary>
        /// Status Column
        /// </summary>
        public string Status { get; set; }

    }

}
