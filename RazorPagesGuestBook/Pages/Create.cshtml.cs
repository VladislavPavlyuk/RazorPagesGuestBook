using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorPagesGuestBook.Data;

namespace RazorPagesGuestBook.Pages
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _context;

        public CreateModel(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            var users = _context.Users.ToList(); // Fetch all users
            ViewData["ListUsers"] = new SelectList(users, "Id", "UserName"); // Use ViewData instead of ViewBag
            return Page();
        }

        [BindProperty]
        public Message Message { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            var userId = User?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                ModelState.AddModelError(string.Empty, "Unable to determine the current user.");
                return Page();
            }

            Message.UserId = userId;

            // Explicitly load the User entity
            //Message.User = await _context.Users.FindAsync(userId) ?? throw new InvalidOperationException("User not found.");
            Message.User = await _context.Users.FindAsync(Message.UserId)
                            ?? throw new InvalidOperationException("User not found.");

            if (Message.User == null)
            {
                ModelState.AddModelError(string.Empty, "The user could not be found.");
                return Page();
            }

            // Remove Message.User from ModelState validation
            ModelState.Remove("Message.UserId");

            if (!ModelState.IsValid || _context.Messages == null || Message == null)
            {
                // Reload the user list in case of validation errors
                ViewData["ListUsers"] = new SelectList(_context.Users.ToList(), "Id", "UserName");
                return Page();
            }
            _context.Messages.Add(Message);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
