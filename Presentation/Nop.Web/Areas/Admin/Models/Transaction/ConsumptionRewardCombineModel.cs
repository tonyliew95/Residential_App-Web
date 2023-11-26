using Nop.Web.Framework.Mvc.Models;

namespace Nop.Web.Areas.Admin.Models.Transaction
{
    public class ConsumptionRewardCombineModel : BaseNopModel
    {
        public ConsumptionRewardCombineModel() //Jerry 20181102 MSP-436
        {
            SelfListModel = new ConsumptionRewardSelfListModel();
            TeamModel = new ConsumptionRewardModel();
        }

        public ConsumptionRewardSelfListModel SelfListModel;
        public ConsumptionRewardModel TeamModel;
    }
}
