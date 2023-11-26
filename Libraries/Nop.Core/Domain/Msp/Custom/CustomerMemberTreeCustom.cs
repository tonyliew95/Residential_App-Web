using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Msp.Custom
{
    public class CustomerMemberTreeCustom
    {
        /// <summary>
        /// Gets or sets the Username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the CustomerID
        /// </summary>
        public int CustomerID { get; set; }

        /// <summary>
        /// Gets or sets the ParentID
        /// </summary>
        public int? ParentID { get; set; }
        
    }
}
