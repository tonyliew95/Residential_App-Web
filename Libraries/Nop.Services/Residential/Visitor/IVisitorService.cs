using Nop.Core.Domain.Residential.Custom;
using Nop.Core.Domain.Residential.Visitor;
using System.Linq;

namespace Nop.Services.Residential.Visitor
{
    public interface IVisitorService
    {
        /// <summary>
        /// Check duplicate clock in/out
        /// </summary>
        /// <param name="trxId"></param>
        /// <param name="clockType"></param>
        /// <returns></returns>
        bool CheckDuplicateClockInOut(int trxId, string clockType);

        /// <summary>
        /// Get Trx_Visitor
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Trx_Visitor GetTrxVisitor(int id);

        //WKK 20190417 RDT-193 [API] Visitor - Favourite Listing
        /// <summary>
        /// Get User's visitor favourite list
        /// </summary>
        /// <param name="request"></param>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        IQueryable<FavouriteListCustom> GetFavouriteVisitorList(int customerId, int orgId, int propId);

    }
}
