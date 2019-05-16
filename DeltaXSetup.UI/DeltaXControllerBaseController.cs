using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DeltaXSetup
{
    interface IDeltaXBaseController
    {
        ActionResult json(object obj);
    }

    public class DeltaXBaseController : Controller, IDeltaXBaseController
    {
        //
        // GET: /DeltaXControllerBase/
        public ActionResult json(object obj)
        {
            return base.Json(obj);
        }

    }
}
