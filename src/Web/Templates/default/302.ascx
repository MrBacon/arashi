﻿<%@ Control Language="C#" Inherits="Arashi.Web.Mvc.TemplateEngine.TemplateBase<Arashi.Web.Mvc.Models.TemplateContentModel>" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" lang="en-US">

<head>
<meta http-equiv="Content-Type" content='<%= bloginfo("html_type") %>; charset=<%= bloginfo("charset") %>' />
<title><%= wp_title("&laquo;", true, "right") %></title>
<link rel="stylesheet" href='<%= bloginfo("stylesheet_url") %>' type="text/css" media="screen" />
<style type="text/css" media="screen">
	#page { background: url('<%= bloginfo("stylesheet_directory") %>/images/kubrickbgwide.jpg') repeat-y top; border: none; }
   #maintenance-container {
      display: block;
      height: 200px;
      text-align: center;
      margin: 0 20%;
   }

   #maintenance-container span.title {
      font-weight: bold;
      font-size: 22px;
   }
   #maintenance-container span.description {
      font-weight: normal;
      font-size: 20px;
   }
</style>
<%= wp_head() %>
</head>
<body <%= body_class() %>>
   <!-- Start Navigation -->
   <div id="navwrap">
      <div class="navigation">
         <div class="border">
            <%--
            <ul id="nav">
		    	</ul>
            <div class="search">
            </div>
            --%>
            <div class="clear"></div>
         </div>
      </div>
   </div>
   <!-- Stop Navigation -->
   
      <!-- Start Page -->
		<div id="page">
			<!-- Header -->
         <div id="header">
	         <div id="headerimg">
		         <h1><a href='<%= get_option("home") %>/'><%= bloginfo("name") %></a></h1>
		         <div class="description"><%= bloginfo("description") %></div>
	         </div>
         </div>
         <hr />

         <div class="clear"></div>
         <div id="content">

         <div class="content_wrap">

            <div id="maintenance-container" >
               <p style="text-align:center;">
                  <span class="description" style="color: #666;">
                     This site is temporarily closed for maintenance
                     <br />
                     Please come back later...
                  </span>
                  <br />
                  <br />
                  <br />
                  <br />
               </p>
            </div>

            <div class="clear"></div>
         </div>

         </div>
         <!-- End Content -->
<hr />
<div id="footer">
	<p>
		<%= bloginfo("name") %> is powered by
		<a href="http://www.arashi-project.com/">Arashi</a>
		<br /><a href='<%= bloginfo("rss2_url") %>'>Entries (RSS)</a>
		<%--and <a href='<% bloginfo("comments_rss2_url"); %>'>Comments (RSS)</a>.--%>
	</p>
</div>
</div>
<%= Plugin.GoogleAnalytics() %>
</body>
</html>
