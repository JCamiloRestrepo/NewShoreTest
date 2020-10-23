//Carousel
document.addEventListener('DOMContentLoaded', function () {
  var elems = document.querySelectorAll('.carousel', {
    fullWidth: true
  });
  var instance = M.Carousel.init(elems, {
    duration: 100,
  });
});
function ShowCarousel() {
  let elem = querySelector("#Slider")
  let instance = M.Carousel.getInstance(elem);
  instance.open();
}
//Side
document.addEventListener('DOMContentLoaded', function () {
  var elems = document.querySelectorAll('.sidenav');
  var instances = M.Sidenav.init(elems, {
    edge: "left"
  });
});

function Showsidenav() {
  let elem = querySelector("#slide-out")
  let instance = M.Sidenav.getInstance(elem);
  instance.open()
}
//Slider
document.addEventListener('DOMContentLoaded', function () {
  let elems = document.querySelectorAll('.slider');
  let instances = M.Slider.init(elems, {
    indicators: false,
    height: 500,
    duration: 500,
    interval: 3000
  });
});

function ShosSlider() {
  let elm = querySelector("#slides")
  let instance = M.Slider.getInstance(elem);
  instance.open()
}


