using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace NPager.Helpers
{
    public static class ControlExtensions
    {
        public static void AddCssClass(this Control con, string cssClass)
        {
            if (con == null) return;
            List<string> classes;

            var webControl = con as WebControl;
            if (webControl != null)
            {
                var control = webControl;
                var c = control.Attributes["class"];
                if (!string.IsNullOrWhiteSpace(c))
                {
                    classes = c.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    if (!classes.Contains(cssClass))
                        classes.Add(cssClass);
                }
                else
                {
                    classes = new List<string> { cssClass };

                }
                control.Attributes["class"] = string.Join(" ", classes.ToArray());

            }
            else
            {
                var control = con as HtmlControl;
                if (control == null) return;
                var c = control.Attributes["class"];
                if (!string.IsNullOrWhiteSpace(c))
                {
                    classes = c.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    if (!classes.Contains(cssClass))
                        classes.Add(cssClass);
                }
                else
                {
                    classes = new List<string> { cssClass };
                }
                control.Attributes["class"] = string.Join(" ", classes.ToArray());
            }


        }

        public static void RemoveCssClass(this WebControl control, string cssClass)
        {
            if (control == null) return;
            var classes = new List<string>();
            var c = control.Attributes["class"];
            if (!string.IsNullOrWhiteSpace(c))
            {
                classes = c.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            }
            classes.Remove(cssClass);
            control.Attributes["class"] = string.Join(" ", classes.ToArray());
        }
    }
}
