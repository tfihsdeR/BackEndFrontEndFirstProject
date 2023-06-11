function GetAllToDoList() {
    fetch("https://localhost:7099/api/todo")
        .then(response => response.json())
        .then(data => {
            if (data == null || data == "") {
                return alert("Data is empty!");
            }

            RemoveTableElements();

            let tHead = document.querySelector("div > .toDoList > .tHead");
            let tBody = document.querySelector("div > .toDoList > .tBody");

            const trHead = document.createElement("tr");
            trHead.innerHTML = "<th>Create Date</th><th>Update Date</th><th>Message</th><th></th><th></th>"
            tHead.appendChild(trHead);

            data.forEach(item => {
                const trBody = document.createElement("tr");
                trBody.innerHTML = `<td>${item.createDate}</td><td>${item.updateDate}</td><td><input value="${item.message}"></td><td><button class="btnUpdate" onclick="Update('${item.message}', this.parentNode.parentNode.querySelector('input').value)">Update</button></td><td><button class="btnDelete" onclick="RemoveByMessageToDo('${item.message}')">Delete</button></td>`;
                tBody.appendChild(trBody);
            })
        })
}

function CreateToDo(event) {
    event.preventDefault();
    let message = document.getElementById("messageCreate").value;

    fetch("https://localhost:7099/api/todo", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
            message: message
        })
    }).then(() => {
        RemoveTableElements();
        GetAllToDoList();
    })
}

function GetByMessageToDo(event) {
    event.preventDefault();
    let message = document.getElementById("messageGet").value;
    fetch(`https://localhost:7099/api/ToDo/${message}`)
        .then(response => response.json())
        .then(data => {
            if (data == null || data == "") {
                return alert("Data is empty!");
            }

            RemoveTableElements();

            let tHead = document.querySelector(".tHead");
            let tBody = document.querySelector(".tBody");

            const trHead = document.createElement("tr");
            trHead.innerHTML = "<th>Create Date</th><th>Update Date</th><th>Message</th><th></th><th></th>"
            tHead.appendChild(trHead);

            const trBody = document.createElement("tr");
            trBody.innerHTML = `<td>${data.createDate}</td><td>${data.updateDate}</td><td>${data.message}</td><td><button class="btnUpdate" onclick="Update('${data.message}', this.parentNode.parentNode.querySelector('input').value)">Update</button></td><td><button class="btnDelete" onclick="RemoveByMessageToDo('${data.message}')">Delete</button></td>`;
            tBody.appendChild(trBody);
        })
}

function RemoveTableElements() {
    let tHead = document.querySelector(".tHead");
    let tBody = document.querySelector(".tBody");
    while (tHead.firstChild) {
        tHead.removeChild(tHead.firstChild);
    }
    while (tBody.firstChild) {
        tBody.removeChild(tBody.firstChild);
    }
}

function RemoveByMessageToDo(message) {
    fetch(`https://localhost:7099/api/todo/${message}`, {
        method: "delete",
        headers: { "content-type": "application/json" }
    }).then(() => {
        GetAllToDoList();
        alert("Deleted!");
    })
}

function Update(oldMessage, newMessage) {
    fetch(`https://localhost:7099/api/todo/${oldMessage}`, {
        method: "put",
        headers: { "content-type": "application/json" },
        body: JSON.stringify({
            message: newMessage
        })
    }).then(() => {
        GetAllToDoList();
        alert("Updated!");
    })
}