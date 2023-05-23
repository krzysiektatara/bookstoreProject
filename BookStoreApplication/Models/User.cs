using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BookStoreApplicationAPI.Models;

namespace BookStoreApplication.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Role_Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        [ForeignKey("User_Id")]
        public List<BookingEntity> Products { get; set; }
        
    }
}
