﻿function submitMessage() {
    $(function () {
        var text = document.getElementById("messageText").value;
        if (text == "") {
            document.getElementById("resultMessage").innerHTML = "<p>Please enter a message!</p>";
        }
        else {
            var message = { "MessageContent": text };
            $.ajax({
                type: 'POST',
                data: message,
                url: "http://auditionproj-dev.us-east-1.elasticbeanstalk.com/api/Message",
                dataType: 'json',
                success: function () {
                    output = "\"" + message.MessageContent + "\" was posted successfully!";
                    document.getElementById("resultMessage").innerHTML = output;
                    document.getElementById("messageText").value = "";
                },
                error: function (xhr, status, error) {
                    document.getElementById("resultMessage").innerHTML = error;
                }
            });
        }
    });
}

function listMessages() {
    var output = "";
    $(function () {
    $.ajax({
        type: 'GET',
        url: "http://auditionproj-dev.us-east-1.elasticbeanstalk.com/api/Message",
        success: function (result) {
            output += "<table>";
            for (i in result) {
                output += "<tr><td><a href=\"#\" onclick=\"getMessageDetail(" + result[i].ID + ");\">" + result[i].MessageContent + "</a></td></tr>";
            }
            output += "</table>";
            document.getElementById("messageList").innerHTML = output;
            document.getElementById("resultMessage").innerHTML = "";
        }
    })
})
}

function getMessageDetail(ID) {
    var output = "";
    $(function () {
    $.ajax({
        type: 'GET',
        url: "http://auditionproj-dev.us-east-1.elasticbeanstalk.com/api/Message/" + ID,
        dataType: 'json',
        success: function (result) {
            if (result.IsPalindrome == false) {
                output = " not";
            }
            alert("\"" + result.MessageContent + "\" is" + output + " a palindrome.");
        }
    })
})
}

function clearMessages() {
    document.getElementById("messageList").innerHTML = "";
}