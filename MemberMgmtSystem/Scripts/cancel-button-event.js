function setCancelButtonClickEventGoToIndexPage() {
    $("input[value=Cancel]").click(function () {
        window.location.href = "/Customers/Index";
    });
}

setCancelButtonClickEventGoToIndexPage();