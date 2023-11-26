using FluentValidation;
using Nop.Services.Localization;
using Nop.Web.Framework.Validators;
using Nop.Data;
using Nop.Core.Domain.Msp.Setting;
using Nop.Web.Areas.Admin.Models.UserGuides;

namespace Nop.Web.Areas.Admin.Validators.UserGuides
{
    public partial class UserGuidesValidator : BaseNopValidator<UserGuidesModel>
    {
        public UserGuidesValidator(ILocalizationService localizationService, IDbContext dbContext)
        {
            //validate content title
            RuleFor(x => x.ContentTitle).NotEmpty()
                .WithMessage(localizationService.GetResource("UserGuides.ContentTitle.Required"));

            //validate content title length [1 to 200] characters
            RuleFor(x => x.ContentTitle).Length(1, 200)
                .WithMessage(string.Format(localizationService.GetResource("UserGuides.ContentTitle.MaxLengthValidation"), 200));
            
            //validate content
            RuleFor(x => x.Content).NotEmpty()
                .WithMessage(localizationService.GetResource("UserGuides.Content.Required"));

            //validate end date must greater than or equal to start date when start date not equal to empty
            RuleFor(x => x.EndDate).GreaterThanOrEqualTo(x => x.StartDate)
                .When(x => x.StartDate != null);

            SetDatabaseValidationRules<MSP_Announce_Content>(dbContext);
        }
    }
}