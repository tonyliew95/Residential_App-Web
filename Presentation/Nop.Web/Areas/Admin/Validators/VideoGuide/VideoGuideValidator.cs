using FluentValidation;
using Nop.Core.Domain.Msp.Setting;
using Nop.Data;
using Nop.Services.Localization;
using Nop.Web.Areas.Admin.Models.VideoGuide;
using Nop.Web.Framework.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Areas.Admin.Validators.VideoGuide
{
    public partial class VideoGuideValidator : BaseNopValidator<VideoGuideModel>
    {
        public VideoGuideValidator (ILocalizationService localizationService, IDbContext dbContext)
        {
            //validate content title
            RuleFor(x => x.ContentTitle).NotEmpty()
                .WithMessage(localizationService.GetResource("VideoGuide.ContentTitle.Required"));

            //validate content title length [1 to 200] characters
            RuleFor(x => x.ContentTitle).Length(1, 200)
                .WithMessage(string.Format(localizationService.GetResource("VideoGuide.ContentTitle.MaxLengthValidation"), 200));

            //Tony Liew 20181221 MDT-143 \/
            ////validate content
            //RuleFor(x => x.Content).NotEmpty()
            //    .WithMessage(localizationService.GetResource("VideoGuide.Content.Required"));

            //validate content url
            RuleFor(x => x.ContentUrl).NotEmpty()
                .WithMessage(localizationService.GetResource("VideoGuide.ContentUrl.Required"));

            //validate content url
            RuleFor(x => x.ContentTittleChinese).NotEmpty()
                .WithMessage(localizationService.GetResource("VideoGuide.ContentTitle.Chinese.Required"));

            ////validate content name 
            //RuleFor(x => x.ContentName).NotEmpty()
            //    .WithMessage(localizationService.GetResource("VideoGuide.ContentName.Required"));
            //Tony Liew 20181221 MDT-143 /\
            //validate end date must greater than or equal to start date when start date not equal to empty
            RuleFor(x => x.EndDate).GreaterThanOrEqualTo(x => x.StartDate)
                .When(x => x.StartDate != null);

            SetDatabaseValidationRules<MSP_Announce_Content>(dbContext);
        }
    }
}
