using BusinessEntities;
using BusinessServices.Actor;
using BusinessServices.Movies;
using BusinessServices.Producer;
using DeltaXSetup.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DeltaXSetup.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMoviesServices _MoviesServices;
        private readonly IDeltaXBaseController _ControllerBase;
        private readonly IActorServices _ActorServices;
        private readonly IProducerServices _ProducerServices;
        private Movie model;

        #region Public Constructor
        /// <summary>  
        /// Public constructor to initialize product service instance  
        /// </summary>  
        public MoviesController(IMoviesServices MoviesServices, IActorServices actorServices, IProducerServices producerServices)
        {
            _MoviesServices = MoviesServices;
            _ControllerBase = new DeltaXBaseController();
            _ActorServices = actorServices;
            _ProducerServices = producerServices;
            GetDependentData();
        }
        //
        // GET: /Movies/

        public ActionResult Index()
        {
            var movies = _MoviesServices.GetMovieList();
            if (movies != null && movies.Count > 0)
            {
                return View(movies);
            }
            return RedirectToAction("Create");
        }

        //
        // GET: /Movies/Details/5

        public ActionResult Details(int id)
        {
            var movie = _MoviesServices.GetMovieById(id);
            return View(movie);
        }

        private IEnumerable<SelectListItem> GetActors()
        {
            var actors = _ActorServices
                        .GetActorList()
                        .Select(x =>
                                new SelectListItem
                                {
                                    Value = x.ActorId.ToString(),
                                    Text = x.Name
                                });

            return new SelectList(actors, "Value", "Text");
        }

        private IEnumerable<SelectListItem> GetProducers()
        {
            var producers = _ProducerServices
                        .GetProducerList()
                        .Select(x =>
                                new SelectListItem
                                {
                                    Value = x.ProducerId.ToString(),
                                    Text = x.Name
                                });

            return new SelectList(producers, "Value", "Text");
        }

        private void GetDependentData()
        {
             model = new Movie
            {
                ActorData = GetActors(),
                ProducerData = GetProducers()
            };
        }

        //
        // GET: /Movies/Create
        public ActionResult Create()
        {            
            return View(model);
        }

        //
        // POST: /Movies/Create

        [HttpPost]
        public ActionResult Create(Movie collection, HttpPostedFileBase postedFile)
        {
            try
            {
                List<MovieActorEntity> movieActors = new List<MovieActorEntity>();
                List<MovieProducerEntity> producer = new List<MovieProducerEntity>();
                if (Request.Form["MovieData.MovieActors"].Length > 0)
                {
                    var selectedActors = Request.Form["MovieData.MovieActors"].Split(',');
                    foreach (var item in selectedActors)
                    {
                        movieActors.Add(new MovieActorEntity { ActorId = Convert.ToInt32(item), MovieId = collection.MovieData.MovieId });
                    }
                }
                else
                {
                    ModelState.AddModelError("Actors", "Atleast One Actor is required.!");
                    return View();
                }
                if (Request.Form["MovieData.MovieProducers"].Length > 0)
                {
                    var selectedProducer = Request.Form["MovieData.MovieProducers"];
                    producer.Add(new MovieProducerEntity { ProducerId = Convert.ToInt32(selectedProducer), MovieId = collection.MovieData.MovieId });
                }
                else {
                    ModelState.AddModelError("Producer", "Producer is required.!");
                    return View(model);
                }
                MovieEntity movie = new MovieEntity
                {
                    MovieId = collection.MovieData.MovieId,
                    Name=collection.MovieData.Name,
                    Plot = collection.MovieData.Plot,
                    ReleaseYear = collection.MovieData.ReleaseYear,
                    File = postedFile,
                    MovieActors = movieActors,
                    MovieProducers = producer
                };

                decimal result = _MoviesServices.AddMovie(movie);

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return View(model);
            }
        }

        //
        // GET: /Movies/Edit/5

        public ActionResult Edit(int id)
        {
            var movie = _MoviesServices.GetMovieById(id);
            var model = new Movie
            {
                MovieData = movie,
                ActorData = GetActors(),
                ProducerData = GetProducers()
            };
            return View(model);
        }

        //
        // POST: /Movies/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, Movie collection, HttpPostedFileBase postedFile)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Movies/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Movies/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        #endregion
    }
}
