/// <summary>
/// admin.core
/// Core functions for the Control Panel
/// </summary>

// Global fields
var skiploading = 0; // usata col validator per evitare il blockUI se la validazione non passa
var ie6 = $.browser.msie && ($.browser.version == "6.0");


/// <summary>
/// Run First!
/// </summary>

// Redirect per IE6 to unsupported-browser.htm
if (ie6)
   window.location.href = '/unsupported-browser.htm';

// Blocca subito la UI durante il caricamento...
//blockUserInterface();
// ---------------------------------------------------------------------------
 
 
/// <summary>
/// On Document Ready
/// </summary>
$(document).ready(function() {

   //alert( getBrowserLanguage() ) ;

   // Site switcher
   if ( $("#site-switcher").is(':visible') ) {
      $("#site-switcher").hoverIntent({
         sensitivity: 2, // number = sensitivity threshold (must be 1 or higher)
         interval: 100, // number = milliseconds for onMouseOver polling interval
         over: showSiteSwitcherMenu, // function = onMouseOver callback (REQUIRED)
         timeout: 500, // number = milliseconds delay before onMouseOut
         out: showSiteSwitcherMenu // function = onMouseOut callback (REQUIRED)
      });
   }

   // hover on Site index ControlPanelIcons
   $("#site-chooser li").hover(
      function() {
         $(this).addClass("ui-state-hover");
      },
      function() {
         $(this).removeClass("ui-state-hover");
      }
   );

   // Form elements hover
   $(".ui-form-default ol li input, .ui-form-default ol li select, .ui-form-default ol li textarea")
	   .hover(
		   function() {
		      $(this).addClass("ui-form-hover");
		   },
		   function() {
		      $(this).removeClass("ui-form-hover");
		   }
	   );

   // setup button widget
   $(".button, .call-to-action, .coolbutton").button();


   // hover on the tables row
	$("table.grid tbody tr:not(.emptyrow, #reply-container)")
      .hover(
		   function() {
		      $(this).addClass("ui-state-highlight ui-state-hover-row");
		   },
		   function() {
		      $(this).removeClass("ui-state-highlight ui-state-hover-row");
		   }
      );



   // hover on the #utility column links
   $("#utility .section li a")
      .hover(
		   function() {
		      $(this).addClass("ui-state-hover");
		      $(this).addClass("ui-corner-all");
		   },
		   function() {
		      $(this).removeClass("ui-state-hover");
		      $(this).removeClass("ui-corner-all");
		   }
      );


   // Site MegaMenu under breadcrumbs
   if ( $("#breadcrumb").is(':visible') ) {
      // setup hoverIntent for Site Mega DropDown Menu
      var config = {
           sensitivity: 2, // number = sensitivity threshold (must be 1 or higher)
           interval: 100, // number = milliseconds for onMouseOver polling interval
           over: megaHoverOver, // function = onMouseOver callback (REQUIRED)
           timeout: 500, // number = milliseconds delay before onMouseOut
           out: megaHoverOut // function = onMouseOut callback (REQUIRED)
      };

      $("#breadcrumb li.home").hoverIntent(config);
   } 

   // block when ajax activity starts & unblock when stop
   //   $().ajaxStart(function() {
   //      blockUserInterface();
   //   })
   //      .ajaxStop(function() {
   //         $.unblockUI();
   //      });

   // Render messages with Growl
   if ($("#message-container").contents("div").length > 0) {
      showMessage('', false);
   }
   

   // ajax error common handler
   $(document).ajaxError(function(request, settings) {
      $.unblockUI();

      // Log su Firebug se presente
      if (window.console && window.console.error) {
         console.error(request);
         console.error(settings);
         console.trace();
         
         // check if the error was caused by a HTTP 403
         if (settings != null && settings.status == 403)
            alert("Sorry, you are not authorized to perform this operation.");
         else
            alert(request);
      }
      else {
         if (settings != null && settings.status == 403)
            alert("Sorry, you are not authorized to perform this operation.");
         else
            alert("Ooops, sorry but an unexptected error occured.\nRetry, but if the problem persist contact us!");
      }
   });

   // async load of the gravatar image
   loadCurrentUserGravatar();
   
   // Closing Loading-mask
   //   $.unblockUI();
});



function startTutorial() {
   // dinamically load the tutorial script
   $.getScript('/Resources/js/src/admin.tutorial.js', function() {
      runTutorial();
   });      

   return false;
}


/// <summary>
/// Usa jquery.blockUI per generare un overlay full screen per prevenire l'attivit� dell'utente
/// finch� la pagina non stata completamente caricata
/// </summary>
//function blockUserInterface() {
////   $.blockUI({
//      message: '<div class="loading-indicator ui-widget ui-state-default ui-corner-all">Loading...<img src="/Resources/img/ajax-loader-bar.gif" /></div>', //$('#loading'),
//      overlayCSS: {
//         backgroundColor: '#6EA7D1',
//         opacity: '0.9'
//      },
//      css: {
//         width: '20%',
//         left: '40%',
//         border: '2px solid #E4F1FC'
//      }
//   });
//}


//function blockUserInterfaceOnSubmit()
//{
//   if (skiploading == 0)
//      blockUserInterface();
//}

function getBrowserLanguage() {
   return normaliseLang(navigator.language /* Mozilla */ ||	navigator.userLanguage /* IE */);
}

/* Ensure language code is in the format aa-AA. */
function normaliseLang(lang) {
	lang = lang.replace(/_/, '-').toLowerCase();
	if (lang.length > 3) {
		lang = lang.substring(0, 3) + lang.substring(3).toUpperCase();
	}
	return lang;
}



/// <summary>
/// Show a growl message. 
/// The message html passed as argument will be put in the #message-container element
/// </summary>
function showMessage(msg, sticky) {

   if (!_isNull(sticky))
      sticky = true;

   if (!_isNull(msg))
      $("#message-container").empty().append(msg);

   var iserror = $("#message-container div.message div").hasClass("ui-state-error") ? true : false;
   growl( $("#message-container .message p").html(), iserror, sticky );
}



function growl(msg, iserror, sticky) {

   var stateClass = iserror ? "ui-state-error" : "ui-state-highlight";
   
   if (iserror)
      sticky = true;
   
   $.jGrowl(msg, { 
      sticky: sticky, 
      theme: stateClass + " ui-shadow",
      life: 7000
   });
}



// show the site switcher menu
function showSiteSwitcherMenu() {
   // determine if already opened
   if ( $('#site-chooser').is(':hidden') ) {
      $("#site-switcher").addClass("ui-state-default ui-corner-top");
      $('#site-chooser').css({ 'left': 0 });
      $('#site-chooser').stop().fadeTo('fast', 1).show(); 
   } else {
      $('#site-chooser').hide();
      $("#site-switcher").removeClass("ui-state-default ui-corner-top");
   }
}



//On Hover Over
function megaHoverOver(){
   var megamenuIndex = "#sitemegamenu";

   var rowWidth = 0;
   //Calculate width based on the contained ul width
   $(megamenuIndex).find("ul").each(function() {					
	   rowWidth += parseInt($(this).css('width'));
	});

   // set position
   $(megamenuIndex).css({ 'left': $(this).offset().left - (rowWidth / 2) });
   //$(megamenuIndex).css({ 'left': 0 });
   
   $(megamenuIndex).css({'width': (rowWidth + 42)});
   $(megamenuIndex).stop().fadeTo('fast', 1).show(); //Find sub and fade it in
   $(this).addClass("ui-state-default ui-corner-top");
}


//On Hover Out
function megaHoverOut() {
   //$(this).removeClass("ui-state-hover");
   $(this).removeClass("ui-state-default ui-corner-top");

   var megamenuIndex = "#sitemegamenu";
   
   if (megamenuIndex == '#')
      return false;
      
   $(megamenuIndex).stop().fadeTo('fast', 0, function() { //Fade to 0 opactiy
      $(this).hide();  //after fading, hide it
   });
}



/// <summary>
/// IsNull
/// </summary>
function _isNull(i) {
   return (i == null || i == 'null' || i == '' || i == 'undefined');
}



/// <summary>
/// Load in background the Gravatar image
/// </summary>
function loadCurrentUserGravatar() {

   var img = new Image();
   
   // use the 'load' event to run code after the image has been loaded
   $(img).load( function() {
      // hide first
      $(this).css('display','none'); // since .hide() failed in safari
      $("#current-user-gravatar").after(this);
      $("#current-user-gravatar").fadeOut(function(){
         $(img).fadeIn();
      });
   })
   .error(function(xhr, textStatus, errorThrown) {
	   $(this).attr("src", "/Resources/img/gravatar-default-32x32.png");
   })
   .attr('src', $("#user-gravatar-url").val()); // this will load the image
    
}

