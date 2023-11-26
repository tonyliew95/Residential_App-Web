using Newtonsoft.Json;

namespace Nop.Plugin.Api.Models.Base
{
	/// <summary>
	/// Response result for paged list
	/// </summary>
	public class ResponseResultPagedList : ResponseResult
	{
        /// <summary>
        /// Ctor
        /// </summary>
        public ResponseResultPagedList()
        {

		}

		/// <summary>
		/// Gets or sets the Pagination Record Count
		/// </summary>
		[JsonProperty("RecordCount")]
		public int RecordCount { get; set; }

		/// <summary>
		/// Gets or sets the Pagination Page Count
		/// </summary>
		[JsonProperty("PageCount")]
		public int PageCount { get; set; }

		/// <summary>
		/// Gets or sets the Pagination Page Number
		/// </summary>
		[JsonProperty("PageNum")]
		public int PageNum { get; set; }

		/// <summary>
		/// Gets or sets the Pagination Page Size
		/// </summary>
		[JsonProperty("PageSize")]
		public int PageSize { get; set; }
	}
}
