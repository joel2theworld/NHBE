using Microsoft.AspNetCore.Identity;

namespace NeighbourhoodHelp.Model.Entities
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PostalCode { get; set; } = String.Empty;
        public string? Image { get; set; }
        public string Street { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;

        public string Otp { get; set; } = string.Empty;

       // public string ErrandId { get; set; }
       
        public IList<Errand> Errands { get; set; }
    }
}