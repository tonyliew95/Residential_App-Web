using FluentValidation;
using Nop.Services.Localization;
using Nop.Web.Framework.Validators;
using Nop.Data;
using Nop.Core.Domain.Msp.Setting;
using Nop.Web.Areas.Admin.Models.Incidents;
using Nop.Core.Domain.Residential.Incident;

namespace Nop.Web.Areas.Admin.Validators.Announcements
{
    public partial class IncidentsValidator : BaseNopValidator<IncidentsModel> //wailiang 20190321 RDT-127
    {
        public IncidentsValidator(ILocalizationService localizationService, IDbContext dbContext)
        {
            //validate title
            RuleFor(x => x.Title).NotEmpty()
                .WithMessage(localizationService.GetResource("Incidents.Title.Required"));

            RuleFor(x => x.Title).Length(1, 100)
                .WithMessage(string.Format(localizationService.GetResource("Incidents.Title.MaxLengthValidation"), 100));

            //validate description length [1 to 1000] characters
            RuleFor(x => x.Desc).NotEmpty()
                .WithMessage(localizationService.GetResource("Incidents.Description.Required"));

            RuleFor(x => x.Desc).Length(1, 500)
                .WithMessage(string.Format(localizationService.GetResource("Incidents.Description.MaxLengthValidation"), 500));

            SetDatabaseValidationRules<Trx_Incident>(dbContext);
        }
    }
}