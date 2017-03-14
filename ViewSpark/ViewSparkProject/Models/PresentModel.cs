using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ViewSparkProject.Models
{
    public class PresentModel
    {
        public int id { get; set; }

        public PresentModel(int id)
        {
            this.id = id;
        }
    }
}