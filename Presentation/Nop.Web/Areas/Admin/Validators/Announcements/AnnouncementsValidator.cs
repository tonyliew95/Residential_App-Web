using FluentValidation;
using Nop.Services.Localization;
using Nop.Web.Framework.Validators;
using Nop.Web.Areas.Admin.Models.Announcements;
using Nop.Data;
using Nop.Core.Domain.Msp.Setting;

namespace Nop.Web.Areas.Admin.Validators.Announcements
{
    public partial class AnnouncementsValidator : BaseNopValidator<AnnouncementsModel>
    {
        public AnnouncementsValidator(ILocalizationService localizationService, IDbContext dbContext)
        {
            //validate content title
            RuleFor(x => x.ContentTitle).NotEmpty()
                .WithMessage(localizationService.GetResource("Announcement.ContentTitle.Required"));

            //validate content title length [1 to 200] characters
            RuleFor(x => x.ContentTitle).Length(1, 200)
                .WithMessage(string.Format(localizationService.GetResource("Announcement.ContentTitle.MaxLengthValidation"), 200));
            
            //validate content
            RuleFor(x => x.Content).NotEmpty()
                .WithMessage(localizationService.GetResource("Announcement.Content.Required"));

            //Tony Liew 20181130 MDT-114 \/
            //validate end date must greater than or equal to start date when start date not equal to empty
            RuleFor(x => x.EndDate).GreaterThanOrEqualTo(x => x.StartDate)
                .When(x => x.StartDate != null);

            #region Chew 20181214 MDT-131 Commented Code
            ////validate shut down end date must greater than or equal to shut down start date when shut down start date not equal to empty
            //RuleFor(x => x.ShutDownEndDate).GreaterThanOrEqualTo(x => x.ShutDownStartDate)
            //    .When(x => x.ShutDownStartDate != null);

            ////validate start date must greater than or equal to shut down start date when shut down start date not equal to empty
            //RuleFor(x => x.ShutDownStartDate).GreaterThanOrEqualTo(x => x.StartDate)
            //    .When(x => x.ShutDownStartDate != null);
            #endregion

            //validate shutdown start date is required
            //RuleFor(x => x.ShutDownStartDate).NotEmpty()
            //    .WithMessage(localizationService.GetResource("Announcement.ShutDownStartOnUtc.Required"));

            ////validate shutdown end date is required
            //RuleFor(x => x.ShutDownEndDate).NotEmpty()
            //    .WithMessage(localizationService.GetResource("Announcement.ShutDownEndOnUtc.Required"));
            //Tony Liew 20181130 MDT-114 /\
            SetDatabaseValidationRules<MSP_Announce_Content>(dbContext);
        }
    }
}