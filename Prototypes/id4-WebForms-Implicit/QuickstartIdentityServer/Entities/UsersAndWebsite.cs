using System;

namespace QuickstartIdentityServer.Entities
{
    public partial class UsersAndWebsite
    {
        public int Id { get; set; }

        public int? UserId { get; set; }

        public int? WebsiteId { get; set; }

        public int? RoleId { get; set; }

        public bool Enabled { get; set; }

        public DateTime? ConfirmDate { get; set; }

        public Users User { get; set; }
    }
}
