using FluentValidation;
using Nop.Core.Domain.Msp.Security;
using Nop.Data;
using Nop.Services.Localization;
using Nop.Web.Areas.Admin.Models.Security;
using Nop.Web.Framework.Validators;

namespace Nop.Web.Areas.Admin.Validators.Security
{
    public partial class SecurityValidator : BaseNopValidator<SecurityQuestionViewModel>
    {
        public SecurityValidator(ILocalizationService localizationService, IDbContext dbContext)
        {
            //validate content title
            RuleFor(x => x.Question).NotEmpty()
                .WithMessage(localizationService.GetResource("SecurityQuestions.Question.Required"));

            //validate content title length [1 to 200] characters
            RuleFor(x => x.Question).Length(1, 200)
                .WithMessage(string.Format(localizationService.GetResource("SecurityQuestions.Question.MaxLengthValidation"), 200));

            //SetDatabaseValidationRules<MSP_SecurityQuestion>(dbContext); //Atiqah 20190131 MDT-205
        }
    }
}