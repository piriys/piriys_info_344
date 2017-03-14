using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ViewSparkProject
{
    [MetadataType(typeof(PollMetaData))]
    public partial class Poll
    {
    }

    public class PollMetaData
    {
        [Display(Name = "Poll Name")]
        [Required(ErrorMessage = "You must type in a poll name.")]
        public object Name { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "You must type in a description.")]
        public string Descr { get; set; }

        [Display(Name = "Choice 1")]
        [Required(ErrorMessage = "You must type in a choice.")]
        public string Choice1 { get; set; }

        [Display(Name = "Choice 2")]
        [Required(ErrorMessage = "You must type in a choice.")]
        public string Choice2 { get; set; }

        [Display(Name = "Choice 3")]
        [Required(ErrorMessage = "You must type in a choice.")]
        public string Choice3 { get; set; }

        [Display(Name = "Choice 4")]
        [Required(ErrorMessage = "You must type in a choice.")]
        public string Choice4 { get; set; }

        [Display(Name = "Choice 5")]
        [Required(ErrorMessage = "You must type in a choice.")]
        public string Choice5 { get; set; }
    }
}