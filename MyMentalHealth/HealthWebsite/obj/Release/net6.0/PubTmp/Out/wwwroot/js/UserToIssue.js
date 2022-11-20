$(function () {

    $("button[name='SaveUsers']").prop('disabled', true);

    $('select').on('change', function (){
        var url = "/UserToIssue/GetUsersForIssue?mentalHealthIssueId=" + this.value;

        if(this.value != 0){
            $.ajax(
                {
                    type: "GET",
                    url = url,
                    success: function (data) {
                        $("#UsersList").html(data);
                        $("button[name='SaveUsers']").prop('disabled', false);

                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                        PresentClosableBootstrapAlert("#alert", "danger", "An error occured", "An error occurred!" )
                        console.error("An error has occurred: " + thrownError + "Status: " + xhr.status + "\r\n" + xhr.responseText);

                    }
                }
            );
        }
        else {
            $("button[name='SaveUsers']").prop('disabled', true);
            $("input[type=checkbox]").prop("checked", false);
            $("input[type=checkbox]").prop("disabled", true);
        }


    });

});

$('#SaveUsers').click(function () {

    var url = "/UserToIssue/SaveUsers";

    var mentalHealthIssueId = $("#MentalHealthIssueId").val();

    var antiForgeryToken = $("input[name='__RequestVerificationToken']").val();

    var selectedUsers = [];

    DisableControls(true);

    $(".progress").show("fade");


    $('input[type=checkbox]:checked').each(function () {
        var userModel = {
            Id: $(this).attr("value")
        };
        selectedUsers.push(userModel);
    });

    var usersSelectedForCategory = {
        __RequestVerificationToken: antiForgeryToken,
        MentalHealthIssueId: mentalHealthIssueId,
        SelectedUsers: selectedUsers
    };
    $.ajax(
        {
            type: "POST",
            url: url,
            data: usersSelectedForCategory,
            success: function (data) {
                $("#UsersList").html(data);

                $(".progress").hide("fade", function () {
                    $(".alert-success").fadeTo(2000, 500).slideUp(500, function () {
                        DisableControls(false);

                    });

                });

            },
            error: function (xhr, ajaxOptions, thrownError) {
                $(".progress").hide("fade", function () {
                    PresentClosableBootstrapAlert("#alert", "danger", "An error occurred!", "An error occurred!");
                    console.error("An error has occurred: " + thrownError + "Status: " + xhr.status + "\r\n" + xhr.responseText);

                    DisableControls(false);
                });
            }
        }
    );

    function DisableControls(disable) {
        $('input[type=checkbox]').prop("disabled", disable);
        $("#SaveUsers").prop('disabled', disable);
        $('select').prop('disabled', disable);
    }

});
});
