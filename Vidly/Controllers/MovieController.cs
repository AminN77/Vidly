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

        public ActionResult New()
        {
            var viewModel = new MovieFormViewModel()
            {
                Genres = _context.Genres.ToList()
            };

            return View("MovieForm",viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {
            if(!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel(movie)
                {
                    Genres = _context.Genres.ToList()
                };

                return View("MovieForm", viewModel);
            }

            if (movie.Id == 0)
            {
                movie.DateAdded = DateTime.UtcNow;
                _context.Movies.Add(movie);
            }
            else
            {
                var exMovie = _context.Movies.Single(m => m.Id == movie.Id);

                exMovie.Name = movie.Name;
                exMovie.NumberInStock = movie.NumberInStock;
                exMovie.ReleaseDate = movie.ReleaseDate;
                exMovie.GenreId = movie.GenreId;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Movie");
        }

        public ActionResult Edit(int id)
        {

            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == null)
                return HttpNotFound();

            var viewModel = new MovieFormViewModel(movie)
            {
                Genres = _context.Genres.ToList()
            };

            return View("MovieForm", viewModel);
        }
    }
}