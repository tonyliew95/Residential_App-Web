using Nop.Web.Framework.Mvc.ModelBinding;
using Nop.Web.Framework.Mvc.Models;

// Tony Liew 20181204 MDT-122 \/
namespace Nop.Web.Areas.Admin.Models.Settings
{
    public class AllMspSettingsListModel : BaseNopModel
    {
        [NopResourceDisplayName("Admin.Configuration.Settings.AllSettings.Fields.SettingKey")]
        public string SearchSettingName { get; set; }
        [NopResourceDisplayName("Admin.Configuration.Settings.AllSettings.Fields.SettingValue")]
        public string SearchSettingValue { get; set; }
    }
}
// Tony Liew 20181204 MDT-122 /\
