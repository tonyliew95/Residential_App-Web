using Nop.Services.Residential.Helpers.BaseHelper;
using System;
using System.Globalization;

// Tony Liew 20190321 \/
namespace Nop.Services.Residential.Helpers.ValidatorHelper
{
    public class ValidatorHelper: IValidatorHelper
    {

        private readonly IBaseHelper _baseHelper;

        public ValidatorHelper(IBaseHelper baseHelper)
        {
            _baseHelper = baseHelper;
        }

        /// <summary>
        /// Validate Input Date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public bool validateInputDate(string date)//Tony Liew 20190307 RDT-118
        {
            if (!string.IsNullOrEmpty(date))
                return DateTime.TryParseExact(date, _baseHelper.getSettingValueByKey("RES_DateFormat", "dd MMMM yyyy"), CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime newDate);
            else
                return true;
        }

        /// <summary>
        /// Validate Input Time
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public bool validateInputTime(string time)//Tony Liew 20190307 RDT-118
        {
            if (!string.IsNullOrEmpty(time))
                return DateTime.TryParseExact(time, _baseHelper.getSettingValueByKey("RES_TimeFormat", "hh:mm tt"), CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime newTime);
            else
                return true;
        }
    }
}
// Tony Liew 20190321 /\