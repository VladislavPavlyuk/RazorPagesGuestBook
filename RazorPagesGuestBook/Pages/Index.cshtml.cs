using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesGuestBook.Data;

namespace RazorPagesGuestBook.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }
        public IList<Message> Message { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Messages != null)
            {
                //Message = await _context.Messages.ToListAsync();
                Message = await _context.Messages
                        .Include(m => m.User) // Eagerly load the User navigation property
                        .OrderByDescending(m => m.CreatedAt) // Order messages by CreatedAt
                        .ToListAsync();
            }
        }

        public async Task OnGetByAge()
        {
            if (_context.Messages != null)
            {
                Message = await _context.Messages.OrderBy(s => s.CreatedAt).ToListAsync();
            }
        }
        /*private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        private readonly IEnumerable<Message> _messages;

        public IndexModel(IEnumerable<Message> messages)
        {
            _messages = messages;
        }

        public void OnGet()
        {
            foreach (var message in _messages)
            {
                // Process messages
            }

        }*/
    }
}
