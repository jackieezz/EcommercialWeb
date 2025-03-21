using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace ECommerceWebsite.Models
{
    public class UserViewModel
    {
        public ObjectId Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set;}

        [Required]
        public string Password { get; set; }
    }
}
