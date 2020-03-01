using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CeloDemo.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Name cannot logger than 50 charactors")]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Title { get; set; }
        public DateTime Birth { get; set; }
        public string Phone { get; set; }
        public string LargePicture { get; set; }
        public string ThumbnailPicture { get; set; }
    }
}
