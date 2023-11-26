using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Core.Domain.Msp.Setting;
using System;
using System.Collections.Generic;
using Nop.Core; 
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.RoadMap
{
    /// <summary>
    /// RoadMap service interface
    /// </summary>
    public partial interface IRoadMapService
    {
        //wailiang 20181231 MDT-149 \/
        IList<MSP_RoadMap> GetAllRoadMap(bool isactive, int pageIndex = 0, int pageSize = int.MaxValue); //wailiang 20190111 MSP-667
        
        /// <summary>
        /// Retrieve roadmap by Id and active
        /// </summary>
        /// <param name="id"></param>
        /// <param name="active"></param>
        /// <returns></returns>
        MSP_RoadMap GetRoadMapById(int id);

        /// <summary>
        /// Retrieve roadmap by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        void UpdateSeq(int[] Id); //wailiang 20190109 MDT-149

        /// <summary>
        /// Inserts an roadmap content
        /// </summary>
        /// <param name="roadmap"></param>
        void InsertRoadMapContent(MSP_RoadMap roadmap);

        /// <summary>
        /// Updates an roadmap content
        /// </summary>
        /// <param name="roadmap"></param>
        void UpdateRoadMapContent(MSP_RoadMap roadmap);

        /// <summary>
        /// Deletes an roadmap content
        /// </summary>
        /// <param name="roadmap"></param>
        void DeleteRoadMapContent(MSP_RoadMap roadmap);
        //wailiang 20181231 MDT-149 /\
    }
}
