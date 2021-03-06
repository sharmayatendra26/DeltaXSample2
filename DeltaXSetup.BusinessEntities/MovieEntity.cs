﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BusinessEntities
{
    public class MovieEntity
    {      
        public decimal MovieId { get; set; }
        public string Name { get; set; }
        public decimal ReleaseYear { get; set; }
        public string Plot { get; set; }
        public HttpPostedFileBase File { get; set; }
        public string Poster { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }

        public ICollection<MovieActorEntity> MovieActors { get; set; }
        public ICollection<MovieProducerEntity> MovieProducers { get; set; }
    }


}
