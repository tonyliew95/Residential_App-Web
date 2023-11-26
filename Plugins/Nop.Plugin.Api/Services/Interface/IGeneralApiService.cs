using Nop.Plugin.Api.Models.General.DTOs;
using System.Collections.Generic;

namespace Nop.Plugin.Api.Services.Interface
{
    public interface IGeneralApiService
    {
        /// <summary>
        /// Get general setting from Adm_SysControl
        /// </summary>
        /// <returns></returns>
        SettingDto GetGeneralSetting(); //JK 20190322 RDT-166
    }
}
