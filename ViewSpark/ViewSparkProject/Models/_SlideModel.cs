using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ViewSparkProject.Models
{
    public class _SlideModel
    {
        public string View { get; set; }

        public _SlideModel(Slide slide, string view)
        {
            this.Slide = slide;
            this.View = view;
        }

        public string GetID()
        {
            return "slide-" + Slide.ID;
        }

        public string GetIDWithHash()
        {
            return "#slide-" + Slide.ID;
        }

        public Slide Slide { get; set; }
    }
}