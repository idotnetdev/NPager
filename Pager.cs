using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using NPager.Helpers;

namespace NPager
{
    public class PagerControl : Control
    {
        public PagerControl()
        {
            var query = HttpContext.Current.Request.QueryString["pageIndex"].ToInt();
            PageIndex = query < 1 ? 1 : query;
        }

        #region Fields
        private int _pageSize;
        private string _paginationUlClass;

        #endregion

        #region Public Members
        public int Offset
        {
            get
            {
                if (PageIndex > 0)
                {
                    return (PageIndex.ToInt() - 1) * PageSize;
                }
                return 0;
            }
        }
        public long RecordCount { get; set; }

        /// <summary>
        /// Size of the page, by default its 12.
        /// </summary>
        public int PageSize
        {
            get
            {
                if (_pageSize == 0)
                {
                    _pageSize = 12;
                }
                return _pageSize;
            }
            set { _pageSize = value; }
        }
        /// <summary>
        /// Gives CSS class to pagination UL tag
        /// </summary>
        public string PaginationUlClass
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_paginationUlClass))
                {
                    _paginationUlClass = "pagination pagination-sm";
                }
                return _paginationUlClass;
            }
            set { _paginationUlClass = value; }
        }
        #endregion

        #region Private 
        private int PageIndex { get; set; }
        private const int PagerBlockSize = 10;

        private int StartIndex
        {
            get
            {
                var startFactor = Math.Floor(PageIndex.ToDecimal() / PagerBlockSize).ToInt();
                var startIndex = PagerBlockSize * startFactor;
                return startIndex > 0 ? startIndex : 1;
            }
        }

        private int EndIndex
        {
            get
            {
                var end = StartIndex + PagerBlockSize - 1;
                return end > LastIndex ? LastIndex : end;
            }
        }

        private int LastIndex
        {
            get
            {
                long remainder;
                var quoteint = Math.DivRem(RecordCount, (long)PageSize, out remainder);
                if (remainder > 0)
                {
                    remainder = 1;

                }
                return (int)(quoteint + remainder);
            }
        }

        private int PageCount => Math.Ceiling(RecordCount.ToDecimal() / PageSize).ToInt();

        #endregion

        #region Behaviours
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            this.Controls.Add(GetPagerAnchors());
        }

        protected Control GetPagerAnchors()
        {
            if (RecordCount < PageSize) return new Control();

            var pagerWrapper = new HtmlGenericControl("div");

            var pagerDiv = new HtmlGenericControl("div");
            pagerDiv.AddCssClass("col-sm-8");

            var paginationUl = new HtmlGenericControl("ul");
            paginationUl.AddCssClass(PaginationUlClass);
            paginationUl.Style.Add(HtmlTextWriterStyle.Margin, "0");
            AddPagerFirstPageAnchor(paginationUl);
            AddPagerPreviousButton(paginationUl);
            AddPagerLi(paginationUl);
            AddPagerNextButton(paginationUl);
            AddPagerLastPageAnchor(paginationUl);
            pagerDiv.Controls.Add(paginationUl);

            var pageInfoDiv = new HtmlGenericControl("div");
            pageInfoDiv.AddCssClass("col-sm-4 text-right");
            AddPageInfo(pageInfoDiv);

            pagerWrapper.Controls.Add(pagerDiv);
            pagerWrapper.Controls.Add(pageInfoDiv);

            return pagerWrapper;
        }

        private void AddPagerFirstPageAnchor(Control paginationUl)
        {
            var li = new HtmlGenericControl("li");
            var anchor = new HtmlAnchor
            {
                HRef = QueryStringHelper.AddUpdateQueryStringGetUrl("pageIndex", 1),
                InnerHtml = "First",
                Title = "Go to first page"
            };
            if (PageIndex == 1)
            {
                li.AddCssClass("disabled");
                anchor.HRef = "javascript:void(o)";
            }
            li.Controls.Add(anchor);
            paginationUl.Controls.Add(li);
        }

        private void AddPagerPreviousButton(Control parentControl)
        {
            var li = new HtmlGenericControl("li");
            var anchor = new HtmlAnchor
            {
                HRef = QueryStringHelper.AddUpdateQueryStringGetUrl("pageIndex", PageIndex - 1),
                InnerHtml = "Prev",
                Title = "Go to previous page"

            };
            if (PageIndex == 1)
            {
                li.AddCssClass("disabled");
                anchor.HRef = "javascript:void(o)";
            }
            li.Controls.Add(anchor);
            parentControl.Controls.Add(li);
        }

        private void AddPagerNextButton(Control parentControl)
        {
            var li = new HtmlGenericControl("li");
            var anchor = new HtmlAnchor
            {
                HRef = QueryStringHelper.AddUpdateQueryStringGetUrl("pageIndex", PageIndex + 1),
                InnerHtml = "Next",
                Title = "Go to next page"
            };
            if (PageIndex == LastIndex)
            {
                li.AddCssClass("disabled");
                anchor.HRef = "javascript:void(o)";
            }
            li.Controls.Add(anchor);
            parentControl.Controls.Add(li);
        }

        private void AddPagerLastPageAnchor(Control paginationUl)
        {
            var li = new HtmlGenericControl("li");
            var anchor = new HtmlAnchor
            {
                HRef = QueryStringHelper.AddUpdateQueryStringGetUrl("pageIndex", LastIndex),
                InnerHtml = "Last",
                Title = "Go to last page"
            };
            if (PageIndex == LastIndex)
            {
                li.AddCssClass("disabled");
                anchor.HRef = "javascript:void(o)";
            }
            li.Controls.Add(anchor);
            paginationUl.Controls.Add(li);
        }

        private void AddPageInfo(Control colDiv5)
        {
            var small = new HtmlGenericControl("p");
            small.AddCssClass("control-label text-muted");
            small.Style.Add(HtmlTextWriterStyle.FontSize, "95%");
            small.InnerText = $"Showing Page {PageIndex} of {PageCount} | Total Records : {RecordCount}";
            colDiv5.Controls.Add(small);
        }

        private void AddPagerLi(Control parentControl)
        {
            for (var i = StartIndex; i <= EndIndex; i++)
            {
                var li = new HtmlGenericControl("li");
                var anchor = new HtmlAnchor
                {
                    HRef = QueryStringHelper.AddUpdateQueryStringGetUrl("pageIndex", i),
                    InnerText = i.ToString(),
                    Title = "Go to page " + i
                };
                if (PageIndex == i)
                {
                    li.AddCssClass("active");
                }
                li.Controls.Add(anchor);
                parentControl.Controls.Add(li);
            }
        }

        #endregion

    }
}
