using Library_Management_System.Data.Model;
using Library_Management_System.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library_Management_System.ViewModels;

namespace Library_Management_System.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorController(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }
        [Route("Author")]
        public async Task<IActionResult> List()
        {
            var authors = await _authorRepository.GetAllWithBooks();
            if (authors.Count() == 0) 
            {

                return View("Empty");
            
            }
            return View(authors);

        }
        public IActionResult Update(int id) 
        {
            var author = _authorRepository.GetById(id);
            if (author == null)
            {
                return NotFound();
            }
            else return View(author);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Author  author) 
        {
            if (!ModelState.IsValid)
            {
                return View(author);
            }
            _authorRepository.Update(author);
            return RedirectToAction("List");


        }
        public IActionResult Create()
        {
            var viewModel = new CreateAuthorViewModel
            { Referer = Request.Headers["Referer"].ToString() };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult create(CreateAuthorViewModel authorVM)
        {
            if (!ModelState.IsValid)
            {
                return View(authorVM);
            }
            _authorRepository.Create(authorVM.Author);
            if (!string.IsNullOrEmpty(authorVM.Referer)) 
            {

                return Redirect(authorVM.Referer);
            }

            return RedirectToAction("List");


        }
        public async Task<IActionResult> Delete(int id) 
        {
            var author = await _authorRepository.GetById(id);
            await _authorRepository.Delete(author);
            return RedirectToAction("List");
        }



    }
}
