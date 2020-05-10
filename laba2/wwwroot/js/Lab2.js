const uri = 'api/Rooms';
let rooms = [];
function getRooms() {
    fetch(uri)
        .then(response => response.json())
        .then(data => _displayRooms(data))
        .catch (error => console.error('Unable to get room', error));
}
function addRoom() {
    const addDescriptionTextbox = document.getElementById('aaa');
    const addNumberTextbox = document.getElementById('add-number');
    const addPriceTextbox = document.getElementById('add-price');
    const room = {
        description: addDescriptionTextbox.value.trim(),
        number: parseInt(addNumberTextbox.value.trim()),
        price: parseInt(addPriceTextbox.value.trim()),
    };
    fetch(uri, {
        method: 'POST',
    headers: {
            'Accept':'application/json',
            'Content-Type':'application/json'
    },
    body: JSON.stringify(room)
})
        .then(response => response.json())
        .then(() => {
            getRooms();
            addDescriptionTextbox.value = '';
            addNumberTextbox.value = '';
            addPriceTextbox.value = '';

})
        .catch (error => console.error('Unable to add room', error));
}
function deleteRoom(id) {
    fetch(`${uri}/${id}`, {
        method: 'DELETE'
})
        .then(() =>getRooms())
        .catch (error => console.error('Unable to delete room', error));
}
function displayEditForm(id) {
    const room = rooms.find(room => room.roomID === id);
    document.getElementById('id').value = id;
    document.getElementById('edit-description').value = room.description;
    document.getElementById('edit-number').value = room.number;
    document.getElementById('edit-price').value = room.price;
    document.getElementById('editForm').style.display ='block';
}
function updateRoom() {
    const roomId = document.getElementById('id').value;

    const room = {
        roomID: parseInt(rooomId, 10),
        description: document.getElementById('edit-description').value.trim(),
        number: document.getElementById('edit-number').value.trim(),
        price: document.getElementById('edit-price').value.trim()
};
fetch(`${uri}/${roomId}`, {
    method: 'PUT',
headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
},
body: JSON.stringify(room)
    })

        .then(() => getRooms())
        .catch (error => console.error('Unable to update room', error));
closeInput();
return false;
}
function closeInput() {
    document.getElementById('editForm').style.display = 'none';
}
function _displayRooms(data) {
    const tBody = document.getElementById('rooms');
    tBody.innerHTML ='';

    const button = document.createElement('button');
    data.forEach(room => {
        let editButton = button.cloneNode(false);
        editButton.innerText = 'Edit';
        editButton.setAttribute('onclick', `displayEditForm(${room.roomID})`);
        let deleteButton = button.cloneNode(false);
        deleteButton.innerText ='Delete';
        deleteButton.setAttribute('onclick', `deleteRoom(${room.roomID})`);
        let tr = tBody.insertRow();

        let td1 = tr.insertCell(0);
        let textNode = document.createTextNode(room.description);
        td1.appendChild(textNode);
        let td2 = tr.insertCell(1);
        let textNodeInfo = document.createTextNode(room.number);
        td2.appendChild(textNodeInfo);
        let td5 = tr.insertCell(2);
        let textNodeInfo1 = document.createTextNode(room.price);
        td5.appendChild(textNodeInfo1);
        let td3 = tr.insertCell(3);
        td3.appendChild(editButton);
        let td4 = tr.insertCell(4);
        td4.appendChild(deleteButton);
    });
    rooms = data;
}
