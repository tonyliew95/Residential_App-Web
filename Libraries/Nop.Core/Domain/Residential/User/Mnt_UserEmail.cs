using System;

namespace Nop.Core.Domain.Residential.User
{
    public class Mnt_UserEmail : BaseEntity
    {
        /// <summary>
        /// ctor
        /// </summary>
        public Mnt_UserEmail()
        {
            IsVerified = false;
            Type = "";
            UpdatedOnUtc = DateTime.UtcNow;
        }

        /// <summary>
        /// Gets or sets the CustomerId
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the Type
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the IsVerified
        /// </summary>
        public bool IsVerified { get; set; }

        /// <summary>
        /// Gets or sets the CreatedBy
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the CreatedOnUtc
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the UpdatedBy
        /// </summary>
        public int UpdatedBy { get; set; }


        /// <summary>
        /// Gets or sets the UpdatedOnUtc
        /// </summary>
        public DateTime UpdatedOnUtc { get; set; }
    }
}
