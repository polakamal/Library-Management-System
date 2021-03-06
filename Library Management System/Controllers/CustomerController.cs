using Library_Management_System.Data.Interfaces;
using Library_Management_System.Data.Model;
using Library_Management_System.Data.Repository;
using Library_Management_System.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library_Management_System.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IBookRepository _bookRepository;

        public CustomerController(ICustomerRepository customerRepository, IBookRepository bookRepository)
        {
            _customerRepository = customerRepository;
            _bookRepository = bookRepository;
        }
        [Route("Customer")]
        public async Task<IActionResult> List()
        {
            var customerVM = new List<CustomerViewModel>();
            var customers =  await _customerRepository.GetAll();
            if (customers.Count() == 0)
            {
                return View("Empty");

            }
            foreach(var customer in customers)
            {
                customerVM.Add(new CustomerViewModel
                {
                    Customer = customer,
                    BookCount = await _bookRepository.Count(x=>x.BorrowerId==customer.CustomerId)
                }
                );
            }
            return View(customerVM);
        }
        public async Task<IActionResult> Delete(int id) 
        {
            var customer = await _customerRepository.GetById(id);
            await _customerRepository.Delete(customer);
            return RedirectToAction("List");
        
        }

        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return View(customer);
            }
            _customerRepository.Create(customer);
            
           return RedirectToAction("List");

        }
        public IActionResult Update(int id) { 
        
            var customer = _customerRepository.GetById(id);
            return View(customer);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return View(customer);
            }
            _customerRepository.Update(customer);

            return RedirectToAction("List");

        }


    }
}
