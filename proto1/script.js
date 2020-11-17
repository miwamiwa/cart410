window.onload = start;
let textBox;

function start(){
 textBox = document.createElement("div");
 document.body.appendChild(textBox);

 getLocation();
}

function getLocation() {
  if (navigator.geolocation) {
    navigator.geolocation.watchPosition(showPosition);
  } else {
    textBox.innerHTML = "Geolocation is not supported by this browser.";
  }
}
function showPosition(position) {
  textBox.innerHTML = "Latitude: " + position.coords.latitude +
  "<br>Longitude: " + position.coords.longitude;
}
