using Nop.Core.Domain.Msp.Member;
using Nop.Core.Domain.Msp.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.Msp.Member
{
    public partial class MSP_MemberScorePctMap : NopEntityTypeConfiguration<MSP_MemberScorePct>
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public MSP_MemberScorePctMap()
        {
            this.ToTable("MSP_MemberScorePct");
            this.HasKey(p => p.Id);

            this.Ignore(p => p.ScoreTypeEnum); 
        }
    }
}
