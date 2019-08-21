<%@ Page Title="" Language="C#" MasterPageFile="~/SiteInternalNavLevel1.Master" AutoEventWireup="true" CodeBehind="Calendar.aspx.cs" Inherits="DeltoneCRM.Calendar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

   
    <link href="Scripts/fullcalendar-3.2.0/fullcalendar.min.css" rel="stylesheet" />
    
    <link href="Scripts/fullcalendar-3.2.0/fullcalendar.print.css" rel='stylesheet' media='print' />
    <script src='Scripts/fullcalendar-3.2.0/lib/moment.min.js'></script>
     <script src="Scripts/fullcalendar-3.2.0/lib/jquery.min.js"></script>
    <script src="Scripts/fullcalendar-3.2.0/fullcalendar.min.js"></script>
    <link rel="stylesheet" type="text/css" href="//fonts.googleapis.com/css?family=PT+Serif:400,700,400italic|Open+Sans:700,400" />
    <link href="//fonts.googleapis.com/css?family=Carrois+Gothic" rel="stylesheet" type="text/css" />
    <script src="Scripts/fullcalendar-3.2.0/lib/jquery-ui.min.js"></script>
    <script src="Scripts/bootstrap-3.3.7-dist/js/bootstrap.min.js"></script>
    <link href="Scripts/bootstrap-3.3.7-dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="Scripts/fullcalendar-3.2.0/lib/cupertino/jquery-ui.min.css" rel="stylesheet" />
       <script src="Scripts/bootstrap-3.3.7-dist/js/bootstrap-modal.js"></script>
    <script src="Scripts/bootstrap-3.3.7-dist/js/bootstrap-popover.js"></script>
    <script src="Scripts/bootstrap-3.3.7-dist/js/bootstrap-tooltip.js"></script>
  <script>
      var eventsCal = "";
      var events = [];
      function generateevents() {
          var events = [];
          console.log(eventsCal.length);
          for (var i = 0; i < eventsCal.length; i++) {

              events.push({
                  title: eventsCal[i].title,
                  start: eventsCal[i].start,
                  end: eventsCal[i].end,

              });
          }

          console.log(events);

      }
      $(document).ready(function () {

          function Event() {
              this.id;
              this.title;
              this.start;
              this.end;
              this.allDay;
          };



          var sourceFullView = { url: '/Home/GetDiaryEvents/' };
          var sourceSummaryView = { url: '/Home/GetDiarySummary/' };
          var CalLoading = true;

          

          $.ajax({
              url: 'Fetch/FetchCalendarEvents.aspx',
            
              contentType: "application/json; charset=utf-8",
              dataType: "json",
              success: function (response) {
                  console.log(response.length);
                  for (var i = 0; i < response.length; i++) {
                      var obj = new Event();
                      obj.id = response[i].id;
                      obj.title = response[i].title;
                      obj.start = response[i].start;
                      obj.id = response[i].allDay;
                      console.log(obj);
                      events.push(obj);
                  }

                  CreateCalendar(events);

              }
          });

          console.log(events);

        

          CalLoading = false;


      });

      $('#btnInit').click(function () {
          $.ajax({
              type: 'POST',
              url: "/Home/Init",
              success: function (response) {
                  if (response == 'True') {
                      $('#calendar').fullCalendar('refetchEvents');
                      alert('Database populated! ');
                  }
                  else {
                      alert('Error, could not populate database!');
                  }
              }
          });
      });

      $('#btnPopupCancel').click(function () {
          ClearPopupFormValues();

          $('#popupEventForm').hide();
      });

      $('#btnPopupSave').click(function () {

          $('#popupEventForm').hide();

          var dataRow = {
              'Title': $('#eventTitle').val(),
              'NewEventDate': $('#eventDate').val(),
              'NewEventTime': $('#eventTime').val(),
              'NewEventDuration': $('#eventDuration').val()
          }

          ClearPopupFormValues();

          //$.ajax({
          //    type: 'POST',
          //    url: "/Home/SaveEvent",
          //    data: dataRow,
          //    success: function (response) {
          //        if (response == 'True') {
          //            $('#calendar').fullCalendar('refetchEvents');
          //            alert('New event saved!');
          //        }
          //        else {
          //            alert('Error, could not save event!');
          //        }
          //    }
          //});
      });

      function ShowEventPopup(date) {
          ClearPopupFormValues();
          alert('ttt'+date);
          $('#popupEventForm').show();
          $('#eventTitle').focus();
      }

      function ClearPopupFormValues() {
          $('#eventID').val("");
          $('#eventTitle').val("");
          $('#eventDateTime').val("");
          $('#eventDuration').val("");
      }

      function UpdateEvent(EventID, EventStart, EventEnd) {

          var dataRow = {
              'ID': EventID,
              'NewEventStart': EventStart,
              'NewEventEnd': EventEnd
          }

          $.ajax({
              type: 'POST',
              url: "/Home/UpdateEvent",
              dataType: "json",
              contentType: "application/json",
              data: JSON.stringify(dataRow)
          });
      }

      function CreateCalendar(events) {
          $('#calendar').fullCalendar({
              header: {
                  left: 'prev,next today',
                  center: 'title',
                  right: 'month,basicWeek,basicDay'
              },
              defaultView: 'month',
              editable: true,
              allDaySlot: false,
              selectable: true,
              slotMinutes: 15,
              events: events,
              eventClick: function (calEvent, jsEvent, view) {
                  alert('You clicked on event id: ' + calEvent.id
                      + "\nSpecial ID: " + calEvent.someKey
                      + "\nAnd the title is: " + calEvent.title);

              },

              eventDrop: function (event, dayDelta, minuteDelta, allDay, revertFunc) {
                  if (confirm("Confirm move?")) {
                      UpdateEvent(event.id, event.start);
                  }
                  else {
                      revertFunc();
                  }
              },

              eventResize: function (event, dayDelta, minuteDelta, revertFunc) {

                  if (confirm("Confirm change appointment length?")) {
                      UpdateEvent(event.id, event.start, event.end);
                  }
                  else {
                      revertFunc();
                  }
              },



              dayClick: function (date, allDay, jsEvent, view) {

                  $('#eventTitle').val("");
                  $('#eventDate').val($.fullCalendar.formatDate(date, 'dd/MM/yyyy'));
                  $('#eventTime').val($.fullCalendar.formatDate(date, 'HH:mm'));
                  ShowEventPopup(date);
              },

              viewRender: function (view, element) {

                  if (!CalLoading) {
                      if (view.name == 'month') {
                          $('#calendar').fullCalendar('removeEventSource', sourceFullView);
                          $('#calendar').fullCalendar('removeEvents');
                          $('#calendar').fullCalendar('addEventSource', sourceSummaryView);
                      }
                      else {
                          $('#calendar').fullCalendar('removeEventSource', sourceSummaryView);
                          $('#calendar').fullCalendar('removeEvents');
                          $('#calendar').fullCalendar('addEventSource', sourceFullView);
                      }
                  }
              }

          });
      }

</script>

    <style>

	body {
		margin: 40px 10px;
		padding: 0;
		font-family: "Lucida Grande",Helvetica,Arial,Verdana,sans-serif;
		font-size: 18px;
	}

	#calendar {
		max-width: 900px;
		margin: 0 auto;
	}

        .fc-event {
            font-size: 1.2em !important; 
        }

        .fc button {
            font-size: 1.2em !important; 
        }

</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
   <%-- <div id='myCalendar'></div>

 <div id="eventContent" title="Event Details">
    <div id="eventInfo"></div>
   
</div>--%>



    <div class="container">

    <div>
    <a href="#" id="btnInit" class="btn btn-secondary">Initialise database!</a> 
    </div>

    <div id='calendar' style="width:85%"></div>

</div>


<div id="popupEventForm" class="modal hide" style="display: none;">
   <div class="modal-header"><h3>Add new event</h3></div>
  <div class="modal-body">
    <form id="EventForm" class="well">
        <input type="hidden" id="eventID"/>
        <label>Event title</label>
        <input type="text" id="eventTitle" placeholder="Title here"/><br />
        <label>Scheduled date</label>
        <input type="text" id="eventDate"/><br />
        <label>Scheduled time</label>
        <input type="text" id="eventTime"/><br />
        <label>Appointment length (minutes)</label>
        <input type="text" id="eventDuration" placeholder="15"/><br />
    </form>
</div>
  <div class="modal-footer">
    <button type="button" id="btnPopupCancel" data-dismiss="modal" class="btn">Cancel</button>
    <button type="button" id="btnPopupSave" data-dismiss="modal" class="btn btn-primary">Save event</button>
  </div>
</div>


<%--    <div id="calEventDialog">
    <form>
        <fieldset>
        <label for="eventTitle">Title</label>
        <input type="text" name="eventTitle" id="eventTitle" /><br>
        <label for="eventStart">Start Date</label>
        <input type="text" name="eventStart" id="eventStart" /><br>
        <label for="eventEnd">End Date</label>
        <input type="text" name="eventEnd" id="eventEnd" /><br>
        <input type="radio" id="allday" name="allday" value="1"/>
        Half Day
        <input type="radio" id="allday" name="allday" value="2"/>
        All Day
        </fieldset>
    </form>
</div>--%>
</asp:Content>
