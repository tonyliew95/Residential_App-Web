using Newtonsoft.Json;

//Tony Liew 20190306 RDT-116 \/
namespace Nop.Plugin.Api.Models.Base
{
	/// <summary>
	/// Filter parameters for paged list
	/// </summary>
	public abstract class ApiFilterParamPagination 
	{
		/// <summary>
		/// Ctor
		/// </summary>
		public ApiFilterParamPagination()
		{
            pageNum = 1;
            pageSize = 20;  
        }

		/// <summary>
		/// Gets or sets the Page Number
		/// </summary>
		[JsonProperty("pageNum")]
		public int pageNum { get; set; }

		/// <summary>
		/// Gets or sets the Total Page Size
		/// </summary>
		[JsonProperty("pageSize")]
		public int pageSize { get; set; }
	}
}
//Tony Liew 20190306 RDT-116 /\