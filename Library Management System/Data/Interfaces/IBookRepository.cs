using Library_Management_System.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Library_Management_System.Data.Interfaces
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<IEnumerable<Book>> GetAllWithAuthor();
        Task<IEnumerable<Book>> FindWithAuthor(Expression<Func<Book, bool>> predicate);
        Task<IEnumerable<Book>> FindWithAuthorAndBorrower(Expression<Func<Book, bool>> predicate);
    }
}
