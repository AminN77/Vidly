using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MovieController : Controller
    {
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
             var movies = new List<Movie>
            {
              new Movie { Name="GodFather" ,Id=1},
              new Movie { Name="Forrest Gump" , Id=2},
              new Movie { Name="GoodFellas" , Id=3}
            };

            return View(movies);

        }

        [Route("movie/released/{year}/{month:regex(\\d{2}):range(1,12)}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(year + "/" + month);
        }
    }
}