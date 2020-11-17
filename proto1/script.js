window.onload = start;
let textBox, deltaBox;
let startPos = false;
let scaling = 500000;
let windowMid;

function start(){

  windowMid = {
    x:window.innerWidth/2,
    y:window.innerHeight/2
  };

  textBox = document.createElement("div");
  document.body.appendChild(textBox);

  deltaBox = document.createElement("div");
  document.body.appendChild(deltaBox);

 getLocation();
}

function getLocation() {
  if (navigator.geolocation) {
    navigator.geolocation.watchPosition(showPosition);
  } else {
    textBox.innerHTML = "Geolocation is not supported by this browser.";
  }
}

// called on geo location change
function showPosition(position) {
  textBox.innerHTML = "Latitude: " + position.coords.latitude +
  "<br>Longitude: " + position.coords.longitude;

  if(startPos==false){
    startPos = {
      lat:position.coords.latitude,
      lon:position.coords.longitude
    };
  }

  markPoint(position.coords.latitude,position.coords.longitude);
}


function markPoint(lat,lon){

  let delta = {
    lon: startPos.lat - lat,
    lat: startPos.lon - lon
  }

  deltaBox.innerHTML = "delta Latitude: "+delta.lat
  + "<br> delta Longitude: "+delta.lon;

  let x = windowMid.x + delta.lon * scaling;
  let y = windowMid.y + delta.lat * scaling;

  console.log(windowMid,delta);
  let result = document.createElement("div");
  result.setAttribute("style",`
  position:fixed;
  width:10px;
  height:10px;
  background-color:#3335;
  left:${x}px;
  top:${y}px;
  `);

  document.body.appendChild(result);
}
