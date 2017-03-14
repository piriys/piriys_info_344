using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace ViewSparkProject
{
    [MetadataType(typeof(GroupMetaData))]
    public partial class Group
    {
    }


    public class GroupMetaData
    {
        [Required]
        [StringLength(254)]
        public object Name { get; set; }


        [Required]
        [StringLength(254)]
        public object Description { get; set; }
    }
}