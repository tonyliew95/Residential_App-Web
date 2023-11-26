using FluentValidation;
using Nop.Services.Localization;
using Nop.Web.Framework.Validators;
using Nop.Web.Areas.Admin.Models.Promotions;
using Nop.Data;
using Nop.Core.Domain.Msp.Setting;

namespace Nop.Web.Areas.Admin.Validators.Promotions
{
    public partial class PromotionsValidator : BaseNopValidator<PromotionsModel>
    {
        public PromotionsValidator(ILocalizationService localizationService, IDbContext dbContext)
        {
            //validate content title
            RuleFor(x => x.ContentTitle).NotEmpty()
                .WithMessage(localizationService.GetResource("Promotion.ContentTitle.Required"));

            //validate content title length [1 to 200] characters
            RuleFor(x => x.ContentTitle).Length(1, 200)
                .WithMessage(string.Format(localizationService.GetResource("Promotion.ContentTitle.MaxLengthValidation"), 200));
            
            //validate content
            RuleFor(x => x.Content).NotEmpty()
                .WithMessage(localizationService.GetResource("Promotion.Content.Required"));

            //validate end date must greater than or equal to start date when start date not equal to empty
            RuleFor(x => x.EndDate).GreaterThanOrEqualTo(x => x.StartDate)
                .When(x => x.StartDate != null);

            SetDatabaseValidationRules<MSP_Announce_Content>(dbContext);
        }
    }
}