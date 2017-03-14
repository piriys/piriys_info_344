using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ViewSparkProject.Models
{
    public class _FileBrowserModel
    {
        public JsonResult ItemList { get; set;}
        public string FileType { get; set; }
        public string View { get; set; }
        public int ItemCount { get; set; }
        public int ItemPerPage { get; set; }
        public int CurrentPage { get; set; }

        public _FileBrowserModel(string view, string fileType, JsonResult itemList, int itemcount, int itemPerPage, int currentPage)
        {
            this.ItemList = itemList;
            this.FileType = fileType;
            this.View = view;
            this.ItemCount = itemcount;
            this.ItemPerPage = itemPerPage;
            this.CurrentPage = currentPage;
        }

        public string GetComponentId() 
        {
            return "fileBrowser-" + FileType;
        }

        public string GetComponentIdWithHash()
        {
            return "#fileBrowser-" + FileType;
        }

        public string GetLoaderId()
        {
            return "fileBrowser-" + FileType + "_Loader";
        }

        public string GetLoaderIdWithHash()
        {
            return "#fileBrowser-" + FileType + "Loader";
        }

        public int MaxPage()
        {
            return ((ItemCount - 1) / ItemPerPage) + 1;
        }
        public int subPageIndex()
        {
            if (View == "Edit")
            {
                if (FileType == "Slides")
                {
                    return 1;
                }
                else if (FileType == "Polls")
                {
                    return 2;
                }
                else if (FileType == "Presentations")
                {
                    return 0;
                }
            }
            else if (View == "Present")
            {
                if (FileType == "Slides")
                {
                    return 1;
                } 
                else if (FileType == "Presentations")
                {
                    return 1;
                }
            }

            return 0;
        }
    }
}