using System.ComponentModel.DataAnnotations.Schema;

namespace AguaDeMaria.Model
{
    [Table("UserAuth")]
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public bool IsNew
        {
            get { return Id == 0; }
        }

        [NotMapped]
        public string CurrentPassword { get; set; }
        [NotMapped]
        public string NewPassword { get; set; }
        [NotMapped]
        public string ConfirmNewPassword { get; set; }

        public bool PasswordChanged
        {
            get { return !string.IsNullOrEmpty(CurrentPassword) && !string.IsNullOrEmpty(NewPassword); }
        }

        public bool PasswordMatch
        {
            get { return !string.IsNullOrEmpty(NewPassword) && NewPassword == ConfirmNewPassword; }
        }


    }
}