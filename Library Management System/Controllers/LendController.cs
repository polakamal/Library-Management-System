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
        public async Task<IActionResult> List() 
        {
            var availablebooks = await _bookRepository.FindWithAuthor(x=>x.BorrowerId==0);
            if (availablebooks.Count() == 0) 
            {
                return View("Empty");


            }
            else 
            {
                return View(availablebooks);
            }
        
        }
       

        public async Task<IActionResult> LendBook(int BookId) 
        {
            var LendVM = new LendViewModel() 
            {
            Book = await _bookRepository.GetById(BookId),
            customers= await _customerRepository.GetAll()
            
            
            };
            return View(LendVM);
        }
        [HttpPost]
        public async Task<IActionResult> LendBook(LendViewModel lendViewModel) 
        {
            var book =  await _bookRepository.GetById(lendViewModel.Book.BookId);
            var customer = await _customerRepository.GetById(lendViewModel.Book.BorrowerId);
            book.Borrower = customer;
           await _bookRepository.Update(book);
            return RedirectToAction("List");
        }
    }
}
