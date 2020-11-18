window.onload = start;
let textBox, deltaBox;
let startPos = false;
let scaling = 1000000;
let windowMid;
let stampColor = {
  r: 0,
  g: 0,
  b: 0
}

let markNextPoint= false;
let counter =0;
function mousePressed(){
  markNextPoint = true;
}

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

  let x = windowMid.x + delta.lon * scaling;
  let y = windowMid.y + delta.lat * scaling;

  deltaBox.innerHTML = "delta Latitude: "+delta.lat
  + "<br> delta Longitude: "+delta.lon
  + "<br> new x: "+x
  + "<br> new y: "+y;

  console.log(windowMid,delta);
  if( stampColor.r < 180 ) stampColor.r += 15;
  else if(stampColor.g<180) stampColor.g += 15;
  else if(stampColor.b<180) stampColor.b += 15;
  else {
    stampColor.r = 0;
    stampColor.g =0;
    stampColor.b =0;
  }

  let result = document.createElement("div");
  result.setAttribute("style",`
  position:fixed;
  width:10px;
  font-size:30px;
  height:10px;
  background-color:rgba(${stampColor.r},${stampColor.g},${stampColor.b},0.3);
  left:${x}px;
  top:${y}px;
  `);
  if(markNextPoint){
    result.innerHTML = counter;
    counter++;
  }
  markNextPoint = false;
  document.body.appendChild(result);
}
