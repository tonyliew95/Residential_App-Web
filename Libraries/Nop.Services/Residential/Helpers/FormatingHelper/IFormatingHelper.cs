using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Tony Liew 20190320 \/
namespace Nop.Services.Residential.Helpers.FormatingHelper
{
    public interface IFormatingHelper
    {
        /// <summary>
        /// Validate Input date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        string getDateFormat(DateTime date);//Tony Liew 20190307 RDT-118

        /// <summary>
        /// Validate Input date from mobile
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        string getDateFormat(DateTime? date);//Tony Liew 20190307 RDT-118

        /// <summary>
        /// Validate Input time
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        string getTimeFormat(DateTime time);//Tony Liew 20190307 RDT-118

        /// <summary>
        /// Converting time into UTC
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        string getTimeFormatUTC(string time);//Tony Liew 20190307 RDT-118

        /// <summary>
        /// Converting datetime into UTC
        /// </summary>
        /// <param name="date"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        DateTime getDateTimeFormatUTC(string date, string time);//Tony Liew 20190307 RDT-118

        /// <summary>
        /// Converting date into UTC
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        string getDateFormatUTC(string date); //Tony Liew 20190307 RDT-118

        /// <summary>
        /// Converting datetime into Unix Time
        /// </summary>
        /// <param name="date"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        string ToUnixTime(DateTime date);//Tony Liew 20190403 RDT-116

        /// <summary>
        /// Get Period DateTime
        /// </summary>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <returns></returns>
        string GetPeriodDateTime(DateTime? dateFrom, DateTime? dateTo);//Tony Liew 20190403 RDT-116

    }
}
// Tony Liew 20190320 /\
