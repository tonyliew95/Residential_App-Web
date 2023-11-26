namespace Nop.Web.Areas.Admin.Models.Settings
{
    // Tony Liew 20181204 MDT-122 \/
    /// <summary>
    /// All Msp Settings
    /// </summary>
    public class AllMspSettingsModel
    {
        /// <summary>
        /// Get or Set Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Get or Set SettingKey
        /// </summary>
        public string SettingKey { get; set; }

        /// <summary>
        /// Get or Set SettingValue
        /// </summary>
        public string SettingValue { get; set; }

        /// <summary>
        /// Get or Set Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Get or Set Status
        /// </summary>
        public string Status { get; set; }
    }
    // Tony Liew 20181204 MDT-122 /\
}
