using Nop.Core.Domain.Customers;

namespace Nop.Data.Mapping.Customers
{
    /// <summary>
    /// Mapping class
    /// </summary>
    public partial class CustomerTransactionPasswordMap : NopEntityTypeConfiguration<CustomerTransactionPassword>
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public CustomerTransactionPasswordMap()
        {
            this.ToTable("CustomerTransactionPassword");
            this.HasKey(password => password.Id);

            this.HasRequired(password => password.Customer)
                .WithMany()
                .HasForeignKey(password => password.CustomerId);

            this.Ignore(password => password.PasswordFormat);
        }
    }
}