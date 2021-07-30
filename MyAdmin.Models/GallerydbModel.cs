using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyAdmin.Models
{
  public class GallerydbModel
    {
        public int?  imgID { get; set; }
        [Required]
        [DisplayName("Image Title")]
        public string ImageTitle { get; set; }
       
        public string ImageDesc { get; set; }
       
        public HttpPostedFileBase ImageFile { get; set; } 
    }
}
