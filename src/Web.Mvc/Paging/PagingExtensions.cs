﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace Arashi.Web.Mvc.Paging
{
   using System.Web;

   public static class PagingExtensions
   {
      #region HtmlHelper extensions

      public static IHtmlString Pager(this HtmlHelper htmlHelper, int pageSize, int currentPage, long totalItemCount)
      {
         return Pager(htmlHelper, pageSize, currentPage, totalItemCount, null, null);
      }

      public static IHtmlString Pager(this HtmlHelper htmlHelper, int pageSize, int currentPage, long totalItemCount, string actionName)
      {
         return Pager(htmlHelper, pageSize, currentPage, totalItemCount, actionName, null, null);
      }

      public static IHtmlString Pager(this HtmlHelper htmlHelper, int pageSize, int currentPage, long totalItemCount, string actionName, string searchText)
      {
         return Pager(htmlHelper, pageSize, currentPage, totalItemCount, null, null, null, searchText);
      }

      public static IHtmlString Pager(this HtmlHelper htmlHelper, int pageSize, int currentPage, long totalItemCount, object values)
      {
         return Pager(htmlHelper, pageSize, currentPage, totalItemCount, null, new RouteValueDictionary(values), null);
      }

      public static IHtmlString Pager(this HtmlHelper htmlHelper, int pageSize, int currentPage, long totalItemCount, string actionName, object values)
      {
         return Pager(htmlHelper, pageSize, currentPage, totalItemCount, actionName, new RouteValueDictionary(values), null);
      }

      public static IHtmlString Pager(this HtmlHelper htmlHelper, int pageSize, int currentPage, long totalItemCount, string actionName, object values, string searchText)
      {
         return Pager(htmlHelper, pageSize, currentPage, totalItemCount, actionName, new RouteValueDictionary(values), null, searchText);
      }

      public static IHtmlString Pager(this HtmlHelper htmlHelper, int pageSize, int currentPage, long totalItemCount, RouteValueDictionary valuesDictionary)
      {
         return Pager(htmlHelper, pageSize, currentPage, totalItemCount, null, valuesDictionary, null);
      }

      public static IHtmlString Pager(this HtmlHelper htmlHelper, int pageSize, int currentPage, long totalItemCount, string actionName, RouteValueDictionary valuesDictionary, string queryStringName)
      {
         if (valuesDictionary == null)
         {
            valuesDictionary = new RouteValueDictionary();
         }
         if (actionName != null)
         {
            if (valuesDictionary.ContainsKey("action"))
            {
               throw new ArgumentException("The valuesDictionary already contains an action.", "actionName");
            }
            valuesDictionary.Add("action", actionName);
         }
         var pager = new Pager(htmlHelper.ViewContext, pageSize, currentPage, totalItemCount, valuesDictionary, queryStringName);
         return htmlHelper.Raw(pager.RenderHtml());
      }

      public static IHtmlString Pager(this HtmlHelper htmlHelper, int pageSize, int currentPage, long totalItemCount, string actionName, RouteValueDictionary valuesDictionary, string queryStringName, string searchtext)
      {
         if (valuesDictionary == null)
         {
            valuesDictionary = new RouteValueDictionary();
         }
         if (actionName != null)
         {
            if (valuesDictionary.ContainsKey("action"))
            {
               throw new ArgumentException("The valuesDictionary already contains an action.", "actionName");
            }
            valuesDictionary.Add("action", actionName);
         }
         var pager = new Pager(htmlHelper.ViewContext, pageSize, currentPage, totalItemCount, valuesDictionary, queryStringName, searchtext);
         return htmlHelper.Raw(pager.RenderHtml());
      }

      #endregion

      #region IQueryable<T> extensions

      public static IPagedList<T> ToPagedList<T>(this IQueryable<T> source, int pageIndex, int pageSize)
      {
         return new PagedList<T>(source, pageIndex, pageSize);
      }

      public static IPagedList<T> ToPagedList<T>(this IQueryable<T> source, int pageIndex, int pageSize, int totalCount)
      {
         return new PagedList<T>(source, pageIndex, pageSize, totalCount);
      }

      #endregion

      #region IEnumerable<T> extensions

      public static IPagedList<T> ToPagedList<T>(this IEnumerable<T> source, int pageIndex, int pageSize)
      {
         return new PagedList<T>(source, pageIndex, pageSize);
      }

      public static IPagedList<T> ToPagedList<T>(this IEnumerable<T> source, int pageIndex, int pageSize, int totalCount)
      {
         return new PagedList<T>(source, pageIndex, pageSize, totalCount);
      }

      #endregion
   }
}