using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace ViewSparkProject
{
    [MetadataType(typeof(UserMetaData))]
    public partial class User
    {
    }


    public class UserMetaData
    {
        [Required]
        [StringLength(32)]
        public object Username { get; set; }

        [Required]
        [StringLength(127)]
        public object Email { get; set; }
    }
}