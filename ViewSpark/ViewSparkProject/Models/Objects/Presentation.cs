using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace ViewSparkProject
{
    [MetadataType(typeof(PresentationMetaData))]
    public partial class Presentation
    {
    }


    public class PresentationMetaData
    {
        [Required]
        [StringLength(254)]
        public object Title { get; set; }
    }
}