using Nop.Core;
using Nop.Core.Domain.Msp.Custom;
using Nop.Core.Domain.Msp.Member;
using System;
using System.Collections.Generic;
using static Nop.Services.Msp.MemberTree.MemberTreeServices;

namespace Nop.Services.Msp.MemberTree
{
    public interface IMemberTreeServices
    {
        IPagedList<MSP_MemberTree> GetMemberTreeList(string Username, string GlobalGuid, string IntroducerGuid
            //, DateTime? DateFrom, DateTime? DateTo //RW 20181227 MSP-608
            , int pageIndex = 0, int pageSize = int.MaxValue); //RW 20181214 MDT-139
        IList<CustomerMemberTreeCustom> GetCustomerMemberTreeList(int customerId, int level); //RW 20181214 MDT-139
        CustomerMemberTreeDetailsCustom GetCustomerMemberTreeDetails(int customerId); //RW 20181214 MDT-139
    }
}