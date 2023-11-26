using System;
using System.Collections.Generic;
using System.Linq;
//Tony Liew 20190313 RDT-118\/
namespace Nop.Core.Domain.Residential.Custom
{
    public class Trx_IncidentCustom
    {
        /// <summary>
        /// id Column
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// title Column
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// location Column
        /// </summary>
        public string location { get; set; }

        /// <summary>
        /// date Column
        /// </summary>
        public DateTime date { get; set; }

        /// <summary>
        /// statusCode Column
        /// </summary>
        public string statusCode { get; set; }

        /// <summary>
        /// status Column
        /// </summary>
        public string status { get; set; }

        /// <summary>
        /// CreateOnUtc Column
        /// </summary>
        public DateTime createdDateOnUtc { get; set; }

        /// <summary>
        /// timeDateOnUtc Column
        /// </summary>
        public DateTime createdTimeOnUtc { get; set; }

        /// <summary>
        /// orgId Column
        /// </summary>
        public int orgId { get; set; }

        /// <summary>
        /// propId Column
        /// </summary>
        public int propId { get; set; }

        /// <summary>
        /// media Column
        /// </summary>
        public IQueryable<MediaCustom> media { get; set; }

        /// <summary>
        /// description Column
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// time Column
        /// </summary>
        public DateTime time { get; set; }
    }

    //Tony Liew 20190313 RDT-118 /\

}
