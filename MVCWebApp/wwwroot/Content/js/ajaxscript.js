function getPeople() {
    $.get("/Ajax/GetPeople", function (data, status)
    {
        document.getElementById("personContainer").innerHTML = data;
    });
}

function getPerson() {
    $.post("/Ajax/GetPerson",
        {
            id: $("#txtId").val()
        },
        function (data, status)
        {
            document.getElementById("personContainer").innerHTML = data;
        });
};

function deletePerson() {
    $.post("/Ajax/DeletePerson",
        {
            id: $("#txtId").val()
        },
        function (data, status) {
            document.getElementById("personContainer").innerHTML = data;
        });
};