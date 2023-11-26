using System;

namespace Nop.Core.Domain.Residential.User
{
    public class Mnt_UserProfile : BaseEntity
    {
        public Mnt_UserProfile()
        {
            Locale_Id = 1;
        }

        /// <summary>
        /// Gets or sets the CustomerId
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the Sub
        /// </summary>
        public string Sub { get; set; }

        /// <summary>
        /// Gets or sets the Picture
        /// </summary>
        public string Picture { get; set; }

        /// <summary>
        /// Gets or sets the Preferred_Username
        /// </summary>
        public string Preferred_Username { get; set; }

        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the FamilyName
        /// </summary>
        public string FamilyName { get; set; }

        /// <summary>
        /// Gets or sets the MiddleName
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// Gets or sets the GivenName
        /// </summary>
        public string GivenName { get; set; }

        /// <summary>
        /// Gets or sets the Bio
        /// </summary>
        public string Bio { get; set; }

        /// <summary>
        /// Gets or sets the Gender_Id
        /// </summary>
        public byte? Gender_Id { get; set; }

        /// <summary>
        /// Gets or sets the NickName
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// Gets or sets the DateOfBirth
        /// </summary>
        public string DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets the UserProp_Id
        /// </summary>
        public int? UserProp_Id { get; set; }

        /// <summary>
        /// Gets or sets the Msisdn_Id
        /// </summary>
        public int? Msisdn_Id { get; set; }

        /// <summary>
        /// Gets or sets the Email_Id
        /// </summary>
        public int? Email_Id { get; set; }

        /// <summary>
        /// Gets or sets the Device_Id
        /// </summary>
        public int? Device_Id { get; set; }

        /// <summary>
        /// Gets or sets the Locale_Id
        /// </summary>
        public int? Locale_Id { get; set; }

        /// <summary>
        /// Gets or sets the Identity_Type 
        /// </summary>
        public string Identity_Type { get; set; }
        //private UserProfile_IdentityType _IdentityTypeEnum;

        //public virtual UserProfile_IdentityType IdentityTypeEnum
        //{
        //    get { return _IdentityTypeEnum; }
        //    set { _IdentityTypeEnum = value; }
        //}
        //public string IdentityTypeDescription
        //{
        //    get { return _IdentityTypeEnum.ToDescription<UserProfile_IdentityType>(); }
        //    set
        //    {
        //        IdentityTypeEnum = EnumExtendMethod.GetEnumFromDescription<UserProfile_IdentityType>(value);
        //    }
        //}
        //public string Identity_Type
        //{
        //    get { return _IdentityTypeEnum.ToValue<UserProfile_IdentityType>(); }
        //    set
        //    {
        //        IdentityTypeEnum = EnumExtendMethod.GetEnumFromValue<UserProfile_IdentityType>(value);
        //    }
        //}

        /// <summary>
        /// Gets or sets the Identity_Number
        /// </summary>
        public string Identity_Number { get; set; }

        /// <summary>
        /// Gets or sets the PolicyAccepted
        /// </summary>
        public bool PolicyAccepted { get; set; }

        /// <summary>
        /// Gets or sets the PolicyAcceptedDateOnUtc
        /// </summary>
        public DateTime? PolicyAcceptedDateOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the NewsletterAccepted
        /// </summary>
        public bool NewsletterAccepted { get; set; }

        /// <summary>
        /// Gets or sets the NewsletterAcceptedDateOnUtc
        /// </summary>
        public DateTime? NewsletterAcceptedDateOnUtc { get; set; }
        
        /// <summary>
        /// Gets or sets the Status
        /// </summary>
        public string ZoneInfo { get; set; }

        /// <summary>
        /// Gets or sets the Status
        /// </summary>
        public string Citizen { get; set; }

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
