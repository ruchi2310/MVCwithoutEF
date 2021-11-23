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

namespace MVCwithoutEF.Controllers
{
    public class BookController : Controller

    { static string conStr =  "Server=(local)\\sqlexpress;Database=BookDB;Trusted_Connection=True;MultipleActiveResultSets=true";

        /* private IConfiguartion _configuration;

        public BookController(IConfiguartion configuartion)
        {
           this._configuration = configuartion;
        }
*/
        // GET: Book
        public IActionResult Index()
        {

            DataTable dtbl = new DataTable();
            using (SqlConnection sqlConnection = new SqlConnection(conStr))
            {
                sqlConnection.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("Book", sqlConnection);
                sqlDa.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;


            }
            return View();
        }

        // GET: Book/Details/5

        // GET: Book/AddorEdit/5
        public IActionResult AddorEdit(int? id)
        {

            BookViewModel bookViewModel = new BookViewModel();
            return View(bookViewModel);
        }

        // POST: Book/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddorEdit(int id, [Bind("Bookid,VIEWe,Author,Price")] BookViewModel bookViewModel)
        {

            if (ModelState.IsValid)
            {
                using (SqlConnection sqlConnection = new SqlConnection(conStr))
                {


                    sqlConnection.Open();
                    SqlCommand sqlCmd = new SqlCommand("BookAddorEdit", sqlConnection);
                    sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("Bookid", bookViewModel.Bookid);
                    sqlCmd.Parameters.AddWithValue("Title", bookViewModel.Title);
                    sqlCmd.Parameters.AddWithValue("Author", bookViewModel.Author);
                    sqlCmd.Parameters.AddWithValue("Price", bookViewModel.Price);
                    sqlCmd.ExecuteNonQuery();


                }
                return RedirectToAction(nameof(Index));
            }
            return View(bookViewModel);
        }

        // GET: Book/Delete/5
        public IActionResult Delete(int? id)
        {




            return View();
        }

        // POST: Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {

            return RedirectToAction(nameof(Index));
        }

    }

    public interface IConfiguartion
    {
        string GetConnectionString(string v);
    }
}

