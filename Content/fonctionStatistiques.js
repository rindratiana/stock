function getStatistique() {
    var dateDebut = $("#date_debut").val();
    var dateFin = $("#date_fin").val();
    if (dateDebut == "" || dateFin == "" || dateFin < dateDebut) {
        alert("Vérifiez bien les deux dates");
    }
    else {
        var options = {
            title: 'Emplacement',
            width: '100%',
            height: '100%',
            is3D: true,
            pieHole: 0.3,
        };
        $(document).ready(function () {
            $.ajax({
                url: '/Stock/GetStatistiquesEntreDeuxDates/',
                data: "{ 'dateDebut': '" + dateDebut + "','dateFin':'" + dateFin + "'}",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8"
            })
                .done(function (response) {
                    var arrValues = [['Emplacement', 'Pourcentage','numero']];
                    var iCnt = 0;

                    $.map(response, function () {
                        arrValues.push([response[iCnt].Emplacement.Intitule, response[iCnt].Quantite, response[iCnt].Emplacement.NumeroEmplacement]);
                        iCnt += 1;
                    });
                    var figures = google.visualization.arrayToDataTable(arrValues);
                    var chart = new google.visualization.PieChart(document.getElementById('chartContainer'));
                    function selectHandler() {
                        var selectedItem = chart.getSelection()[0];
                        if (selectedItem) {
                            var select = arrValues[selectedItem.row + 1];
                            getDescription(select[2], dateDebut, dateFin, select[0]);
                        }
                    }
                    google.visualization.events.addListener(chart, 'select', selectHandler);
                    chart.draw(figures, options);
                })
                .fail(function (error) {
                    alert(alert(error));
                })
        });
    }
}

window.onload = function () {

        var options = {
            title: 'Emplacement',
            width: '100%',
            height: '100%',
            is3D: true,
            pieHole: 0.3,
        };

        $(document).ready(function () {
        $.ajax({
            url: '/Stock/GetStatistiques/',
            data: "{}",
            dataType: "json",
            type: "POST",
            contentType: "application/json; charset=utf-8"
        })
            .done(function (response) {
                var arrValues = [['Emplacement', 'Pourcentage','numero']];
                var iCnt = 0;

                $.map(response, function () {
                    arrValues.push([response[iCnt].Emplacement.Intitule, response[iCnt].Quantite, response[iCnt].Emplacement.NumeroEmplacement]);
                    iCnt += 1;
                });
                var figures = google.visualization.arrayToDataTable(arrValues);
                var chart = new google.visualization.PieChart(document.getElementById('chartContainer'));
                function selectHandler() {
                    var selectedItem = chart.getSelection()[0];
                    if (selectedItem) {
                        var select = arrValues[selectedItem.row + 1];
                        getDescription(select[2], "", "", select[0]);
                    }
                }
                google.visualization.events.addListener(chart, 'select', selectHandler);
                chart.draw(figures, options);
            })
            .fail(function (error) {
                alert(alert(error));
            })
        });
}

function getDescription(numeroEmplacements,dateDebut,dateFin,nomEmplacement) {
    var options = {
        title: 'Articles',
        is3D: true,
        pieHole: 0.3,
    };
    $(document).ready(function () {
        $.ajax({
            url: '/Stock/GetStatistiquesByEmplacement/',
            data: "{ 'dateDebut': '" + dateDebut + "','dateFin':'" + dateFin + "','numeroEmplacements':'" + numeroEmplacements + "'}",
            dataType: "json",
            type: "POST",
            contentType: "application/json; charset=utf-8"
        })
            .done(function (response) {
                var arrValues = [['Articles', 'Pourcentage']];
                var iCnt = 0;

                $.map(response, function () {
                    arrValues.push([response[iCnt].Article.Designation, response[iCnt].Quantite]);
                    iCnt += 1;
                });
                var figures = google.visualization.arrayToDataTable(arrValues);
                var chart = new google.visualization.PieChart(document.getElementById('chartModal'));

                $("#titre").append("<i class=\"fas fa-chart-line\"></i> Stat des articles pour " + nomEmplacement);
                $("#modal_chart").modal("show");
                //nettoie le modal à chaque close
                $('#modal_chart').on('hidden.bs.modal', function () {
                    $('#titre').text("");
                });
                chart.draw(figures, options);
            })
            .fail(function (error) {
                alert(alert(error));
            })
    });
}
 