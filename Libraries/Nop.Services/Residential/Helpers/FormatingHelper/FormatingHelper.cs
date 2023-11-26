using Nop.Core.Data;
using Nop.Core.Domain.Residential.Setting;
using Nop.Services.Residential.Helpers.BaseHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Tony Liew 20190320 \/
namespace Nop.Services.Residential.Helpers.FormatingHelper
{
    public class FormatingHelper : IFormatingHelper 
    {
        private readonly IBaseHelper _baseHelper;
        public FormatingHelper(IBaseHelper baseHelper)
        {
            _baseHelper = baseHelper;
        }
        /// <summary>
        /// Validate Input date from mobile
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public string getDateFormat(DateTime date)//Tony Liew 20190307 RDT-118
        {
            var getValue = _baseHelper.getSettingValueByKey("RES_DateFormat", "dd MMM yyyy");
            return date.ToString(getValue);
        }

        /// <summary>
        /// Validate Input date from mobile
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public string getDateFormat(DateTime? date)//Tony Liew 20190307 RDT-118
        {
            if (date.HasValue)
            {
                var getValue = _baseHelper.getSettingValueByKey("RES_DateFormat", "dd MMM yyyy");
                return date.Value.ToString(getValue);
            }
            else return string.Empty;
        }

        /// <summary>
        /// Validate Input time from mobile in AM/PM
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public string getTimeFormat(DateTime time)//Tony Liew 20190307 RDT-118
        {
            var getValue = _baseHelper.getSettingValueByKey("RES_TimeFormat", "hh:mm tt");
            return time.ToString(getValue);
        }

        /// <summary>
        /// Converting time into UTC
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public string getTimeFormatUTC(string time)//Tony Liew 20190307 RDT-118
        {
            var getValue = _baseHelper.getSettingValueByKey("RES_TimeFormatUTC", "HH:mm:ss");
            var timeInUtc = Convert.ToDateTime(time);
            return timeInUtc.ToString(getValue);
        }

        /// <summary>
        /// Converting date into UTC
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public string getDateFormatUTC(string date)//Tony Liew 20190307 RDT-118
        {
            var getValue = _baseHelper.getSettingValueByKey("RES_DateFormatUTC", "yyyy-MM-dd");
            var timeInUtc = Convert.ToDateTime(date);
            return timeInUtc.ToString(getValue);
        }


        /// <summary>
        /// Converting datetime into UTC
        /// </summary>
        /// <param name="date"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public DateTime getDateTimeFormatUTC(string date , string time)//Tony Liew 20190307 RDT-118
        {
            var dateTime = date + " " + time;
            var timeInUtc = Convert.ToDateTime(dateTime);
            return timeInUtc;
        }

        /// <summary>
        /// Converting datetime into Unix Time
        /// </summary>
        /// <param name="date"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public string ToUnixTime(DateTime date)//Tony Liew 20190403 RDT-116
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            long result = Convert.ToInt64((date.ToUniversalTime() - epoch).TotalSeconds);
            return result.ToString();
        }

        /// <summary>
        /// Get Period DateTime
        /// </summary>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <returns></returns>
        public string GetPeriodDateTime(DateTime? dateFrom , DateTime? dateTo)//Tony Liew 20190403 RDT-116
        {
            if (dateFrom.HasValue && dateTo.HasValue)
                return (dateTo.Value.Year - dateFrom.Value.Year).ToString() + " Years " + (dateTo.Value.Month - dateFrom.Value.Month).ToString() + " Months";
            else
                return string.Empty;
        }

    }
}
// Tony Liew 20190320 /\
