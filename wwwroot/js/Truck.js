const uri = 'api/truck';
let trucks = [];

function getTrucks() {
    fetch(uri)
        .then(response => response.json())
        .then(data => _displayTrucks(data))
        .catch(error => console.error('Unable to get trucks.', error));
}

function addTruck() {
    const addNameTextbox = document.getElementById('add-name');

    const truck = {
        name: addNameTextbox.value.trim()
    };

    fetch(uri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(truck)
    })
        .then(response => response.json())
        .then(() => {
            getTrucks();
            addNameTextbox.value = '';
        })
        .catch(error => console.error('Unable to add truck.', error));
}

function deleteTruck(id) {
    fetch(`${uri}/${id}`, {
        method: 'DELETE'
    })
        .then(() => getTrucks())
        .catch(error => console.error('Unable to delete truck.', error));
}

function displayEditForm(id) {
    const truck = trucks.find(truck => truck.id === id);

    document.getElementById('edit-id').value = truck.id;
    document.getElementById('edit-name').value = truck.Name;
    document.getElementById('edit-make').value = truck.Make;
    if (truck.Year != 0) {
        document.getElementById('edit-year').value = truck.Year;
    }   
    document.getElementById('edit-color').value = truck.Color;
    document.getElementById('edit-horsepower').value = truck.Horsepower;
    document.getElementById('editForm').style.display = 'block';
}

function updateTruck() {
    const truckId = document.getElementById('edit-id').value;
    const truck = {
        id: truckId,
        name: document.getElementById('edit-name').value.trim(),
        make: document.getElementById('edit-make').value.trim(),
        year: document.getElementById('edit-year').value.trim(),
        color: document.getElementById('edit-color').value.trim(),
        horsepower: document.getElementById('edit-horsepower').value.trim(),
    };

    fetch(`${uri}/${truckId}`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(truck)
    })
        .then(() => getTrucks())
        .catch(error => console.error('Unable to update truck.', error));

    closeInput();

    return false;
}

function closeInput() {
    document.getElementById('editForm').style.display = 'none';
}

function _displayCount(truckCount) {
    const name = (truckCount === 1) ? 'truck' : 'trucks';

    document.getElementById('counter').innerText = `${truckCount} ${name}`;
}

function _displayTrucks(data) {
    const tBody = document.getElementById('trucks');
    tBody.innerHTML = '';

    _displayCount(data.length);

    const button = document.createElement('button');

    data.forEach(truck => {
        let editButton = button.cloneNode(false);
        editButton.innerText = 'Edit';
        editButton.setAttribute('onclick', `displayEditForm("${truck.id}")`);

        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute('onclick', `deleteTruck("${truck.id}")`);

        let tr = tBody.insertRow();

        let td1 = tr.insertCell(0);
        let textNode = document.createTextNode(truck.Name);
        td1.appendChild(textNode);

        let td2 = tr.insertCell(1);
        td2.appendChild(editButton);

        let td3 = tr.insertCell(2);
        td3.appendChild(deleteButton);
    });

    trucks = data;
}