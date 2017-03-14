using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ViewSparkProject.Models
{
    public class _QueuedSlidesModel
    {
        public int ID { get; set; }
        public int ItemCount { get; set; }
        public int CurrentPage { get; set; }
        public int MaxColumn { get; set; }
        public int MaxRow { get; set; }
        public string Title { get; set; }
        public string View { get; set; }

        public _QueuedSlidesModel(int id, int itemCount, int currentPage, string title, string view, int maxColumn, int maxRow)
        {
            this.ID = id;
            this.ItemCount = itemCount;
            this.CurrentPage = currentPage;
            this.Title = title;
            this.View = view;
            this.MaxColumn = maxColumn;
            this.MaxRow = maxRow;
        }

        public int GetJumpToPageIndex()
        {
            if(View == "Present")
            {
                return 1;
            }
            else if(View == "Edit")
            {
                return 1;
            }

            return 0;
        }

        public int MaxPage()
        {
            return ((ItemCount - 1) / (MaxColumn * MaxRow)) + 1;
        }

        public string GetColumnClass()
        {
            if (MaxColumn == 2)
            {
                return "twocolx";
            }
            if (MaxColumn == 3)
            {
                return "threecolx";
            }
            if (MaxColumn == 4)
            {
                return "fourcolx";
            }
            if (MaxColumn == 5)
            {
                return "fivecolx";
            }
            if (MaxColumn == 6)
            {
                return "sixcolx";
            }
            if (MaxColumn == 7)
            {
                return "sevencolx";
            }

            return "fullcolx";
        }
    }
}