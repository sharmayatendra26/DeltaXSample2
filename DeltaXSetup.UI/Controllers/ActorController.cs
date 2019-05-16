using BusinessServices.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessEntities;
using DeltaXSetup.UI.Logging;

namespace DeltaXSetup.Controllers
{
    public class ActorController : Controller
    {
        private readonly IActorServices _ActorServices;
        private readonly IDeltaXBaseController _ControllerBase;
        Log logger = new Log("ActorController");

        #region Public Constructor        
        /// <summary>  
        /// Public constructor to initialize product service instance  
        /// </summary>  
        public ActorController(IActorServices actorServices)
        {
            _ActorServices = actorServices;
            _ControllerBase = new DeltaXBaseController();
        }

        #endregion 
        //
        // GET: /Default1/

        public ActionResult Index()
        {
            var actors = _ActorServices.GetActorList();
            if (actors != null && actors.Count > 0)
            {
                return View(actors);
            }
            return RedirectToAction("Create");
        }

        //
        // GET: /Default1/Details/5

        public ActionResult Details(int id)
        {
            var actor = _ActorServices.GetActorById(id);
            return View(actor);
        }

        //
        // GET: /Default1/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Default1/Create

        [HttpPost]
        public ActionResult Create(ActorEntity actor)
        {
            try
            {
                decimal result = _ActorServices.AddActor(actor);

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                logger.monitoringLogger.Error("Error", ex.InnerException);
                return View();
            }
        }

        //
        // GET: /Default1/Edit/5

        public ActionResult Edit(int id)
        {
            var actor = _ActorServices.GetActorById(id);
            return View(actor);
        }

        //
        // POST: /Default1/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, ActorEntity actor)
        {
            try
            {
                _ActorServices.UpdateActor(id, actor);

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                logger.monitoringLogger.Error("ActorUpdate Error:", ex);
                ModelState.AddModelError("Update Error", "Update Failed");
                return View();
            }
        }

        //
        // GET: /Default1/Delete/5

        public ActionResult Delete(int id)
        {
            //var result = _ActorServices.DeleteActor(id);
            return RedirectToAction("Index");
        }

        ////
        //// POST: /Default1/Delete/5

        //[HttpPost]
        //public ActionResult Delete(int id, ActorEntity actor)
        //{
        //    try
        //    {
        //        _ActorServices.DeleteActor(
        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
