using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Enumeration
{
    public enum UserIdentityType
    {
        [ValueAttribute("X")]
        [DescriptionAttribute("Old IC")]
        OldIC,
        [ValueAttribute("N")]
        [DescriptionAttribute("New IC")]
        NewIC,
        [ValueAttribute("A")]
        [DescriptionAttribute("Armed Force IC")]
        ArmedForceIC,
        [ValueAttribute("P")]
        [DescriptionAttribute("Passport")]
        Passport
    }

    public enum UserAccountType
    {
        [ValueAttribute("P")]
        [DescriptionAttribute("Principal Account/Master/Owner")]
        Owner,
        [ValueAttribute("T")]
        [DescriptionAttribute("Master Tenant")]
        MasterTenant,
        [ValueAttribute("S")]
        [DescriptionAttribute("Supplementary Account")]
        Supplementary,
    }

    public enum UserAccount_OccupancyType
    {
        [ValueAttribute("F")]
        [DescriptionAttribute("Family Member")]
        Family,
        [ValueAttribute("B")]
        [DescriptionAttribute("Sub-tenant")]
        Sub_Tenant,
        [ValueAttribute("V")]
        [DescriptionAttribute("Vacant/Airbnb")]
        Vacant_AirBnb,
    }

    public enum UserAccount_StatusType
    {
        [ValueAttribute("PAPV")]
        [DescriptionAttribute("Pending Approve")]
        PendingApprove,
        [ValueAttribute("ACTV")]
        [DescriptionAttribute("Active")]
        Active,
        [ValueAttribute("TERM")]
        [DescriptionAttribute("Terminated")]
        Terminated,
        [ValueAttribute("SPND")]
        [DescriptionAttribute("Suspended")]
        Suspended,
        [ValueAttribute("REJC")]
        [DescriptionAttribute("Rejected")]
        Rejected,
        [ValueAttribute("PACT")]
        [DescriptionAttribute("Pending Activation")]
        PendingActivation
    }

    public enum VisitorTimestampType
    {
        [ValueAttribute("CI")]
        [DescriptionAttribute("Clock In")]
        ClockIn,
        [ValueAttribute("CO")]
        [DescriptionAttribute("Clock Out")]
        ClockOut
    }

    public enum TrxVisitorStatus
    {
        [ValueAttribute("A")]
        [DescriptionAttribute("Approved")]
        Approved
    }

    public enum FacilityStatusType
    {
        [ValueAttribute("O")]
        [DescriptionAttribute("Open")]
        Open,
        [ValueAttribute("C")]
        [DescriptionAttribute("Close")]
        Close,
        [ValueAttribute("M")]
        [DescriptionAttribute("Maintenance")]
        Maintenance,
        [ValueAttribute("T")]
        [DescriptionAttribute("Terminated")]
        Terminated
    }

    public enum ModuleVisibleType
    {
        [ValueAttribute("S")]
        [DescriptionAttribute("Show")]
        Show,
        [ValueAttribute("D")]
        [DescriptionAttribute("Dim")]
        Dim,
        [ValueAttribute("H")]
        [DescriptionAttribute("Hide")]
        Hide
    }
}
