var currentUpdateEvent;
var addStartDate;
var addEndDate;
var globalAllDay;

function updateEvent(event, element) {
    //alert( $('#'+element).attr('id'));

    if ($(this).data("qtip")) $(this).qtip("destroy");

    currentUpdateEvent = event;
    //$('#ischangedate').attr('checked', false);

    document.getElementById("ischangedate").checked = false;
    if (event.isreminder == true)
        document.getElementById("isReminderCheckUpdate").checked = true;

    hideStartEndContent();
    $('#displaystarttr').show();
    $('#displayendtr').show();


    $('#updatedialog').dialog('open');
    $("#eventName").val(event.title);
    $("#eventDesc").val(event.description);
    $("#eventId").val(event.id);
    // var formDate = $.fullCalendar.formatDate(event.start, 'MM-dd-yyyy');
    //  addStartDate.format("dd-MM-yyyy hh:mm:ss tt")

    $("#eventDesc").focus();
    if (event.color)
        $("#upbgcolor").val(event.color);
    else
        $("#upbgcolor").val("#800000");
    $("#eventStart").text("" + event.start.toLocaleString());

    if (event.url != "") {
        $("#alinktr").show();
        $("#urlLink").attr("href", event.url);
    }
    else {
        $("#alinktr").hide();
    }

    if (event.end === null) {
        $("#eventEnd").text("");
    }
    else {
        $("#eventEnd").text("" + event.end.toLocaleString());
    }

    if (event.createdDate != "") {

        $("#createdDatertr").show();
        $("#createdDate").text(event.createdDate);
    }
    else
        $("#createdDatertr").hide();

    if (event.modifiedDate != "") {

        $("#modifiedDatertr").show();
        $("#modifiedDate").text(event.modifiedDate);
    }
    else
        $("#modifiedDatertr").hide();

    //if (event.IsQuoteEvent ) {
    //    if (event.IsQuoteEvent == true) {
    //        $("#eventDesc").hide();
    //        $("#divDesc").show();
    //         //$(".ui-dialog-buttonset").hide();
    //        $("#ContentPlaceHolder1_displaynotesHistoryDiv").html(event.description);
    //    }
    //    else {
    //        // $(".ui-dialog-buttonset").show();
    //        $("#eventDesc").show();
    //        $("#divDesc").hide();
    //    }
    //    console.log(event.IsQuoteEvent);
    //}
    //else {
    //    // $(".ui-dialog-buttonset").show();
    //    $("#eventDesc").show();
    //    $("#divDesc").hide();
    //}

    $("#contactspan").text(""); $("#telephonespan").text(""); $("#addressspan").text("");
    $("#contacttr").hide(); $("#telphonetr").hide(); $("#addresstr").hide();

    if (event.companyId > 0) {

        if (event.contactbuilder) {
            $("#contacttr").show();
            $("#telphonetr").show();
            $("#addresstr").show();
            $("#contactspan").text(event.contactbuilder); $("#telephonespan").text(event.telephonebuilder); $("#addressspan").text(event.addressbuilder);
        }
    }




    var dateEndDatabase = dateNow;
    if ($("#eventEnd").text() != "")
        dateEndDatabase = new Date($("#eventEnd").text().replace(" GMT+0000", ""));

    $("#changeEventEndDate").datepicker({
        dateFormat: 'dd/mm/yy'
    }).datepicker('setDate', dateEndDatabase);

    var dateStartDatabase = new Date($("#eventStart").text().replace(" GMT+0000", ""));
    //alert(dateStartDatabase)

    $("#changeEventStartDate").datepicker({
        dateFormat: 'dd/mm/yy'
    }).datepicker('setDate', dateStartDatabase);


    return false;
}

function updateSuccess(updateResult) {
    //alert(window.location.href.split('?')[0]);
    window.location.href = window.location.href.split('?')[0];
    //location.reload();
    alert("Event Successfully Updated.");

}

function deleteSuccess(deleteResult) {
    //alert(deleteResult);
    window.location.href = window.location.href.split('?')[0];
    //location.reload();
    alert("Event Successfully Deleted.");

}

function addSuccess(addResult) {
    // if addresult is -1, means event was not added
    //    alert("added key: " + addResult);

    if (addResult != -1) {
        //$('#calendar').fullCalendar('renderEvent',
        //				{
        //				    title: $("#addEventName").val(),
        //				    start: addStartDate,
        //				    end: addEndDate,
        //				    id: addResult,
        //				    description: $("#addEventDesc").val(),
        //				    allDay: globalAllDay
        //				},
        //				true // make the event "stick"
        //			);


        //$('#calendar').fullCalendar('unselect');
        alert("Event Successfully Added.");
        location.reload();
    }
}

function UpdateTimeSuccess(updateResult) {
    //alert(updateResult);
    alert("Event Successfully Updated.");
    location.reload();


}

function UpdateTimeSuccessNoLoad(updateResult) {
    //alert(updateResult);
    //location.reload();
}

function selectDate(start, end, allDay) {

    $('#addDialog').dialog('open');
    $(".ui-dialog-buttonset").show();
    $("#addEventName").val('');
    $("#eventDesc").val('');
    $("#addEventStartDate").text("" + start.toLocaleString());
    $("#addEventEndDate").text("" + end.toLocaleString());
    document.getElementById("newischangedate").checked = false;
    document.getElementById("isReminderCheckAdd").checked = false;
    addStartDate = start;
    addEndDate = end;
    globalAllDay = allDay;
    //alert(allDay);
}

function updateEventOnDropResize(event, allDay) {

    //alert("allday: " + allDay);
    var eventToUpdate = {
        id: event.id,
        start: event.start
    };

    if (event.end === null) {
        eventToUpdate.end = eventToUpdate.start;
    }
    else {
        eventToUpdate.end = event.end;
    }

    var endDate;
    if (!event.allDay) {
        endDate = new Date(eventToUpdate.end + 60 * 60000);
        endDate = endDate.toJSON();
    }
    else {
        endDate = eventToUpdate.end.toJSON();
    }

    eventToUpdate.start = eventToUpdate.start.toJSON();
    eventToUpdate.end = eventToUpdate.end.toJSON(); //endDate;
    eventToUpdate.allDay = event.allDay;

    PageMethods.UpdateEventTime(eventToUpdate, UpdateTimeSuccess);
}

function eventDropped(event, dayDelta, minuteDelta, allDay, revertFunc) {
    if ($(this).data("qtip")) $(this).qtip("destroy");

    updateEventOnDropResize(event);
}

function eventResized(event, dayDelta, minuteDelta, revertFunc) {
    if ($(this).data("qtip")) $(this).qtip("destroy");

    updateEventOnDropResize(event);
}

function checkForSpecialChars(stringToCheck) {
    var pattern = /[^A-Za-z0-9 ]/;
    return pattern.test(stringToCheck);
}

function isAllDay(startDate, endDate) {
    var allDay;

    if (startDate.format("HH:mm:ss") == "00:00:00" && endDate.format("HH:mm:ss") == "00:00:00") {
        allDay = true;
        globalAllDay = true;
    }
    else {
        //allDay = false;
        globalAllDay = false;
    }

    return allDay;
}

function qTipText(start, end, description) {
    var text;

    if (end !== null)
        text = "<strong>Start:</strong> " + start.format("MM/DD/YYYY hh:mm T") + "<br/><strong>End:</strong> " + end.format("MM/DD/YYYY hh:mm T") + "<br/><br/>" + description;
    else
        text = "<strong>Start:</strong> " + start.format("MM/DD/YYYY hh:mm T") + "<br/><strong>End:</strong><br/><br/>" + description;

    return text;
}

function ValidateFields(title, description) {

    if (title == "")
        return false;

    if (description == "")
        return false;
    return true;

}

function isAlldayMyValidate(sDate, eDate) {

    var s = new Date(sDate);
    // alert('test 1');    var e = new Date(eDate);

    if (s.getDay() != e.getDay()) {
        // alert('test 1');
        return false;
    }
    // alert('test 1' + s.getHours() + "rrr" + e.getHours());
    if (s.getDay() == e.getDay()) {
        if (s.getHours() == e.getHours())
            return false;
        else
            return true;
    }


    return false;
}

$(document).ready(function () {
    // update Dialog
    $('#updatedialog').dialog({
        autoOpen: false,
        width: 740,
        buttons: {
            "update": function () {
                //alert(currentUpdateEvent.title);
                // alert($("#upbgcolor").val());

                var sDateset = "";
                var eDateset = "";

                if ($("#ischangedate").is(':checked')) {
                    sDateset = $("#changeEventStartDate").val();
                    eDateset = $("#changeEventEndDate").val();
                }

                var reminderset = false;
                if ($("#isReminderCheckUpdate").is(':checked')) {
                    reminderset = true;

                }

                var eventToUpdateDate = {
                    id: currentUpdateEvent.id,
                    start: sDateset,
                    end: eDateset,
                    allDay: false
                };
                var isQuote = true;
                if ($("#eventDesc").is(":visible")) {
                    isQuote = false;
                }
                var eventToUpdate = {
                    id: currentUpdateEvent.id,
                    title: $("#eventName").val(),
                    description: $("#eventDesc").val(),
                    color: $("#upbgcolor").val(),
                    isreminder: reminderset,
                    isquoteevent: isQuote
                };

                if (!ValidateFields(eventToUpdate.title, eventToUpdate.description)) {
                    alert("please enter name and description");
                }
                else {
                    if ($("#ischangedate").is(':checked')) {
                        PageMethods.UpdateEventTimeCustomDate(eventToUpdateDate, UpdateTimeSuccessNoLoad);
                    }
                    PageMethods.UpdateEvent(eventToUpdate, updateSuccess);
                    $(this).dialog("close");

                    //currentUpdateEvent.title = $("#eventName").val();
                    //currentUpdateEvent.description = $("#eventDesc").val();
                    //$('#calendar').fullCalendar('updateEvent', currentUpdateEvent);
                }

            },
            "delete": function () {

                if (confirm("do you really want to delete this event?")) {

                    PageMethods.deleteEvent($("#eventId").val(), deleteSuccess);
                    $(this).dialog("close");
                    // $('#calendar').fullCalendar('removeEvents', $("#eventId").val());
                }
            }
        }
    });

    //add dialog
    $('#addDialog').dialog({
        autoOpen: false,
        width: 700,
        buttons: {
            "Add": function () {
                //alert("sent:" + addStartDate.format("dd-MM-yyyy hh:mm:ss tt") + "==" + addStartDate.toLocaleString());
                var reminderset = false;
                if ($("#isReminderCheckAdd").is(':checked')) {
                    reminderset = true;

                }

                var eventToAdd = {
                    title: $("#addEventName").val(),
                    description: $("#addEventDesc").val(),
                    start: addStartDate.toJSON(),
                    end: addEndDate.toJSON(),
                    color: $("#addbgcolor").val(),
                    isreminder: reminderset,
                    allDay: isAllDay(addStartDate, addEndDate)
                };

                if (!ValidateFields(eventToAdd.title, eventToAdd.description)) {
                    alert("please enter name and description");
                }
                else {
                    //alert("sending " + eventToAdd.title);
                    if ($("#newischangedate").is(':checked')) {
                        PageMethods.addEvent(eventToAdd, addMethedGetId);

                    }
                    else {
                        PageMethods.addEvent(eventToAdd, addSuccess);
                    }

                    $(this).dialog("close");
                }
            }
        }
    });


    function addMethedGetId(res) {
        var snewDateset = "";
        var enewDateset = "";
        var resultId = 0;
        if ($("#newischangedate").is(':checked')) {
            snewDateset = $("#newchangeEventStartDate").val();
            enewDateset = $("#newchangeEventEndDate").val();
        }

        var resId = parseInt(res);
        if (resId > 0) {

            var eventNewToUpdateDate = {
                id: resId,
                start: snewDateset,
                end: enewDateset,
                allDay: false
            };
            PageMethods.UpdateEventTimeCustomDate(eventNewToUpdateDate, addSuccess);
        }
    }

    var date = new Date();
    var d = date.getDate();
    var m = date.getMonth();
    var y = date.getFullYear();
    var options = {
        weekday: "long", year: "numeric", month: "short",
        day: "numeric", hour: "2-digit", minute: "2-digit"
    };

    $('#calendar').fullCalendar({
        theme: true,
        header: {
            left: 'prevYear,nextYear,prev,next today',
            center: 'title',
            right: 'month,agendaWeek,agendaDay'
        },
        displayEventTime: false,
        defaultView: 'month',
        eventClick: updateEvent,
        eventDrop: eventDropped,
        eventResize: eventResized,
        eventOrder: "-start",
        weekends: false,
        selectable: true,
        selectHelper: true,
        select: selectDate,
        minTime: "08:00:00",
        maxTime: "18:00:00",
        editable: true,
        events: "DataHandlers/DisplayEventHandler.ashx",


        eventRender: function (event, element) {
            // alert(event.color);
            // element.style.color = "red";
            if (event.color == "#FFFF00")
                element.find('.fc-title').css("color", "black");
            element.find('.fc-title').append("<br/>" + event.description);
            //alert(event.title);
            //element.qtip({
            //    content: {
            //        text: qTipText(event.start, event.end, event.description),
            //        title: '<strong>' + event.title + '</strong>'
            //    },
            //    position: {
            //        my: 'bottom left',
            //        at: 'top right'
            //    },
            //    style: { classes: 'qtip-default  qtip qtip-tipped qtip-shadow qtip-rounded' }
            //});
        }
    });
});
