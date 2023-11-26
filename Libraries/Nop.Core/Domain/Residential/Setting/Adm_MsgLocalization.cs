using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Residential.Setting
{
    public class Adm_MsgLocalization : BaseEntity
    {
        /// <summary>
        /// Gets or Sets the Code
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// Gets or Sets the Message_EN
        /// </summary>
        public string Message_EN { get; set; }

        /// <summary>
        /// Gets or Sets the Message_CN
        /// </summary>
        public string Message_CN { get; set; }

        /// <summary>
        /// Gets or Sets the Message_BM
        /// </summary>
        public string Message_BM { get; set; }

        /// <summary>
        /// Gets or Sets the Created By
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// Gets or Sets the Created On Utc
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

    }
}
