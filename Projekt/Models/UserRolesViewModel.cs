using System.Collections.Generic;

namespace Projekt.Models
{
    public class UserRolesViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public List<string> Roles { get; set; }
    }
}
