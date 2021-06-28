using Library_Management_System.Data.Interfaces;
using Library_Management_System.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Library_Management_System.Data.Repository
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(LibraryDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Book>> FindWithAuthor(Expression<Func<Book, bool>> predicate)
        {
            return await  _context.Books.Include(a=>a.Author).Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<Book>> FindWithAuthorAndBorrower(Expression<Func<Book, bool>> predicate)
        {
            return await _context.Books.Include(a => a.Author).Include(b => b.Borrower).Where(predicate).ToListAsync();
        }

        public  async Task<IEnumerable<Book>> GetAllWithAuthor()
        {
            return await  _context.Books.Include(a => a.Author).ToListAsync();

        }
    }
}
