using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ViewSparkProject.Models
{
    public class EditModel
    {
        public string type { get; set; }
        public int id{ get; set;}

        public EditModel(string type = "none", int id = -1)
        {
            this.type = type;
            this.id = id;
        }
    }
}