const uri = 'api/car';
let cars = [];

function getCars() {
    fetch(uri)
        .then(response => response.json())
        .then(data => _displayCars(data))
        .catch(error => console.error('Unable to get cars.', error));
}

function addCar() {
    const addNameTextbox = document.getElementById('add-name');

    const car = {
        name: addNameTextbox.value.trim()
    };

    fetch(uri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(car)
    })
        .then(response => response.json())
        .then(() => {
            getCars();
            addNameTextbox.value = '';
        })
        .catch(error => console.error('Unable to add car.', error));
}

function deleteCar(id) {
    fetch(`${uri}/${id}`, {
        method: 'DELETE'
    })
        .then(() => getCars())
        .catch(error => console.error('Unable to delete car.', error));
}

function displayEditForm(id) {
    const car = cars.find(car => car.id === id);

    document.getElementById('edit-id').value = car.id;
    document.getElementById('edit-name').value = car.Name;
    document.getElementById('edit-make').value = car.Make;
    if (car.Year != 0) {
        document.getElementById('edit-year').value = car.Year;
    }   
    document.getElementById('edit-color').value = car.Color;
    document.getElementById('edit-type').value = car.Type;
    document.getElementById('editForm').style.display = 'block';
}

function updateCar() {
    const carId = document.getElementById('edit-id').value;
    const car = {
        id: carId,
        name: document.getElementById('edit-name').value.trim(),
        make: document.getElementById('edit-make').value.trim(),
        year: document.getElementById('edit-year').value.trim(),
        color: document.getElementById('edit-color').value.trim(),
        type: document.getElementById('edit-type').value.trim(),
    };

    fetch(`${uri}/${carId}`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(car)
    })
        .then(() => getCars())
        .catch(error => console.error('Unable to update car.', error));

    closeInput();

    return false;
}

function closeInput() {
    document.getElementById('editForm').style.display = 'none';
}

function _displayCount(carCount) {
    const name = (carCount === 1) ? 'car' : 'cars';

    document.getElementById('counter').innerText = `${carCount} ${name}`;
}

function _displayCars(data) {
    const tBody = document.getElementById('cars');
    tBody.innerHTML = '';

    _displayCount(data.length);

    const button = document.createElement('button');

    data.forEach(car => {
        let editButton = button.cloneNode(false);
        editButton.innerText = 'Edit';
        editButton.setAttribute('onclick', `displayEditForm("${car.id}")`);

        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute('onclick', `deleteCar("${car.id}")`);

        let tr = tBody.insertRow();

        let td1 = tr.insertCell(0);
        let textNode = document.createTextNode(car.Name);
        td1.appendChild(textNode);

        let td2 = tr.insertCell(1);
        td2.appendChild(editButton);

        let td3 = tr.insertCell(2);
        td3.appendChild(deleteButton);
    });

    cars = data;
}