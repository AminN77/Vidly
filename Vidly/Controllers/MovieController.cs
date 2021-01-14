using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MovieController : Controller
    {
        private readonly VidlyDataBaseContext _context;

        public MovieController()
        {
            _context = new VidlyDataBaseContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Movie/Random
        public ActionResult Random()
        {
            var movie = new Movie()
            {
                Name = "Ice Age"
            };
            var customers = new List<Customer>
            {
                new Customer {Name ="Customer1"},
                new Customer {Name ="Customer2"},
            };
            var viewModel = new RandomMovieViewModel
            {
                Movie = movie,
                Customers = customers
            };
            
            //We also can use viewbag/viewdata instead of model but its not as efficient as model
            
            return View(viewModel);
        }

        public ActionResult Edit(int movieId)
        {
            return Content("movieId=" + movieId);
        }
       
        public ActionResult Index()
        {
            var movies = _context.Movies.Include(m => m.Genre).ToList();

            return View(movies);

        }

        [Route("movie/released/{year}/{month:regex(\\d{2}):range(1,12)}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(year + "/" + month);
        }

        [Route("movie/details/{id}")]
        public ActionResult Details(int id)
        {
            Movie movie;

            try
            {
                movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);

                if (movie != null)
                    return View(movie);
            }
            catch (Exception exp)
            {

                Console.WriteLine($"exception occured : {exp}");
            }

            return HttpNotFound();
        }
    }
}