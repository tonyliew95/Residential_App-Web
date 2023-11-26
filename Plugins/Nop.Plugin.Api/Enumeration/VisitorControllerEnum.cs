using Nop.Core.Enumeration;

namespace Nop.Plugin.Api.Enumeration
{
    /// <summary>
    /// Visitor Controller Enumeration
    /// </summary>
    public enum VisitorControllerEnum
    {
        /// <summary>
        /// 5001 = Invalid visitor name
        /// </summary>
        [Description("Invalid visitor name")]
        InvalidVisitorName = 5001,

        /// <summary>
        /// 5002 = Invalid phone number
        /// </summary>
        [Description("Invalid phone number")]
        InvalidPhoneNumber = 5002,

        /// <summary>
        /// 5003 = Invalid visit type
        /// </summary>
        [Description("Invalid visit type")]
        InvalidVisitType = 5003,

        /// <summary>
        /// 5004 = Invalid visit date
        /// </summary>
        [Description("Invalid visit date")]
        InvalidVisitDate = 5004,

        /// <summary>
        /// 5005 = Invalid vehicle type
        /// </summary>
        [Description("Invalid vehicle type")]
        InvalidVehicleType = 5005,

        /// <summary>
        /// 5006 = Invalid vehicle number
        /// </summary>
        [Description("Invalid vehicle number")]
        InvalidVehicleNumber = 5006,

        /// <summary>
        /// 5007 = Invalid visit trasaction id
        /// </summary>
        [Description("Invalid visit trasaction id")]
        InvalidTrxId = 5007,

        /// <summary>
        /// 5008 = Invalid clock in out type
        /// </summary>
        [Description("Invalid clock in out type")]
        InvalidClockInOut = 5008,

        /// <summary>
        /// 5009 = Already clock in
        /// </summary>
        [Description("Already clock in")]
        AlreadyClockIn = 5009,

        /// <summary>
        /// 5010 = Already clock out
        /// </summary>
        [Description("Already clock out")]
        AlreadyClockOut = 5010,

        /// <summary>
        /// 5011 = Wrong visit date
        /// </summary>
        [Description("Wrong visit date")]
        WrongVisitDate = 5011,
    }
}