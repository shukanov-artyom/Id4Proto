using System;
using System.Collections.Generic;

namespace QuickstartIdentityServer.Entities
{
    public partial class Users
    {
        public int UserId { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string ProfilePicture { get; set; }

        public int? CategoryId { get; set; }

        public string Country { get; set; }

        public string Position { get; set; }

        public string Specialty { get; set; }

        public string Location { get; set; }

        public string Guid { get; set; }

        public string ReferrerUrl { get; set; }

        public string ReferrerUrlsession { get; set; }

        public string LandingPage { get; set; }

        public string ZipCode { get; set; }

        public string Title { get; set; }

        public string Fellowship { get; set; }

        public string Residency { get; set; }

        public string MedicalSchool { get; set; }

        public string Undergrad { get; set; }

        public bool? EmailNotification { get; set; }

        public short? PrivFirstname { get; set; }

        public short? PrivLastname { get; set; }

        public short? PrivTitle { get; set; }

        public short? PrivLocation { get; set; }

        public short? PrivPosition { get; set; }

        public short? PrivSpecialty { get; set; }

        public short? PrivFellowship { get; set; }

        public short? PrivResidency { get; set; }

        public short? PrivSchool { get; set; }

        public short? PrivUndergrad { get; set; }

        public short? PrivPhoto { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string UsernameDisplay { get; set; }

        public int? VerifiedBy { get; set; }

        public string GraduationYear { get; set; }

        public int? SignupWebsiteid { get; set; }

        public int? ResidencyStartYear { get; set; }

        public int? PracticeStartYear { get; set; }

        public bool IsNewFellowship { get; set; }

        public bool IsNewResidency { get; set; }

        public bool IsNewUndergrad { get; set; }

        public bool IsNewMedicalSchool { get; set; }

        public bool IsNewInternship { get; set; }

        public string Internship { get; set; }

        public short? PrivInternship { get; set; }

        public DateTime UpdatedAt { get; set; }

        public byte[] RowVersion { get; set; }

        public ICollection<UsersAndWebsite> UsersAndWebsite { get; set; }
    }
}
