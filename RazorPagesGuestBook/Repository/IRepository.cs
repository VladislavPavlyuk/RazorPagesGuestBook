using Microsoft.AspNetCore.Mvc;
using RazorPagesGuestBook.Data;

namespace RazorPagesGuestBook.Repository
{
    public interface IRepository
    { 
        Task<List<Message>> GetMessageList();
        Task Create(Message mes);
        Task Save();

    }
}
