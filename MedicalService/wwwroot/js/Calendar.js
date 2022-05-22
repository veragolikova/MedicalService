﻿let eventsArr = [];

let eventsTable = document.getElementById("eventsTable");


let trElems = eventsTable.getElementsByTagName("tr");
for (let tr of trElems)
{
    let tdElems = tr.getElementsByTagName("td");
    let eventObj =
    {
        id: tdElems[0].innerText,
        title: tdElems[1].innerText,
        start: tdElems[2].innerText,
    };
    eventsArr.push(eventObj);

}
let calendarEl = document.getElementById('calendar');

let calendar = new FullCalendar.Calendar(calendarEl,
    {
        initialView: 'dayGridMonth',
        headerToolbar: {
            left: 'prev, next today',
            center: 'title',
            right: 'dayGridMonth, timeGridWeek, timeGrid Day'
        },
        events: eventsArr,
    });

calendar.render();