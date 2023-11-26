using Nop.Core.Domain.Msp.Member;
using Nop.Core.Domain.Msp.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.Msp.Member
{
    public partial class MSP_MemberWithdrawalAddress_HistoryMap : NopEntityTypeConfiguration<MSP_MemberWithdrawalAddress_History>
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public MSP_MemberWithdrawalAddress_HistoryMap()
        {
            this.ToTable("MSP_MemberWithdrawalAddress_History");
            this.HasKey(p => p.Id);
        }
    }
}
