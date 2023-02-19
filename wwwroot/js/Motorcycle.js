const uri = 'api/motorcycle';
let motorcycles = [];

function getMotorcycles() {
    fetch(uri)
        .then(response => response.json())
        .then(data => _displayMotorcycles(data))
        .catch(error => console.error('Unable to get motorcycles.', error));
}

function addMotorcycle() {
    const addNameTextbox = document.getElementById('add-name');

    const motorcycle = {
        name: addNameTextbox.value.trim()
    };

    fetch(uri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(motorcycle)
    })
        .then(response => response.json())
        .then(() => {
            getMotorcycles();
            addNameTextbox.value = '';
        })
        .catch(error => console.error('Unable to add motorcycle.', error));
}

function deleteMotorcycle(id) {
    fetch(`${uri}/${id}`, {
        method: 'DELETE'
    })
        .then(() => getMotorcycles())
        .catch(error => console.error('Unable to delete motorcycle.', error));
}

function displayEditForm(id) {
    const motorcycle = motorcycles.find(motorcycle => motorcycle.id === id);

    document.getElementById('edit-id').value = motorcycle.id;
    document.getElementById('edit-name').value = motorcycle.Name;
    document.getElementById('edit-make').value = motorcycle.Make;
    if (motorcycle.Year != 0) {
        document.getElementById('edit-year').value = motorcycle.Year;
    }   
    document.getElementById('edit-color').value = motorcycle.Color;
    document.getElementById('editForm').style.display = 'block';
}

function updateMotorcycle() {
    const motorcycleId = document.getElementById('edit-id').value;
    const motorcycle = {
        id: motorcycleId,
        name: document.getElementById('edit-name').value.trim(),
        make: document.getElementById('edit-make').value.trim(),
        year: document.getElementById('edit-year').value.trim(),
        color: document.getElementById('edit-color').value.trim()
    };

    fetch(`${uri}/${motorcycleId}`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(motorcycle)
    })
        .then(() => getMotorcycles())
        .catch(error => console.error('Unable to update motorcycle.', error));

    closeInput();

    return false;
}

function closeInput() {
    document.getElementById('editForm').style.display = 'none';
}

function _displayCount(motorcycleCount) {
    const name = (motorcycleCount === 1) ? 'motorcycle' : 'motorcycles';

    document.getElementById('counter').innerText = `${motorcycleCount} ${name}`;
}

function _displayMotorcycles(data) {
    const tBody = document.getElementById('motorcycles');
    tBody.innerHTML = '';

    _displayCount(data.length);

    const button = document.createElement('button');

    data.forEach(motorcycle => {
        let editButton = button.cloneNode(false);
        editButton.innerText = 'Edit';
        editButton.setAttribute('onclick', `displayEditForm("${motorcycle.id}")`);

        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute('onclick', `deleteMotorcycle("${motorcycle.id}")`);

        let tr = tBody.insertRow();

        let td1 = tr.insertCell(0);
        let textNode = document.createTextNode(motorcycle.Name);
        td1.appendChild(textNode);

        let td2 = tr.insertCell(1);
        td2.appendChild(editButton);

        let td3 = tr.insertCell(2);
        td3.appendChild(deleteButton);
    });

    motorcycles = data;
}