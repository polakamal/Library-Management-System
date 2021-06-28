using Library_Management_System.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library_Management_System.Controllers
{


  
     
    public class ReturnController : Controller
    {


        private readonly IBookRepository _bookRepository;
        private readonly ICustomerRepository _customerRepository;

        public ReturnController(IBookRepository bookRepository, ICustomerRepository customerRepository)
        {
            _bookRepository = bookRepository;
            _customerRepository = customerRepository;
        }
        [Route("Return")]
        public async Task<IActionResult> List()
        {
            var borrowedBooks = await _bookRepository.FindWithAuthorAndBorrower(x=>x.BorrowerId !=0);
            if (borrowedBooks == null || borrowedBooks.ToList().Count() == 0)
            {
                return View("Empty");
            }
            return View(borrowedBooks);
        }
        public async Task<IActionResult> ReturnABook(int bookId) 
        {
            var book = await _bookRepository.GetById(bookId);
            book.Borrower = null;
            book.BorrowerId = 0;
            await _bookRepository.Update(book);
            return RedirectToAction("List");
        }


        }
    }
