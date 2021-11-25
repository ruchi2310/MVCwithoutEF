using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MVCwithoutEF.Data;
using MVCwithoutEF.Models;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace MVCwithoutEF.Controllers
{
    public class BookController : Controller

    { 
        private readonly string _connectionString;

         private readonly IConfiguration _configuration;

        public BookController(IConfiguration configuartion)
        {
             _configuration = configuartion;
            _connectionString = _configuration.GetConnectionString("DevConnection");
        }


        public IActionResult Index()
        {
            var dataAccess = new DataAccess(_connectionString);
            var bookList = dataAccess.GetBooks();
            return View(bookList);
        }

        public IActionResult AddorEdit(int? id)
        {

            var bookViewModel = new Book();
          
            
                return View(bookViewModel);
            
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddorEdit(int id, [Bind("Bookid,VIEW,Author,Price")] Book book)
        {

            if (ModelState.IsValid)
            {
                var dataAccess = new DataAccess(_connectionString);
                dataAccess.AddOrEditBook(book);
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {



            var bookViewModel = new Book();
            return View(bookViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // Implement delete functionality here
            var dataAccess = new DataAccess(_connectionString);
            dataAccess.DeleteByid(id);
            return RedirectToAction(nameof(Index));
        }
       
    }

}

