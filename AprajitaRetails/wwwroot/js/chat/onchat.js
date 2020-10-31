"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/Chat/ARChat/Index").build();
document.getElementById("submitButton").disabled = true;

connection.on("receiveMessage", addMessageToChat);


connection.start().then(function () {
    document.getElementById("submitButton").disabled = false;
}).catch(function (err) {
    return console.error(err);
});


// userName is declared in razor page.
const username = userName;
const textInput = document.getElementById("messageText");
const whenInput = document.getElementById("when");
const chat = document.getElementById("chat");
const messagesQueue = [];

function clearInputField() {
    messagesQueue.push(textInput.value);
    textInput.value = "";
}

function sendMessage() {
    let text = messagesQueue.shift() || "";
    if (text.trim() === "") return;

    let when = new Date();
    let message = new Message(username, text, when);
    connection.invoke('sendMessage', message);
   
}

function addMessageToChat(message) {
    let isCurrentUserMessage = message.userName === username;

    let container = document.createElement('div');
    container.className = isCurrentUserMessage ? "container darker" : "container";

    let sender = document.createElement('p');
    sender.className = "sender";
    sender.innerHTML = message.userName;
    let text = document.createElement('p');
    text.innerHTML = message.text;

    let when = document.createElement('span');
    when.className = isCurrentUserMessage ? "time-left" : "time-right";
    var currentdate = new Date();
    when.innerHTML =
        (currentdate.getMonth() + 1) + "/"
        + currentdate.getDate() + "/"
        + currentdate.getFullYear() + " "
        + currentdate.toLocaleString('en-US', { hour: 'numeric', minute: 'numeric', hour12: true })

    container.appendChild(sender);
    container.appendChild(text);
    container.appendChild(when);
    chat.appendChild(container);
}

class Message {
    constructor(username, text, when) {
        this.userName = username;
        this.text = text;
        this.when = when;
    }
}

