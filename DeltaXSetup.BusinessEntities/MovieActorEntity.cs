using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class MovieActorEntity
    {
        public decimal MovieActorId { get; set; }
        public decimal MovieId { get; set; }
        public decimal ActorId { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }

        public virtual ActorEntity Actor { get; set; }
        public virtual MovieEntity Movie { get; set; }
    }
}
