using ValueWallet.Domain.Entities;

namespace ValueWallet.Models
{
    public class BioProfileEntity : BaseEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public LoginBy LoginBy { get; set; }
    }
}
