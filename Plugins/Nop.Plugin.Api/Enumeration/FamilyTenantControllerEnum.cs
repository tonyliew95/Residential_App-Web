using Nop.Core.Enumeration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Api.Enumeration
{
    /// <summary>
    /// FamilyTenant Controller [Global] Enumeration
    /// </summary>
    public enum FamilyTenantController_Global
    {
        /// <summary>
        /// 1005 = pending
        /// </summary>
        [ValueAttribute("P")]
        [Description("Pending")]
        pending = 1005,

        /// <summary>
        /// 1006 = accepted
        /// </summary>
        [ValueAttribute("A")]
        [Description("Accepted")]
        accepted = 1006,

        /// <summary>
        /// 1007 = Inprogress
        /// </summary>
        [ValueAttribute("I")]
        [Description("In Progress")]
        inprogress = 1007,

        /// <summary>
        /// 1008 = Closed
        /// </summary>
        [ValueAttribute("C")]
        [Description("Closed")]
        closed = 1008,

        /// <summary>
        /// 1009 = Cancel
        /// </summary>
        [ValueAttribute("X")]
        [Description("Cancel")]
        cancel = 1009,

        /// <summary>
        /// 1011 = Terminate
        /// </summary>
        [ValueAttribute("TERMI")]
        [Description("Terminate")]
        terminate = 1011,

        #region types of resident
        /// <summary>
        /// 1 = family
        /// </summary>
        [ValueAttribute("F")]
        [Description("family member")]
        family = 1,

        /// <summary>
        /// 2 = tenant
        /// </summary>
        [ValueAttribute("T")]
        [Description("master tenant")]
        tenant = 2,

        /// <summary>
        /// 3 = sub tenant
        /// </summary>
        [ValueAttribute("B")]
        [Description("sub tenant")]
        subTenant = 3,

        /// <summary>
        /// 4 = primary
        /// </summary>
        [ValueAttribute("P")]
        [Description("Owner")]
        primary = 4,
        #endregion

    }

    /// <summary>
    /// FamilyTenant Controller [Details] Enumeration
    /// </summary>
    public enum FamilyTenantController_Details
    {
        /// <summary>
        /// 3013 = No Data Available
        /// </summary>
        [Description("No Data Available")]
        invalidIncidentDetails = 3013,
    }

    /// <summary>
    /// FamilyTenant Controller [Insert] Enumeration
    /// </summary>
    public enum FamilyTenantController_Insert
    {
        /// <summary>
        /// 4000 = The User does not in the system
        /// </summary>
        [Description("The user does not in the system")]
        userNotInSystem = 4000,

        /// <summary>
        /// 4001 = The user property does not mapped into the system
        /// </summary>
        [Description("The user property does not mapped into the system")]
        propertiesDoesNotMap = 4001,

        /// <summary>
        /// 3013 = Invalid Date Format
        /// </summary>
        [Description("invalid date format")]
        invalidDateFormat = 3013,

        /// <summary>
        /// 3014 = Invalid account type
        /// </summary>
        [Description("Invalid account type")]
        invalidType = 3014,

        /// <summary>
        /// 3015 = Occupy start date is greater than Occupy end
        /// </summary>
        [Description("Occupy start date is greater than Occupy end")]
        invalidOccupyStart = 3015,

        /// <summary>
        /// 3016 = Reminder date is greater than Occupy end
        /// </summary>
        [Description("Reminder date is greater than Occupy end")]
        invalidReminderDate = 3016,
    }

    /// <summary>
    /// FamilyTenant Controller [Updated] Enumeration
    /// </summary>
    public enum FamilyTenantController_Updated
    {
        /// <summary>
        /// 4001 = The user account does not in the system
        /// </summary>
        [Description("The user account does not in the system")]
        userAccountNotInSystem = 4001,

        /// <summary>
        /// 3017 = Country code is null
        /// </summary>
        [Description("Country code is null")]
        countryCodeFormatInvalid = 3017,

        /// <summary>
        /// 3018 = Empty Msisdn
        /// </summary>
        [Description("Empty Msisdn")]
        emptyMsisdn = 3018,
    }
}
