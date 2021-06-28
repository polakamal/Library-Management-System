using Library_Management_System.Data.Interfaces;
using Library_Management_System.Data.Model;
using Library_Management_System.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library_Management_System.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;

        public BookController(IBookRepository  bookRepository,IAuthorRepository authorRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
        }
        [Route("Book")]
        public async Task<IActionResult> List(int? authorId, int? borrowerId)
        {

            if (authorId == null && borrowerId == null)
            {
                var books = await _bookRepository.GetAllWithAuthor();
                return CheckBooks(books);

            }
            else if (authorId != null)
            {
                var author = await _authorRepository.GetWithBooks((int)authorId);

                if (author.Books.Count() == 0)
                {
                    return View("AuthorEmpty", author);
                }
                else
                {
                    return View(author.Books);
                }

            }
        
        else if (borrowerId != null)
            {
                var books = await _bookRepository.FindWithAuthorAndBorrower(book=>book.BorrowerId==borrowerId);
                return CheckBooks(books);
            }
        else 
            {
                throw new ArgumentException();


            }

        
        }

        public IActionResult CheckBooks(IEnumerable<Book> books) 
        {

            if (books.Count() == 0)
            {

                return View("Empty");

            }
            else
            {
             return   View(books);
            }

        }

        public async Task<IActionResult> Create() 
        {
            var BookVM = new BookViewModel()
            {

                Authors = await _authorRepository.GetAll(),

            
            };
            return View(BookVM);
        
        }
        [HttpPost]
        public async Task<IActionResult> Create(BookViewModel bookViewModel) 
        {

            if (!ModelState.IsValid)
            {
                bookViewModel.Authors = await _authorRepository.GetAll();
                return View(bookViewModel);
            }

            await _bookRepository.Create(bookViewModel.Book);
           return  RedirectToAction("List");
            
        }
        public async Task<IActionResult> Update(int id)
        {
            var BookVM = new BookViewModel()
            {  

                Book =    await _bookRepository.GetById(id),
                Authors = await _authorRepository.GetAll()


            };
            return View(BookVM);

        }
        [HttpPost]
        public  async Task<IActionResult> Update(BookViewModel bookViewModel)
        {
            if (!ModelState.IsValid)
            {
                bookViewModel.Authors = await _authorRepository.GetAll();
                return View(bookViewModel);
            }
 
            await _bookRepository.Update(bookViewModel.Book);
            return RedirectToAction("List");

        }
        public async Task<IActionResult> Delete(int id) 
        {
            var book = await _bookRepository.GetById(id);

            await _bookRepository.Delete(book);

            return RedirectToAction("List");

        }
    }
}
