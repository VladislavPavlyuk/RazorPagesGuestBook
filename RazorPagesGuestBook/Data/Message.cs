using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace RazorPagesGuestBook.Data
{
    public class Message
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Content is required.")]
        [StringLength(500, ErrorMessage = "Content cannot exceed 500 characters.")]
        public string? Content { get; set; }

        [DataType(DataType.Date)] 
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Связь с AspNetUsers  
        [Required]
        public string? UserId { get; set; }

        public virtual IdentityUser? User { get; set; }

        [ActivatorUtilitiesConstructor] 
        public Message() { } // A parameterless public constructor

    }

}
