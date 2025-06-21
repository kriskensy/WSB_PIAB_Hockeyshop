// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.addEventListener("DOMContentLoaded", function () {
    const btn = document.getElementById("messagesBtn");
    const badge = document.getElementById("messagesBadge");

    //pobiera liczbę nieprzeczytanych na starcie
    fetch('/api/notifications/unread-count') 
        .then(response => response.json())
        .then(unreadCount => {
            if (unreadCount > 0) {
                btn.classList.remove("d-none");
                badge.textContent = unreadCount;
            } else {
                btn.classList.add("d-none");
            }
        });

    //obsługa powiadomień w czasie rzeczywistym
    const connection = new signalR.HubConnectionBuilder()
        .withUrl("/notificationHub")
        .build();

    connection.on("ReceiveMessageNotification", function (unreadCount) {
        if (unreadCount > 0) {
            btn.classList.remove("d-none");
            badge.textContent = unreadCount;
        } else {
            btn.classList.add("d-none");
        }
    });

    connection.start()
        .then(function () {
            console.log("SignalR connection started (site.js)");
        })
        .catch(function (err) {
            console.error("SignalR error:", err.toString());
        });
});
