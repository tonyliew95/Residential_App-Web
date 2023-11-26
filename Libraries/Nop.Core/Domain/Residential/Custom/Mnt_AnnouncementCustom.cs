using System;
using System.Collections.Generic;

//WKK 20190315 RDT-121\/
namespace Nop.Core.Domain.Residential.Custom
{
    public class Mnt_AnnouncementCustom
    {
        /// <summary>
        /// orgId Column
        /// </summary>
        public int orgId { get; set; }

        /// <summary>
        /// id Column
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// subject Column
        /// </summary>
        public string subject { get; set; }

        /// <summary>
        /// content Column
        /// </summary>
        public string content { get; set; }

        /// <summary>
        /// date Column
        /// </summary>
        public DateTime date { get; set; }

        /// <summary>
        /// date Column
        /// </summary>
        public IEnumerable<MediaCustom> media { get; set; }
    }
 
    //WKK 20190315 RDT-121 /\

}
