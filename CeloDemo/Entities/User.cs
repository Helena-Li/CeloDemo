using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CeloDemo.Entities
{
    public class User
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
        [Required]
        public string Title { get; set; }
        [Required]
        public DateTime Birth { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string LargePicture { get; set; }
        [Required]
        public string ThumbnailPicture { get; set; }
    }
}
