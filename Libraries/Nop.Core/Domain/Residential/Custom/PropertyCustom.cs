using Newtonsoft.Json;
using System;
// Tony Liew 20190315 RDT-65 \/
namespace Nop.Core.Domain.Residential.Custom
{
    /// <summary>
    /// PropertyCustom class
    /// </summary>
    public class PropertyCustom
    {
        /// <summary>
        /// Gets or sets the orgId 
        /// </summary>
        public int orgId { get; set; }

        /// <summary>
        /// Gets or sets the orgName 
        /// </summary>
        public string orgName { get; set; }

        /// <summary>
        /// Gets or sets the user property id 
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Gets or sets the block 
        /// </summary>
        public string block { get; set; }

        /// <summary>
        /// Gets or sets the level 
        /// </summary>
        public string level { get; set; }

        /// <summary>
        /// Gets or sets the unit 
        /// </summary>
        public string unit { get; set; }

        /// <summary>
        /// Gets or sets the reId 
        /// </summary>
        public string reId { get; set; }

        /// <summary>
        /// Gets or sets the updatedOnUtc 
        /// </summary>
        public DateTime updatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the updatedOnUtc 
        /// </summary>
        public string accPropType { get; set; }

        /// <summary>
        /// Gets or sets the orgImage 
        /// </summary>
        public string orgImage { get; set; }
    }
}// Tony Liew 20190315 RDT-65 /\
