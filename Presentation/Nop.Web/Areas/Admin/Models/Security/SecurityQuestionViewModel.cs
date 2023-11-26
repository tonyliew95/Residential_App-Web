using FluentValidation.Attributes;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Areas.Admin.Validators.Security;
using Nop.Web.Framework.Localization;
using Nop.Web.Framework.Mvc.ModelBinding;
using Nop.Web.Framework.Mvc.Models;
using System.Collections.Generic;

namespace Nop.Web.Areas.Admin.Models.Security
{
    [Validator(typeof(SecurityValidator))]
    public partial class SecurityQuestionViewModel : BaseNopModel, ILocalizedModel<SecurityQuestionViewLocalizedModel>//Tony Liew 20180828 MSP-47
    {
        public SecurityQuestionViewModel()
        {
            Locales = new List<SecurityQuestionViewLocalizedModel>();
            AvailableStatusOptions = new List<SelectListItem>();
        }
        /// <summary>
        /// Question's ID
        /// </summary>
        [NopResourceDisplayName("Question ID")]
        public int QuestionID { get; set; }

        /// <summary>
        /// Question
        /// </summary> 
        [NopResourceDisplayName("Question")]
        public string Question { get; set; }

        /// <summary>
        /// Current Status for Questions
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Tickbox for Status. Active = true ; Inactive = false
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Index for list page
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Questions status value in dropdown list
        /// </summary>
        [NopResourceDisplayName("Question Status")]
        public string SearchStatusID { get; set; }

        /// <summary>
        /// Questions status dropdown list display
        /// </summary>
        [NopResourceDisplayName("Question Status")]
        public IList<SelectListItem> AvailableStatusOptions { get; set; }

        public IList<SecurityQuestionViewLocalizedModel> Locales { get; set; }
    }

    public partial class SecurityQuestionViewLocalizedModel : ILocalizedModelLocal //Tony Liew 20180828 MSP-47
    {
        public int LanguageId { get; set; }

        [NopResourceDisplayName("Question")]
        public string Question { get; set; }

    }



}
