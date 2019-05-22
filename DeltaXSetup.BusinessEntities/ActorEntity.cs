using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BusinessEntities
{
    public partial class ActorEntity
    {
        [Required]
        public decimal ActorId { get; set; }

        [Required]
        [Display(Name = "Actor Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public string Sex { get; set; }

        [Required]
        [Display(Name = "Date Of Birth")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")] 
        public DateTime DOB { get; set; }

        public string Bio { get; set; }

        [HiddenInput(DisplayValue = true)]
        public bool IsDeleted { get; set; }

        [HiddenInput(DisplayValue = true )]
        public DateTime CreatedOn { get; set; }

        [HiddenInput(DisplayValue = true)]
        public string CreatedBy { get; set; }

        [HiddenInput(DisplayValue = true)]
        public DateTime ModifiedOn { get; set; }

        [HiddenInput(DisplayValue = true)]
        public string ModifiedBy { get; set; }

        //public virtual ICollection<MovieActorEntity> MovieActors { get; set; }
    }
}
