﻿<%@ Control Language="C#" Inherits="Arashi.Web.Mvc.TemplateEngine.TemplateBase<Arashi.Web.Mvc.Models.TemplateContentModel>" %>
<% get_header(); %>

	<div id="content" class="widecolumn" role="main">

	<% if (have_posts()) { %>

		<div class="navigation">
			<%--<div class="alignleft"><?php previous_post_link('&laquo; %link') ?></div>
			<div class="alignright"><?php next_post_link('%link &raquo;') ?></div>--%>
		</div>
	
      <% foreach (Post post in Model.Posts) { the_post(post); %>

			<div <%= post_class() %> id='post-<%= the_ID() %>'>
				<h2><%= the_title() %></h2>

				<div class="entry">
					<%= the_content("<p class='serif'>Read the rest of this entry &raquo;</p>") %>

				   <%--<?php wp_link_pages(array('before' => '<p><strong>Pages:</strong> ', 'after' => '</p>', 'next_or_number' => 'number')); ?>--%>
				   <%= the_tags( "<p>Tags: ", ", ", "</p>") %>

				   <p class="postmetadata alt">
					   <small>
						   This entry was posted
						   <%-- /* This is commented, because it requires a little adjusting sometimes.
							   You'll need to download this plugin, and follow the instructions:
							   http://binarybonsai.com/wordpress/time-since/ */--%>
							   <%--/* $entry_datetime = abs(strtotime($post->post_date) - (60*120)); echo time_since($entry_datetime); echo ' ago'; */--%> 
						   on <%= the_time("l, F jS, Y") %> at <%= the_time() %>
						   and is filed under <%= the_category(", ") %>.
						   <%--You can follow any responses to this entry through the <%= post_comments_feed_link("RSS 2.0") %> feed.--%>

						   <% if ( comments_open() && pings_open() ) { %>
							   <%--// Both Comments and Pings are open --%>
							   You can <a href="#respond">leave a response</a>, or <a href='<%= trackback_url() %>' rel="trackback">trackback</a> from your own site.
                     <% } else if (!comments_open() && pings_open()) { %>
							   <%--// Only Pings are Open--%> 
							   Responses are currently closed, but you can <a href='<%= trackback_url() %>' rel="trackback">trackback</a> from your own site.
                     <% } else if (comments_open() && !pings_open()) { %>
							   <%--// Comments are open, Pings are not--%>
							   You can skip to the end and leave a response. Pinging is currently not allowed.
                     <% } else if (!comments_open() && !pings_open()) { %>
							   <%--// Neither Comments, nor Pings are open--%>
							   Both comments and pings are currently closed.
                     <% } %>

						   <%--<?php } edit_post_link('Edit this entry','','.'); ?>--%>

					   </small>
				   </p>
				</div>
			</div>

	      <% comments_template(); %>

      <% } %>
   <% } else { %>
		<p>Sorry, no posts matched your criteria.</p>
   <% } %>

	</div>

<% get_footer(); %>
