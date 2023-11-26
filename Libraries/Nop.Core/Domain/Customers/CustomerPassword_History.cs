using System;

namespace Nop.Core.Domain.Customers
{
    /// <summary>
    /// Represents a customer password
    /// </summary>
    public partial class CustomerPassword_History : BaseEntity
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public CustomerPassword_History()
        {
            this.PasswordFormat = PasswordFormat.Clear;
        }

        /// <summary>
        /// Gets or sets the customer identifier
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the password format identifier
        /// </summary>
        public int PasswordFormatId { get; set; }

        /// <summary>
        /// Gets or sets the password salt
        /// </summary>
        public string PasswordSalt { get; set; }

        /// <summary>
        /// Gets or sets the date and time of entity creation
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the password format
        /// </summary>
        public PasswordFormat PasswordFormat
        {
            get { return (PasswordFormat)PasswordFormatId; }
            set { this.PasswordFormatId = (int)value; }
        }

        /// <summary>
        /// Gets or sets the customer
        /// </summary>
        public virtual Customer Customer { get; set; }

        /* Transfer these 2 columns to CustomerTransactionPassword table
         * WilliamHo 20180807 12.48PM
        //20180801 Richard Wong
        /// <summary>
        /// Gets or sets the Transaction password
        /// </summary>
        public string TransactionPassword { get; set; }
        /// <summary>
        /// Gets or sets the Transaction password salt
        /// </summary>
        public string TransactionPasswordSalt { get; set; }
        //20180801 Richard Wong
        */
    }
}