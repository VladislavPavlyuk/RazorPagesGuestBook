using Microsoft.EntityFrameworkCore;
using RazorPagesGuestBook.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;

namespace RazorPagesGuestBook.Repository
{
    public class Repository : IRepository
    {
        private readonly AppDbContext _context;
        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Message>> GetMessageList()
        {
            return await _context.Messages.Include(p => p.User).ToListAsync();

        }
        public async Task Create(Message mes)
        {
            await _context.Messages.AddAsync(mes);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
