using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library_Management_System.Data.Model;


namespace Library_Management_System.Data.Interfaces
{
    public interface IAuthorRepository : IRepository<Author>
    {
        Task<IEnumerable<Author>> GetAllWithBooks();
        Task<Author> GetWithBooks(int id);
    }
}
