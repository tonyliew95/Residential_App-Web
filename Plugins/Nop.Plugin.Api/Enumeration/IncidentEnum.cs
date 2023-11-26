using Nop.Core.Enumeration;

namespace Nop.Plugin.Api.Enumeration
{
    //Tony Liew 20190307 RDT-118 \/
    /// <summary>
    /// Incident Controller [Insert] Enumeration
    /// </summary>
    #region Incident Controller [Insert] Enumeration
    public enum IncidentController_Insert
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
        /// 1010 = Incident Reported
        /// </summary>
        [Description("Incident Reported")]
        reportSuccessfully = 1010,

        /// <summary>
        /// 3000 = Invalid Date Format
        /// </summary>
        [ValueAttribute("invalid date format")]
        invalidDateFormat = 3000,

        /// <summary>
        /// 3001 = Invalid Time Format
        /// </summary>
        [ValueAttribute("invalid time format")]
        invalidTimeFormat = 3001,

        /// <summary>
        /// 5000 = Unsuccessfully Insert
        /// </summary>
        [ValueAttribute("unsuccessfully report incident")]
        unsuccessfullyInsert = 5000,
    }

    #endregion


    #region Incident Controller [Details] Enumeration
    /// <summary>
    /// Incident Controller [Details] Enumeration
    /// </summary>
    public enum IncidentController_IncidentDetails
    {

        /// <summary>
        /// 3013 = No Data Available
        /// </summary>
        [Description("No Data Available")]
        invalidIncidentDetails = 3013,

    }
    #endregion


    //Tony Liew 20190307 RDT-118 /\
}
