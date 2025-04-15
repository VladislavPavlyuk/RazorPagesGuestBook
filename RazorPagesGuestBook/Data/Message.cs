using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace RazorPagesGuestBook.Data
{
    public class Message
    {
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Связь с AspNetUsers
        [Required]
        public string UserId { get; set; }
        public virtual IdentityUser User { get; set; }
    }

}
