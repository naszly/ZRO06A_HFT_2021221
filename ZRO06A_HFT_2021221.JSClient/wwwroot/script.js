window.onload = (() => {
    const carEndpoint = "http://localhost:62730/car/";
    const brandEndpoint = "http://localhost:62730/brand/";
    const hub = "http://localhost:62730/hub/"

    let cars = [];
    let brands = [];

    let createBttn = document.getElementById("create_car");
    let updateBttn = document.getElementById("update_car");
    let table = document.getElementById("result");

    let selectedCar;

    function setupSignalR() {
        connection = new signalR.HubConnectionBuilder()
            .withUrl(hub)
            .configureLogging(signalR.LogLevel.Information)
            .build();

        connection.on("CarCreated", (user, message) => {
            getdata();
        });

        connection.on("CarDeleted", (user, message) => {
            getdata();
        });

        connection.on("CarUpdated", (user, message) => {
            getdata();
        });

        connection.onclose(async () => {
            await start();
        });
        start();

    }

    async function start() {
        try {
            await connection.start();
            console.log("SignalR Connected.");
        } catch (err) {
            console.log(err);
            setTimeout(start, 5000);
        }
    };

    async function getdata() {
        await fetch(carEndpoint)
            .then(x => x.json())
            .then(y => {
                cars = y;
                console.log(cars);
                displayTable();
            });
        await fetch(brandEndpoint)
            .then(x => x.json())
            .then(y => {
                brands = y;
                displaySelection();
            });
    }

    function displayTable() {
        table.innerHTML = "";
        for (var i = 0; i < cars.length; i++) {
            let row = document.createElement("tr");
            let id = cars[i].id;

            let removeBttn = document.createElement("button");
            let selectBttn = document.createElement("button");

            removeBttn.addEventListener("click", () => remove(id));
            selectBttn.addEventListener("click", () => select(id));
            removeBttn.innerHTML = "remove";
            selectBttn.innerHTML = "select";

            let td = document.createElement("td");
            td.appendChild(removeBttn);
            td.appendChild(selectBttn);

            row.innerHTML = `<td>${cars[i].id}</td><td>${cars[i].brand.name}</td><td>${cars[i].model}</td><td>${cars[i].basePrice}</td>`
            row.appendChild(td);

            table.appendChild(row);
        }
    }

    function displaySelection() {
        let selection = document.getElementById("brand");
        selection.innerHTML = "";
        for (var i = 0; i < brands.length; i++) {
            let option = document.createElement("option");
            option.value = brands[i].id;
            option.innerHTML = brands[i].name;
            selection.appendChild(option);
        }
    }

    function remove(id) {
        fetch(carEndpoint + id, {
            method: 'DELETE',
            headers: { 'Content-Type': 'application/json', },
            body: null
        })
            .then(response => response)
            .then(data => {
                console.log('Success:', data);
                getdata();
            })
            .catch((error) => { console.error('Error:', error); });

    }

    function select(id) {
        let car = cars.find(x => x.id == id);

        createBttn.hidden = true;
        updateBttn.hidden = false;
        table.hidden = true;

        document.getElementById('model').value = car.model;
        document.getElementById('price').value = car.basePrice;
        document.getElementById('brand').value = car.brandId;

        selectedCar = id;
    }

    function update() {
        let model = document.getElementById('model').value;
        let price = document.getElementById('price').value;
        let brand = document.getElementById('brand').value;

        fetch(carEndpoint, {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json', },
            body: JSON.stringify(
                { Id: selectedCar, Model: model, BasePrice: price, BrandId: brand })
        })
            .then(response => response)
            .then(data => {
                console.log('Success:', data);
                getdata();
            })
            .catch((error) => { console.error('Error:', error); });

        document.getElementById('model').value = "";
        document.getElementById('price').value = "";
        createBttn.hidden = false;
        updateBttn.hidden = true;
        table.hidden = false;
    }

    function create() {

        let model = document.getElementById('model').value;
        let price = document.getElementById('price').value;
        let brand = document.getElementById('brand').value;

        console.log(model, price, brand);
        fetch(carEndpoint, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json', },
            body: JSON.stringify(
                { Model: model, BasePrice: price, BrandId: brand })
        })
            .then(response => response)
            .then(data => {
                console.log('Success:', data);
                getdata();
            })
            .catch((error) => { console.error('Error:', error); });
    }

    getdata();
    setupSignalR();

    createBttn.addEventListener("click", () => create());
    updateBttn.addEventListener("click", () => update());
    createBttn.hidden = false;
    updateBttn.hidden = true;
    table.hidden = false;
});