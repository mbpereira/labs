import { HubConnectionBuilder } from '@microsoft/signalr'

const connection = new HubConnectionBuilder().withUrl("https://localhost:44314/chatHub").build();

const registerEvents = store => {
    connection.start({ withCredentials: false }).then(function () {
        connection.on("ReceiveSignal", function (user, message) {
            store.commit("addMessage", { from: user, content: message })     
        });
    }).catch(function (err) {
        return console.error(err.toString());
    });
}

export default registerEvents
