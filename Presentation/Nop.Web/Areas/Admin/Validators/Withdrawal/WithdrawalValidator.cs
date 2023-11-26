using FluentValidation;
using Nop.Core.Domain.Msp.Custom;
using Nop.Core.Domain.Msp.Setting;
using Nop.Data;
using Nop.Services.Localization;
using Nop.Web.Areas.Admin.Models.Transaction;
using Nop.Web.Framework.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Areas.Admin.Validators.Withdrawal
{
    public partial class WithdrawalValidator : BaseNopValidator<WithdrawalModel> //wailiang 20181031 MSP-423
    {
        public WithdrawalValidator (ILocalizationService localizationService, IDbContext dbContext)
        {
            //validate end date must greater than or equal to start date when start date not equal to empty
            RuleFor(x => x.Date).GreaterThanOrEqualTo(x => x.Date)
                .When(x => x.Date != null);

            SetDatabaseValidationRules<WithdrawalTransactionCustom>(dbContext);
        }
    }
}
