using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DeltaXSetup.Models
{
    public class Movie
    {
        public MovieEntity MovieData { get; set; }

        public IEnumerable<SelectListItem> ActorData { get; set; }

        public IEnumerable<SelectListItem> ProducerData { get; set; }
    }
}