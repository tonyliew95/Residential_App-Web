using Nop.Core.Domain.Msp.Member;
using Nop.Core.Domain.Msp.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.Msp.Member
{
    public partial class MSP_MemberTreeMap : NopEntityTypeConfiguration<MSP_MemberTree>
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public MSP_MemberTreeMap()
        {
            this.ToTable("MSP_MemberTree");
            this.HasKey(p => p.Id);
            this.Property(p => p.WithdrawalLimit).HasPrecision(26, 8); // Tony Liew 20181126 MDT-8
            this.Property(p => p.WithdrawalLimitVIP).HasPrecision(26, 8);// Tony Liew 20181126 MDT-8
            //wailiang 20190228 MDT-286 \/
            this.Property(p => p.CurrMaxDepositPlanAmt).HasPrecision(26, 8);
            this.Property(p => p.RoleMinDepositMBtc).HasPrecision(26, 8);
            this.Property(p => p.RoleMinConsumptionMbtc).HasPrecision(26, 8);
            this.Property(p => p.RoleMemberDepositMbtc).HasPrecision(26, 8);
            this.Property(p => p.RoleMemberConsumptionMbtc).HasPrecision(26, 8);
            //wailiang 20190228 MDT-286 /\
        }
    }
}
