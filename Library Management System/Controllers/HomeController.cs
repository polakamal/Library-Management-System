using Library_Management_System.Data.Interfaces;
using Library_Management_System.Models;
using Library_Management_System.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Library_Management_System.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly ICustomerRepository _customerRepository;
        public HomeController(IBookRepository bookRepository,ICustomerRepository customerRepository,IAuthorRepository authorRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _customerRepository = customerRepository;

        }

        public async Task<IActionResult> Index()
        {
            var homeVM = new HomeViewModel()
            {
                AuthorCount = await _authorRepository.Count(x => true),
                CustomerCount = await _customerRepository.Count(x => true),
                BookCount = await _bookRepository.Count(x => true),
                LendBookCount = await _bookRepository.Count(x => x.Borrower != null)
            };
            // call view
            return View(homeVM);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
