using System.ComponentModel.DataAnnotations;

namespace CapitalPlacementTask.API.Models.Entities
{
    public class PersonalInformation
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [EmailAddress(ErrorMessage = "Please input a valid email address")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Please input a valid phone number")]
        public string PhoneNumber { get; set; }

        public string Nationality { get; set; }

        public string CurrentResidence { get; set; }

        public string IdNumber { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Gender { get; set; }
    }
}
