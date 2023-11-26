using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Core.Data;
using Nop.Core.Domain.Msp.Setting;
using Nop.Core.Enumeration;
using Nop.Services.Events;
using Nop.Services.Helpers;
using Nop.Services.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using Nop.Core;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.RoadMap
{
    /// <summary>
    /// RoadMap Service
    /// </summary>
    public partial class RoadMapService : IRoadMapService
    {
        #region Fields
        private readonly IRepository<MSP_RoadMap> _mspRoadMapRepository;
        private readonly ILocalizationService _localizationService;
        private readonly IEventPublisher _eventPublisher;
        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly IMspHelper _mspHelper;
        #endregion

        #region Ctor
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="mspRoadMapRepository"></param>
        /// <param name="localizationService"></param>
        /// <param name="eventPublisher"></param>
        public RoadMapService(
            IRepository<MSP_RoadMap> mspRoadMapRepository,
            ILocalizationService localizationService,
            IDateTimeHelper dateTimeHelper,
            IMspHelper mspHelper,
            IEventPublisher eventPublisher
        )
        {
            this._mspRoadMapRepository = mspRoadMapRepository;
            this._localizationService = localizationService;
            this._eventPublisher = eventPublisher;
            this._dateTimeHelper = dateTimeHelper;
            this._mspHelper = mspHelper;
        }
        #endregion

        #region Methods
        //wailiang 20181231 MDT-149 \/
        //wailiang 20190111 MSP-667 \/
        public virtual IList<MSP_RoadMap> GetAllRoadMap(bool isactive, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _mspRoadMapRepository.Table;
            query = query.Where(x => x.IsActive == isactive);

            //query = query.OrderByDescending(x => x.CreatedOnUtc);

            //var msp_Roadmap = new PagedList<MSP_RoadMap>(query, pageIndex, pageSize);

            //return msp_Roadmap;
            return query.OrderBy(x=>x.Seq).ToList(); //wailiang 20190111 MSP-671
        }
        //wailiang 20190111 MSP-667 /\

        /// <summary>
        /// Gets an road map by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual MSP_RoadMap GetRoadMapById(int id)
        {
            if (id == 0) return null;

            var result = (from x in _mspRoadMapRepository.Table
                          where x.Id == id
                          select x).FirstOrDefault();

            return result;
        }

        public virtual void UpdateSeq(int[] Id) //wailiang 20190109 MDT-149
        {
            var arrayId = Id;

            List<MSP_RoadMap> roadmap = new List<MSP_RoadMap>();

            //update all the records in MSP_roadMap, set seq = NULL, IsActive = false
            if (Id != null)
            {
                var seq = 1;
                var setnull = (from data in _mspRoadMapRepository.Table
                               select data).ToList();

                if (setnull.Count > 0)
                {
                    foreach (var items in setnull)
                    {
                        items.Seq = null;
                        items.IsActive = false;
                        roadmap.Add(items);
                    }

                    _mspRoadMapRepository.Update(roadmap);
                }

                //passing Id to change seq number
                foreach (var items in arrayId)
                {
                    var listId = Convert.ToInt32(items);

                    var result = _mspRoadMapRepository.Table.Where(x => x.Id == listId).FirstOrDefault();

                    if (result != null) //wailiang 20190111 MSP-660
                    {
                        result.IsActive = true;
                        result.Seq = Convert.ToByte(seq++);
                    }
                    
                    roadmap.Add(result);
                }

                if (roadmap.Count > 0) _mspRoadMapRepository.Update(roadmap);
            }
        }

        /// <summary>
        /// Inserts a new road map content
        /// </summary>
        /// <param name="roadmap"></param>
        public virtual void InsertRoadMapContent(MSP_RoadMap roadmap)
        {
            if (roadmap == null)
                throw new ArgumentNullException(nameof(roadmap));

            _mspRoadMapRepository.Insert(roadmap);

            //event notification
            _eventPublisher.EntityInserted(roadmap);
        }

        /// <summary>
        /// Updates an roadmap content
        /// </summary>
        /// <param name="roadmap"></param>
        public virtual void UpdateRoadMapContent(MSP_RoadMap roadmap)
        {
            if (roadmap == null)
                throw new ArgumentNullException(nameof(roadmap));

            _mspRoadMapRepository.Update(roadmap);

            //event notification
            _eventPublisher.EntityUpdated(roadmap);
        }

        /// <summary>
        /// Deletes an roadmap content
        /// </summary>
        /// <param name="roadmap"></param>
        public virtual void DeleteRoadMapContent(MSP_RoadMap roadmap)
        {
            if (roadmap == null)
                throw new ArgumentNullException(nameof(roadmap));

            _mspRoadMapRepository.Delete(roadmap);

            //event notification
            _eventPublisher.EntityDeleted(roadmap);
        }
        //wailiang 20181231 MDT-149 /\
        #endregion
    }
}
