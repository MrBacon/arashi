﻿<%@ Control Language="C#" Inherits="Arashi.Web.Mvc.TemplateEngine.TemplateBase<Arashi.Web.Mvc.Models.TemplateContentModel>" %>
<% get_header(); %>

	<div id="content" class="narrowcolumn" >

	<% if (have_posts()) { %>
	
      <% foreach (Post post in Model.Posts) { the_post(post); %>

			<div <%= post_class() %> id='post-<%= the_ID() %>'>
				<h2><a href='<%= the_permalink() %>' rel="bookmark" title="Permanent Link to <%= the_title_attribute() %>"><%= the_title() %></a></h2>
				<small><%= the_time("F jS, Y") %> by <%= the_author() %> </small>

				<div class="entry">
					<%= the_content("Read the rest of this entry &raquo;") %>
				</div>

				<p class="postmetadata"><%= the_tags("Tags: ", ", ", "<br />") %> Posted in <%= the_category(", ") %> | <%= comments_popup_link("No Comments &#187;", "1 Comment &#187;", "% Comments &#187;") %></p>
			</div>

      <% } %>

		<div class="navigation">
			<div class="alignleft"><%= next_posts_link("&laquo; Older Entries") %></div>
			<div class="alignright"><%= previous_posts_link("Newer Entries &raquo;") %></div>
			<%--<%= wp_pagenavi()%>--%>
		</div>

	<% } else { %>

		<h2 class="center">Not Found</h2>
		<p class="center">Sorry, but you are looking for something that isn't here.</p>
		<%-- TODO: to do!!
		<?php get_search_form(); ?>--%>

	<% } %>

	</div>

<% get_sidebar(); %>
<% get_footer(); %>
