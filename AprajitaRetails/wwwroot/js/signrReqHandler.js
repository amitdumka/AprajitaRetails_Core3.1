﻿var connection = new signalR.HubConnectionBuilder()
    .withUrl('/Chat/ARChat/Index')
    .build();

connection.on('receiveMessage', addMessageToChat);

connection.start()
    .catch(error => {
        console.error(error.message);
    });

function sendMessageToHub(message) {
    connection.invoke('sendMessage', message)
}