using Library_Management_System.Data.Interfaces;
using Library_Management_System.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library_Management_System.Controllers
{
    public class LendController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly ICustomerRepository _customerRepository;
        
        public LendController(IBookRepository bookRepository,ICustomerRepository customerRepository)
        {
            _bookRepository = bookRepository;
            _customerRepository = customerRepository;
        }
        [Route("Lend")]
        public IActionResult List() 
        {
            var availablebooks = _bookRepository.FindWithAuthor(x=>x.BorrowerId==0);
            if (availablebooks.Count() == 0) 
            {
                return View("Empty");


            }
            else 
            {
                return View(availablebooks);
            }
        
        }
       

        public IActionResult LendBook(int BookId) 
        {
            var LendVM = new LendViewModel() 
            {
            Book = _bookRepository.GetById(BookId),
            customers= _customerRepository.GetAll()
            
            
            };
            return View(LendVM);
        }
        [HttpPost]
        public IActionResult LendBook(LendViewModel lendViewModel) 
        {
            var book = _bookRepository.GetById(lendViewModel.Book.BookId);
            var customer = _customerRepository.GetById(lendViewModel.Book.BorrowerId);
            book.Borrower = customer;
            _bookRepository.Update(book);
            return RedirectToAction("List");
        }
    }
}
