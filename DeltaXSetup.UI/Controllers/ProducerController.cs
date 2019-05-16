using BusinessEntities;
using BusinessServices.Producer;
using DeltaXSetup.UI.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DeltaXSetup.Controllers
{
    public class ProducerController : Controller
    {
        private readonly IProducerServices _ProducerServices;
        private readonly IDeltaXBaseController _ControllerBase;
        Log logger = new Log("ActorController");
        #region Public Constructor        
        /// <summary>  
        /// Public constructor to initialize product service instance  
        /// </summary>  
        public ProducerController(IProducerServices ProducerServices)
        {
            _ProducerServices = ProducerServices;
            _ControllerBase = new DeltaXBaseController();
        }
        //
        // GET: /Producer/

        public ActionResult Index()
        {
            var producers = _ProducerServices.GetProducerList();
            if (producers != null && producers.Count > 0)
            {
                return View(producers);
            }
            return RedirectToAction("Create");
        }

        //
        // GET: /Producer/Details/5

        public ActionResult Details(int id)
        {
            var producer = _ProducerServices.GetProducerById(id);
            return View(producer);
        }

        //
        // GET: /Producer/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Producer/Create

        [HttpPost]
        public ActionResult Create(ProducerEntity producer)
        {
            try
            {
                decimal result = _ProducerServices.AddProducer(producer);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                logger.monitoringLogger.Error("Error", ex.InnerException);
                return View();
            }
        }

        //
        // GET: /Producer/Edit/5

        public ActionResult Edit(int id)
        {
            var producer = _ProducerServices.GetProducerById(id);
            return View(producer);
        }

        //
        // POST: /Producer/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, ProducerEntity producer)
        {
            try
            {
                _ProducerServices.UpdateProducer(id, producer);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                logger.monitoringLogger.Error("ProducerUpdate Error:", ex);
                ModelState.AddModelError("Update Error", "Update Failed");
                return View();
            }
        }

        //
        // GET: /Producer/Delete/5

        public ActionResult Delete(int id)
        {
            //var result = _ProducerServices.DeleteProducer(id);
            return RedirectToAction("Index");
        }

        
        #endregion
    }
}
