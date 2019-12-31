$( document ).ready(function()
{console.log( "I am ready!" ); //when document is ready the console log will have this fired message

// pop up images on click in best obs ssection

	$(".bestobs #DRACO").click(function(){ //selects class and id of element. provides a click function
        $(".astro-pop_up1").delay(100).fadeIn(100,function(){ //gets .astro-pop_up1 element and delays then fades in
			console.log('animation complete') //gives details to console log to show it fired the click event
			}
			);
    });
    $(".closeBtn").click(function(){ // on click of cancel button fadeout over 100 milliseconds
        $(".astro-pop_up1").fadeOut(100 );
	});


	$(".bestobs #EUDODUS").click(function(){
        $(".astro-pop_up2").delay(100).fadeIn(100,function(){
			console.log('animation complete')
			}
			);
    });
    $(".closeBtn").click(function(){
        $(".astro-pop_up2").fadeOut(100 );
	});


	$(".bestobs #ELEPHANTS").click(function(){
        $(".astro-pop_up3").delay(100).fadeIn(100,function(){
			console.log('animation complete')
			}
			);
    });
    $(".closeBtn").click(function(){
        $(".astro-pop_up3").fadeOut(100 );
	});


	$(".bestobs #MESSIER").click(function(){
        $(".astro-pop_up4").delay(100).fadeIn(100,function(){
			console.log('animation complete')
			}
			);
    });
    $(".closeBtn").click(function(){
        $(".astro-pop_up4").fadeOut(100 );
	});


// checks to see if the planet items are in order and if ot will provide feedback to the player

   $('#sortableContainer').sortable(); //defines the sortable container

   $('#thesolarsystem').click(function() { //onclick of the solar system tag the loop will run to check if planets are in order
   var itemOrder = $('#sortableContainer').sortable("toArray");
   for (var i = 0; i < itemOrder.length; i++) { //iteratres through the array to the length of the 'sortablecontainer' list

            var text; //sets the variable for the text output

            if(i != itemOrder[i]) //if 'i' is not equal to the ID of the planet (set as a number here) then it will display text and greak out fo loop
            {
            text = "Sorry. Please try again !";
            document.getElementById("yourscore").innerHTML = text; //shows text before breakign out of loop
            return false; //breaks our of loop when it sees an item out of order
            }
            else //displays this text when the right ordre is shown
            {
            text = "Well done. That's correct !";
            }
            document.getElementById("yourscore").innerHTML = text; //inserts 'text' into the yourscore ID element.

        }
    })


});













