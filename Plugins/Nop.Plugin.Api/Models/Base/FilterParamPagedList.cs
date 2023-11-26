using Newtonsoft.Json;

namespace Nop.Plugin.Api.Models.Base
{
	/// <summary>
	/// Filter parameters for paged list
	/// </summary>
	public abstract class FilterParamPagedList : FilterParam
	{
		/// <summary>
		/// Ctor
		/// </summary>
		public FilterParamPagedList()
		{
			PageNum = 1;
            //PageSize = 10;  //LeeChurn 20181009 MSP-259
            PageSize = 100;   //LeeChurn 20181009 MSP-259
        }

		/// <summary>
		/// Gets or sets the Page Number
		/// </summary>
		[JsonProperty("PageNum")]
		public int PageNum { get; set; }

		/// <summary>
		/// Gets or sets the Total Page Size
		/// </summary>
		[JsonProperty("PageSize")]
		public int PageSize { get; set; }
	}
}
