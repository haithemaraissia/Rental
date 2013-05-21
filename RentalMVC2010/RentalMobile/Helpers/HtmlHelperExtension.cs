﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace RentalMobile.Helpers
{
    public static class HtmlHelperExtensions
    {


 //       public static string RelativePath(this HtmlHelper helper, string photopath)
 //       {
 //return server.Content(item.PhotoPath)
 //       }

        public static IEnumerable<SelectListItem> GetRoles(this HtmlHelper helper)
        {
            return new[] {
                new SelectListItem { Text="Tenant" , Value="Tenant"},
                 new SelectListItem { Text="Owner" , Value="Owner"},
                  new SelectListItem { Text="Agent" , Value="Agent"},
                   new SelectListItem { Text="Specialist" , Value="Specialist"},
                new SelectListItem { Text="Landlord", Value="Landlord" },
            };
        }





        /// <summary>
        /// Custom Label for the purpose of :
        /// new { @class= "SmallInput" })
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="html"></param>
        /// <param name="expression"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>

        public static MvcHtmlString LabelFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object htmlAttributes)
        {
            return LabelFor(html, expression, new RouteValueDictionary(htmlAttributes));
        }
        public static MvcHtmlString LabelFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, IDictionary<string, object> htmlAttributes)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            var htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            var labelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();
            if (String.IsNullOrEmpty(labelText))
            {
                return MvcHtmlString.Empty;
            }

            var tag = new TagBuilder("label");
            tag.MergeAttributes(htmlAttributes);
            tag.Attributes.Add("for", html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(htmlFieldName));
            tag.SetInnerText(labelText);
            return MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
        }

        public static HtmlString Label(this HtmlHelper helper, string target = "", string text = "", string id = "")
        {
            return new HtmlString(string.Format("<label id='{0}' for='{1}'>{2}</label>", id, target, text));
        }
    }
}

