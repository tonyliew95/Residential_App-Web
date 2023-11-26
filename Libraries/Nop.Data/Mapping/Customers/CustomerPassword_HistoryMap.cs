using Nop.Core.Domain.Customers;

namespace Nop.Data.Mapping.Customers
{
    /// <summary>
    /// Mapping class
    /// </summary>
    public partial class CustomerPassword_HistoryMap : NopEntityTypeConfiguration<CustomerPassword_History>
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public CustomerPassword_HistoryMap()
        {
            this.ToTable("CustomerPassword_History");
            this.HasKey(password => password.Id);

            this.HasRequired(password => password.Customer)
                .WithMany()
                .HasForeignKey(password => password.CustomerId);

            this.Ignore(password => password.PasswordFormat);
        }
    }
}