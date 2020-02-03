
//slider display of photos

var slideIndex = 1;
showDivs(slideIndex); //carry out funtion showDivs below

function plusDivs(n) {
  showDivs(slideIndex += n);
}

function showDivs(n) {
  var i;
  var x = document.getElementsByClassName("mySlides"); //get element of mySlides and asign to x
  if (n > x.length) {slideIndex = 1}    //check slide index against n
  if (n < 1) {slideIndex = x.length}
  for (i = 0; i < x.length; i++) { //iterate through length of slides and increment i
     x[i].style.display = "none";  //hide display
  }
  x[slideIndex-1].style.display = "block";  //show last display
}


