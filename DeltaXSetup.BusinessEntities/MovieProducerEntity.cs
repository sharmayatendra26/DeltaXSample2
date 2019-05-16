using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public partial class MovieProducerEntity
    {
        public decimal MovieProducerId { get; set; }
        public decimal MovieId { get; set; }
        public decimal ProducerId { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }

        public virtual MovieEntity Movie { get; set; }
        public virtual ProducerEntity Producer { get; set; }
    }
}
