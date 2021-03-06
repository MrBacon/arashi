﻿<%@ Control Language="C#" Inherits="Arashi.Web.Mvc.TemplateEngine.TemplateBase<Arashi.Web.Mvc.Models.TemplateContentModel>" %>
<% get_header(); %>

		<!-- coda slider -->
		
        <div id="slider" class="blog">

            <div class="scroll blog">
			
				<div id="blog_header_top">&nbsp;</div>
			
                <div class="scrollContainer blog">
				
					<div class="panel" id="panel_01">
					
						<div id="blog_header_midtop">&nbsp;</div>
					
						<%--<h2 class="title main" id="title-blog"><?= $post->post_title; ?></h2>--%>
                  <% if (have_posts()) { %>
                     <% /* If this is a category archive */ if (is_category()) { %>
                     <h2 class="pagetitle">Archive for the &#8216;<%= single_cat_title() %>&#8217; Category</h2>
                     <% /* If this is a tag archive */ } else if ( is_tag() ) { %>
                     <h2 class="pagetitle">Posts Tagged &#8216;<%= single_tag_title() %>&#8217;</h2>
                     <% /* If this is a daily archive */ } else if (is_day()) { %>
                     <h2 class="pagetitle">Archive for <%= the_time("F jS, Y") %></h2>
                     <% /* If this is a monthly archive */ } else if (is_month()) { %>
                     <h2 class="pagetitle">Archive for <%= the_time("F, Y") %></h2>
                     <% /* If this is a yearly archive */ } else if (is_year()) { %>
                     <h2 class="pagetitle">Archive for <%= the_time("Y") %></h2>
                     <% /* If this is an author archive */ } else if (is_author()) { %>
                     <h2 class="pagetitle">Author Archive</h2>
                     <% } %>

						<hr />
						<%--<?php i_blog_posts(); ?>
						<?php if (have_posts()) : ?>--%>
						<div class="panel_left">

                     <% foreach (Post post in Model.Posts) { the_post(post); %>
							<div class="post">
							
								<h3> <em><%= comments_popup_link("0", "1", "%") %><span> comments for post:</span></em> <strong>&quot;</strong><a href="<%= the_permalink() %>" class="post_link"><%= the_title() %></a><strong>&quot;</strong> <ins>|</ins> <span>Posted on <%= the_time("d. m.") %> by	<a href="#author" class="author"><%= the_author() %></a></span></h3>
								
								<div class="content">
								
									<%--<?php the_post_thumbnail( 'blog-thumbnail', array('class' => "main" ) ); ?>--%>
									
									<%= the_content("") %>
									
									<div class="meta">Posted in <%= the_category(", ") %></div>
								</div>
																
								<a href="<%= the_permalink() %>" class="button green">Read more</a>
							
							</div>
                     <% } %>
							
							<%--<?php if(function_exists('wp_pagenavi')): ?>--%>
							<%= wp_pagenavi()%>
							<%--<div class="navigation">
			               <div class="alignleft"><%= next_posts_link("&laquo; Older Entries") %></div>
			               <div class="alignright"><%= previous_posts_link("Newer Entries &raquo;") %></div>
							</div>--%>

							<%--<?php endif; ?>	--%>						
													
						</div>
						
                  <% } else { %>
					
							<h2 class="center">Not Found</h2>
							<p class="center">Sorry, but you are looking for something that isn't here.</p>
							<% get_search_form(); %>
					
                  <% } %>

						
						<div class="panel_right">
						
							<div id="sidebar">
							
								<div id="sidebar_top">&nbsp;</div>
								
								<div id="sidebar_main">
								
									<%--<?php get_sidebar('blog'); ?>--%>
                           <% get_sidebar(); %>
									
								</div>
								
								<div id="sidebar_bottom">&nbsp;</div>
							
							</div>
						
						</div>
					
					</div>
					
				</div>
				
			</div>

        </div>
		
		<div class="scrollBottom round">&nbsp;</div>
		
		<!-- / coda slider -->
		
		<!-- bottom container -->
		
		<%--<?php get_sidebar('bottom'); ?>	--%>
				
		<!-- / bottom container -->
				
<% get_footer(); %>
