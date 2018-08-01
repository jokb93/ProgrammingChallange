//Variables
let passenger_list = document.getElementById("passengers_list");
let submitButton = document.getElementsByTagName("button")[0];
let AddPassangerButton = document.getElementsByClassName("add")[0];
let list = passenger_list.getElementsByClassName("list")[0];

//Triggers
submitButton.onclick = function (ev) {
  ev.preventDefault();
  submitPassanger();
}

showPassengers();

AddPassangerButton.onclick = function () {
  passenger_list.style.display = "none"
  document.getElementById("addPassenger").style.display = "block"
};


//Functions
function showPassengers() {

  list.innerHTML = ""

  let XMLRequest = new XMLHttpRequest();
  XMLRequest.open("GET", "http://127.0.0.1:4567/passengers", true);
  XMLRequest.onload = function (e) {
    if (XMLRequest.readyState === 4) {
      if (XMLRequest.status === 200) {
        
        let jsonObject = JSON.parse(XMLRequest.responseText);

        for (var i = 0; i < jsonObject.length; i++) {

          let firstName = jsonObject[i].firstName;
          let lastName = jsonObject[i].lastName;
          let email = jsonObject[i].email;
          let id = jsonObject[i].id;

          var elem = `<p> &bull; ${firstName} ${lastName} &lt;${email}&gt;
            <a onclick="if(confirm(\'Are you sure?\')) removePassenger(\'${id}\');" href="#">&#10005;</a></p>`;
          list.innerHTML += elem
        }

      } else {
        console.error(XMLRequest.statusText);
      }
    }
  };
  XMLRequest.onerror = function (e) {
    console.error(XMLRequest.statusText);
  };
  XMLRequest.send(null);
}

function removePassenger(id) {
  var req = new XMLHttpRequest()
  req.open("GET", "http://127.0.0.1:4567/remove_passenger?id=" + id, false)
  req.send()
  showPassengers()
}

function submitPassanger(){
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
};
