//Variables
let submitButton;
let passenger_list;

//Triggers
//When doc is ready
document.addEventListener("DOMContentLoaded", function (event) {
  showPassengers();

  //Set vars after HTML is loaded
  submitButton = document.getElementById("submitPassanger");
  passenger_list = document.getElementById("passengers_list");
});


//Functions
function showPassengers() {
  var req = new XMLHttpRequest();

  req.open("GET", "http://127.0.0.1:4567/passengers", false);
  req.send();
  var list = passenger_list.getElementsByClassName("list")[0]
  list.innerHTML = ""
  json = eval(req.responseText)
  for (var i=0;i<json.length;i++) {
    pax = json[i]
    var elem = "<p> &bull; " + pax.firstName + " " + pax.lastName + " &lt;" + pax.email + "&gt; " +
      '<a onclick="if(confirm(\'Are you sure?\')) removePassenger(\'' + pax.id + '\');" href="#">&#10005;</a></p>';
    list.innerHTML += elem
  }
}

function removePassenger(id) {
  var req = new XMLHttpRequest()
  req.open("GET", "http://127.0.0.1:4567/remove_passenger?id=" + id, false)
  req.send()
  showPassengers()
}

submitButton.onclick = function(ev) {
  ev.preventDefault()

  var form = document.getElementsByTagName("form")[0]
  var first = form.elements.firstName.value
  var last = form.elements.lastName.value
  var email = form.elements.email.value

  if (first == "") {
    alert("First name is required");
  }

  if (last == "") {
    alert("Last name is required");
    return;
  }

  if (!(/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test(email))) {
    alert("Invalid email")
    return
  }

  var req = new XMLHttpRequest()
  req.open("GET", "http://127.0.0.1:4567/add_passenger?firstName=" + first + "&lastName=" + last + "&email=" + email, false)
  req.send()
  showPassengers()
  passenger_list.style.display = "block"
  document.getElementById("addPassenger").style.display = "none"
}

document.getElementsByClassName("add")[0].onclick = function() {
  passenger_list.style.display = "none"
  document.getElementById("addPassenger").style.display = "block"
};

