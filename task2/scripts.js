//Variables
let passenger_list = document.getElementById("passengers_list");
let submitButton = document.getElementById("submitPassanger");
let AddPassangerButton = document.getElementById("addPassanger");
let list = document.getElementById("passangerList");

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

  list.innerHTML = "<p>Loading passanger list...</p>"
  
  let XMLRequest = new XMLHttpRequest();
  XMLRequest.open("GET", "http://127.0.0.1:4567/passengers", true);
  XMLRequest.onload = function (e) {
    if (XMLRequest.readyState === 4) {
      if (XMLRequest.status === 200) {
        list.innerHTML = ""
        
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

  list.innerHTML = "<p>Removing passanger...</p>"
  let XMLRequest = new XMLHttpRequest();
  XMLRequest.open("GET", `http://127.0.0.1:4567/remove_passenger?id=${id}`, true);
  XMLRequest.onload = function (e) {
    if (XMLRequest.readyState === 4) {
      if (XMLRequest.status === 200) {
        showPassengers();
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

function submitPassanger(){
  let form = document.getElementById("addPassangerForm");
  let first = form.elements.firstName.value
  let last = form.elements.lastName.value
  let email = form.elements.email.value

  if (ValidateName(first) == 0) {
    alert("First name is required");
    return
  } else if (ValidateName(name) == 1){
    alert("A valid First name is required");
    return
  }


  if (ValidateName(last) == 0) {
    alert("Last name is required");
    return
  } else if (ValidateName(last) == 1) {
    alert("A valid Last name is required");
    return
  }

  if (!(/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test(email))) {
    alert("Invalid email")
    return
  }


  passenger_list.style.display = "block"
  document.getElementById("addPassenger").style.display = "none"
  list.innerHTML = "<p>Adding passanger...</p>"
  let XMLRequest = new XMLHttpRequest();
  XMLRequest.open("GET", `http://127.0.0.1:4567/add_passenger?firstName=${first}&lastName=${last}&email=${email}`, true);
  XMLRequest.onload = function (e) {
    if (XMLRequest.readyState === 4) {
      if (XMLRequest.status === 200) {
        showPassengers();
      } else {
        console.error(XMLRequest.statusText);
      }
    }
  };
  XMLRequest.onerror = function (e) {
    console.error(XMLRequest.statusText);
  };
  XMLRequest.send(null);
};


function ValidateName(name){
  if(name == ""){
    return 0; // field empty
  } else if ((/[0-9]/.test(name))){
    return 1; // invalid input
  } else return 2
}