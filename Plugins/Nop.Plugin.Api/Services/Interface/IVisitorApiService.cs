using Nop.Plugin.Api.Models.Visitor.DTOs;
using Nop.Plugin.Api.Models.Visitor.Request;
using Nop.Plugin.Api.Models.Visitor.ResponseResults;

namespace Nop.Plugin.Api.Services.Interfaces
{
    /// <summary>
    /// Interface
    /// </summary>
    public interface IVisitorApiService
    {
        //WKK 20190411 RDT-192 [API] Visitor - Record History Details
        /// <summary>
        /// Get Record History Details
        /// </summary>
        /// <param name="visitId"></param>
        /// <returns></returns>
        HistoryDetailsDto GetHistoryDetails(int visitId);

        //WKK 20190411 RDT-189 [API] Visitor - Record History Listing
        /// <summary>
        /// Get Record History Listing
        /// </summary>
        /// <param name="request"></param>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        HistoryList_ResponseResult GetHistoryListing(HistoryList_Request request, int CustomerId);

        // WKK 20190415 RDT-190 [API] Visitor - Request Submission
        /// <summary>
        /// Visitor - Request Submission
        /// </summary>
        /// <param name="request"></param>
        /// <param name="customerId">customer id</param>
        /// <returns></returns>
        int VisitorRequestSubmission(VisitorSubmission_Request request, int customerId);

        // WKK 20190416 RDT-197 [API] Visitor - Clock In/Out
        /// <summary>
        /// Visitor - Clock In/Out
        /// </summary>
        /// <param name="trxId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        void VisitorTimeClock(int trxId, string type);

        //WKK 20190417 RDT-193 [API] Visitor - Favourite Listing
        /// <summary>
        /// Get User's visitor favourite list
        /// </summary>
        /// <param name="request"></param>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        FavouriteList_ResponseResult GetFavouriteVisitorList(FavouriteList_Request request, int CustomerId);

        // WKK 20190417 RDT-195 [API] Visitor - Favourite Add
        /// <summary>
        /// Visitor - Favourite Add
        /// </summary>
        /// <param name="request"></param>
        /// <param name="customerId">customer id</param>
        /// <returns></returns>
        void FavouriteVisitorAdd(FavouriteVisitor_Request request, int customerId);

        // WKK 20190418 RDT-196 [API] Visitor - Favourite Remove
        /// <summary>
        /// Visitor - Favourite Remove
        /// </summary>
        /// <param name="request"></param>
        /// <param name="customerId">customer id</param>
        /// <returns></returns>
        void FavouriteVisitorRemove(FavouriteVisitor_Request request, int customerId);

        /// <summary>
        /// Get Visitor Details
        /// </summary>
        /// <param name="visitorId"></param>
        /// <returns></returns>
        VisitorDto GetVisitorDetails(int visitorId);

        /// <summary>
        /// Get Visit Details
        /// </summary>
        /// <param name="visitId"></param>
        /// <returns></returns>
        VisitDto GetVisitDetails(int visitId);
    }
}
