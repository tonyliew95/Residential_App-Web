using Nop.Core.Domain.Msp.Member;
using Nop.Core.Domain.Msp.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.Msp.Member
{
    public partial class MSP_MemberProfilePictureMap : NopEntityTypeConfiguration<MSP_MemberProfilePicture>
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public MSP_MemberProfilePictureMap()
        {
            this.ToTable("MSP_MemberProfilePicture");
            this.HasKey(p => p.Id);
        }
    }
}
