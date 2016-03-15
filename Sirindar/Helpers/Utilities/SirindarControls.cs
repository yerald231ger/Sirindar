using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Linq.Expressions;

namespace Sirindar.Helpers
{
    public static class SirindarControls 
    {
        public static MvcHtmlString DropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes, int selectedOption = 0)
        {
            var ddl = new List<SelectListItem>();
            if (typeof(TProperty).IsEnum)
            {
                foreach (string item in Enum.GetNames(typeof(TProperty)))
                {
                    var enumValue = (int)Enum.Parse(typeof(TProperty), item, true);
                    ddl.Add(new SelectListItem
                    {
                        Text = item,
                        Value = enumValue.ToString(),
                        Selected = (enumValue == selectedOption)
                    });
                }
                return htmlHelper.DropDownListFor(expression, ddl, htmlAttributes);
            }
            return htmlHelper.DropDownListFor(expression, htmlAttributes);
        }

        public static List<SelectListItem> EnumAsList<TProperty>() 
            where TProperty : struct
        {
            var ddl = new List<SelectListItem>();            
            foreach (string item in Enum.GetNames(typeof(TProperty)))
            {
                var enumValue = (int)Enum.Parse(typeof(TProperty), item, true);
                ddl.Add(new SelectListItem
                {
                    Text = item,
                    Value = enumValue.ToString()
                });
            }
            return ddl;
        }
      
    }
}