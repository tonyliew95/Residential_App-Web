using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Enumeration
{
    /// <summary>
    /// An enumeration file that store all the file enum for RES project
    /// </summary>
    public enum RES_FileEnum
    {
        /// <summary>
        /// 1 = incident
        /// </summary>
        [ValueAttribute("incident")]
        incident = 1,

        /// <summary>
        /// 2 = announcement
        /// </summary>
        [ValueAttribute("announcement")]
        announcement = 2,

        /// <summary>
        /// 3 = avatar
        /// </summary>
        [ValueAttribute("avatar")]
        avatar = 3,

        /// <summary>
        /// 4 = visitor
        /// </summary>
        [ValueAttribute("visitor")]
        visitor = 4,

        /// <summary>
        /// 5 = family
        /// </summary>
        [ValueAttribute("FamilyTenant")]
        familyTenant = 5,

        /// <summary>
        /// 6 = facility
        /// </summary>
        [ValueAttribute("facility")]
        facility = 6,

        /// <summary>
        /// 7 = residential-image
        /// </summary>
        [ValueAttribute("residential-image")]
        residentialImage = 7,
    }
}
