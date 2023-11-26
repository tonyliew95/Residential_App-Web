using Nop.Core.Domain.Msp.Member;
using Nop.Core.Domain.Msp.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.Msp.Member
{
    public partial class MSP_SessionMap : NopEntityTypeConfiguration<MSP_Session>
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public MSP_SessionMap()
        {
            this.ToTable("MSP_Session");
            this.HasKey(p => p.Id);
        }
    }
}
