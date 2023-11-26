using FluentValidation;
using Nop.Services.Localization;
using Nop.Web.Framework.Validators;
using Nop.Web.Areas.Admin.Models.Announcements;
using Nop.Data;
using Nop.Core.Domain.Msp.Setting;

namespace Nop.Web.Areas.Admin.Validators.Announcements
{
    public partial class GrcAnnouncementsValidator : BaseNopValidator<GrcAnnouncementsModel>
    {
        public GrcAnnouncementsValidator(ILocalizationService localizationService, IDbContext dbContext)
        {
            RuleFor(x => x.Title_EN).NotEmpty()
                .WithMessage(localizationService.GetResource("Announcement.ContentTitle.Required"));
			RuleFor(x => x.Title_EN).Length(1, 50)
				.WithMessage(string.Format(localizationService.GetResource("Announcement.ContentTitle.MaxLengthValidation"), 50));
			RuleFor(x => x.ShortDescription_EN).NotEmpty()
				.WithMessage(localizationService.GetResource("Announcement.ShortDescription.Required"));
			RuleFor(x => x.ShortDescription_EN).Length(1, 100)
                .WithMessage(string.Format(localizationService.GetResource("Announcement.ShortDescription.MaxLengthValidation"), 100));
			RuleFor(x => x.Content1_EN).NotEmpty()
                .WithMessage(localizationService.GetResource("Announcement.Content.Required"));

			RuleFor(x => x.Title_CN).NotEmpty()
				.WithMessage(localizationService.GetResource("Announcement.ContentTitleChinese.Required"));
			RuleFor(x => x.Title_CN).Length(1, 50)
				.WithMessage(string.Format(localizationService.GetResource("Announcement.ContentTitleChinese.MaxLengthValidation"), 50));
			RuleFor(x => x.ShortDescription_CN).NotEmpty()
				.WithMessage(localizationService.GetResource("Announcement.ShortDescriptionChinese.Required"));
			RuleFor(x => x.ShortDescription_CN).Length(1, 100)
				.WithMessage(string.Format(localizationService.GetResource("Announcement.ShortDescriptionChinese.MaxLengthValidation"), 100));
			RuleFor(x => x.Content1_CN).NotEmpty()
				.WithMessage(localizationService.GetResource("Announcement.ContentChinese.Required"));

			//validate end date must greater than or equal to start date when start date not equal to empty
			RuleFor(x => x.EndDate).GreaterThanOrEqualTo(x => x.StartDate)
                .When(x => x.StartDate != null);

            SetDatabaseValidationRules<MSP_GrcAnnouncement>(dbContext);
        }
    }
}