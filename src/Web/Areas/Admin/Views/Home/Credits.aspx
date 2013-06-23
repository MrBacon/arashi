<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
	<title>Credits</title>
	<script type="text/javascript">
	   $(function() {
	      $("#credits li:not(.ui-state-disabled)")
            .hover(
               function() {
                  $(this).addClass("ui-state-hover ui-corner-all");
               },
               function() {
                  $(this).removeClass("ui-state-hover ui-corner-all");
               }
            )	   
	   });
	</script>
   <style type="text/css">
   li.item {
      margin: 8px 0;
      padding: 4px;
      border: solid 1px transparent;
   }
   #credits img {
      float: left;
      margin-right: 7px;
   }
   #credits p {
      height: 48px;
      margin: 0 0 0 210px;  
      padding-left:3px;
      border-left: 1px solid #A6C9E2;
      font-weight: normal; 
   }
   #credits span {color: #585858}
   #credits span.title {
      font-size: 1.2em;
      font-weight: bold;
      color: #2E6E9E;
   }
   #credits a:hover span {color: #E17009}
   </style>	
</asp:Content>

<asp:Content ContentPlaceHolderID="BreadCrumbsPlaceHolder" runat="server">
   <%= Html.Breadcrumbs(b => b.AddHome("Home", Url.Action("Index", "Site", new {siteid = RequestContext.ManagedSite.SiteId}))
                              .AddCurrent("Credits")) %>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
   <div id="adminpagetitle">
      <img class="icon" src="/Resources/img/32x32/bookmark.png" alt="list users" />
      <h2>Credits & Thanks</h2>
   </div>

   <div id="credits" class="ui-widget ui-widget-content ui-corner-all">
      <ul class="content-padding-all">
         <li class="item ui-corner-all">
            <a class="ui-widget ui-corner-all" href="http://www.cuyahoga-project.org/" >
               <img src="http://www.cuyahoga-project.org/UserFiles/Image/logos/cuyahoga-wide.png" alt='site'
                    width="202" height="32" />
               <p>
                  <span class="title">Cuyahoga Framework (Martin Boland)</span>
               </p>
            </a>
         </li>
         <li class="item ui-corner-all">
            <a class="ui-widget ui-corner-all" href="http://jquery.com/" >
               <img src="/Resources/img/jQuery_logo_bw_32.png" alt='jquery' />
               <p>
                  <span class="title">jQuery</span>
               </p>
            </a>
         </li>
         <li class="item ui-corner-all">
            <a class="ui-widget ui-corner-all" href="http://jqueryui.com/" >
               <img src="/Resources/img/jQuery_UI_logo_color_onwhite_32.png" alt='jqueryui' />
               <p>
                  <span class="title">jQuery UI</span>
               </p>
            </a>
         </li>
         <li class="item ui-corner-all">
            <a class="ui-widget ui-corner-all" href="http://nhforge.org/" >
               <img src="/Resources/img/LogoNH.gif" alt='nhforge' />
               <p>
                  <span class="title">NHibernate</span>
               </p>
            </a>
         </li>
         <li class="item ui-corner-all">
            <a class="ui-widget ui-corner-all" href="http://code.google.com/p/unhaddins/" >
               <img src="/Resources/img/uNHaddins.png" alt='uNHaddins' width="48" height="48" />
               <p>
                  <span class="title">uNHAddins (Fabio Maulo)</span>
               </p>
            </a>
         </li>
      </ul>
   </div>
</asp:Content>
