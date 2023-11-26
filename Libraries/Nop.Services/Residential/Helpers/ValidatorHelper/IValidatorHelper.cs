using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Residential.Helpers.ValidatorHelper
{
    public interface IValidatorHelper
    {
        /// <summary>
        /// Validate Input Date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        bool validateInputDate(string date); //Tony Liew 20190307 RDT-118

        /// <summary>
        /// Validate Input Time
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        bool validateInputTime(string time);//Tony Liew 20190307 RDT-118
    }
}
