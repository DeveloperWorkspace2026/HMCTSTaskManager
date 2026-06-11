const apiUrl = "/api/tasks";

$(document).ready(function () {
    loadTasks();

    $("#taskForm").submit(function (e) {
        e.preventDefault();

        const task = {
            title: $("#title").val(),
            description: $("#description").val(),
            dueDate: $("#dueDate").val()
        };
       
        $.ajax({
            url: apiUrl,
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify(task),
            success: function () { 
                console.log("Success called");
                $("#taskForm")[0].reset();               
                loadTasks();
                alert("Task created successfully.");
            },
            error: function () {
                alert("Failed to create task.");
            }
        });
    });
});

function loadTasks() {
    //debugger;
    $.get(apiUrl, function (tasks) {

        $("#taskList").empty();

        tasks.forEach(function (task) {  
            
            $("#taskList").append(`
                    <div class= "task-card">
                    <h3>${task.title}</h3>
                    <p>${task.description || ""}</p>
                    <p>Status: ${task.status}</p>
                    <p>Due:${new Date(task.dueDate).toLocaleString()}</p>

                    <select id="status-${task.id}" name="status-${task.id}" onchange="updateStatus(${task.id}, this.value)">
                        <Option value="Pending" ${task.status === "Pending" ? "selected" : ""}> Pending</Option>
                        <Option value="In Progress" ${task.status === "In Progress" ? "selected" : ""}> In Progress</Option>
                        <Option value="Completed" ${task.status === "Completed" ? "selected" : ""}> Completed</Option>
                        <Option value="Cancelled" ${task.status === "Cancelled" ? "selected" : ""} > Cancelled</Option>
                    </select>

                    <button onclick="deleteTask(${task.id},'${task.title}')">Delete</button>
                </div>
            `);
        });

    });
}


function updateStatus(id, status) {
   // debugger;
    $.ajax({
        url: `${apiUrl}/${id}`,
        type: "PUT",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ status: status }),       
        success: function () {           
            loadTasks();
            console.log("Success called");
            alert("Task status updated successfully.");
        },
        error: function (xhr) {
            console.log(xhr.responseText);
            alert("Failed to update task.");
        }
    });
}

function deleteTask(id, title) {

    if (!confirm(`Are sure you want to delete " ${title} " task?`)) {
        return;
    }
  
    $.ajax({
        url: `${apiUrl}/${id}`,
        type: "DELETE",
        success: loadTasks
    });
}
