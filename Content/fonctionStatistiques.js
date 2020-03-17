function playSound(mysound) {
    $("#chatAudio")[0].play();
    $("#chatAudio").prop('muted', false);
}
$(function () {
    var chat = $.connection.hubClient;
    var audio;
    chat.client.setNotifications = function (numero, numero_ticket) {
        $.notify("Ticket n* " + numero_ticket);
        var indexPrecedent = parseInt($('#notif').text());
        var indexActuel = indexPrecedent + 1;
        $('#notif').text(indexActuel.toString());
        $('#notif2').text(indexActuel.toString() + " notification(s)");
        playSound("/Content/Uploads/notif.mp3");
        var listeNotif = $("#notification");
        listeNotif.append("<div id=\"notif" + numero_ticket + "\"><a href=\"/Stock/Index\" class=\"dropdown-item\"><i class=\"fas fa-envelope mr-2\"></i>" + numero_ticket + "</a><div class=\"dropdown-divider\"></div></div>");

    }
    $.connection.hub.start();
});

function msToTime(duration) {
    var milliseconds = parseInt((duration % 1000) / 100),
        seconds = Math.floor((duration / 1000) % 60),
        minutes = Math.floor((duration / (1000 * 60)) % 60),
        hours = Math.floor((duration / (1000 * 60 * 60)) % 24);

    hours = (hours < 10) ? "0" + hours : hours;
    minutes = (minutes < 10) ? "0" + minutes : minutes;
    seconds = (seconds < 10) ? "0" + seconds : seconds;

    return hours + ":" + minutes + ":" + seconds + "." + milliseconds;
}

function getListeCommande() {
    $('#detail2').on('hidden.bs.modal', function () {
        $('#tbody_detail2').text("");
    });
    var date_debut = $("#date_debut").val();
    var date_fin = $("#date_fin").val();
    $(document).ready(function () {
        $.ajax({
            url: '/Stock/GetListeCommande/',
            data: "{ 'dateDebut': '" + date_debut + "','dateFin':'" + date_fin + "'}",
            dataType: "json",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            beforeSend:function() {
                $('body').append("<img id=\"loader\" style='top: 45%; position: absolute; height: 100px; width: 100px;background: black;left: 45%;' src=\"/Content/template/dist/img/load/giphy.GIF\"/>");
            }
        })
            .done(function (response) {
                console.log(date_debut + " / " + date_fin);
                console.log(response);
                let data = response;
                for (var i = 0; i < data.length; i++) {
                    var j = i + 1;
                    var dateParts = data[i].DateCommande.split("/");
                    var annee = dateParts[2].split(" ");
                    var dateParts1 = data[i].Duree.HeureCommandeString.split(" ");
                    var dateParts2 = data[i].Duree.HeureSortieString.split(" ");
                    var timeStart = new Date("01/01/2007 " + dateParts1[1]).getTime();
                    var timeEnd = new Date("01/01/2007 " + dateParts2[1]).getTime();
                    var hourDiff = (timeEnd - timeStart);
                    var difference = msToTime(hourDiff).split('.');
                
                    for (var v = 0; v < data[i].ListeDetailCommande.length; v++) {
                        j = i + 1;
                        if (v >= 1) {
                            $("#tbody_detail2").append("<tr>" +
                                "<td style=\"display:none\"></td>" +
                                "<td style=\"display:none\"></td>" +
                                "<td style=\"display:none\"></td>" +
                                "<td style=\"display:none\"></td>" +
                                "<td style=\"display:none\"></td>" +
                                "<td>" + data[i].ListeDetailCommande[v].Article.Designation + "</td>" +
                                "<td style=\"vertical-align:middle;text-align:right\">" + data[i].ListeDetailCommande[v].Quantite + "</td>" +
                                "<td style=\"display:none\"></td>" +
                                "<td style=\"display:none\"></td>" +
                                "<td style=\"display:none\"></td>" +
                                "</tr>");
                        }
                        else {
                            $("#tbody_detail2").append("<tr>" +
                            "<td style=\"vertical-align:middle; text-align:center\" rowspan=\"" + data[i].ListeDetailCommande.length + "\">" + j + "</td>" +
                            "<td style=\"vertical-align:middle; text-align:center\" rowspan=\"" + data[i].ListeDetailCommande.length+"\">" + data[i].Numero + "</td>" +
                            "<td style=\"vertical-align:middle; text-align:center\" rowspan=\"" + data[i].ListeDetailCommande.length +"\">" + dateParts[0] + "/" + dateParts[1] + "/" + annee[0] + "</td>" +
                            "<td style=\"vertical-align:middle; text-align:center\" rowspan=\"" + data[i].ListeDetailCommande.length +"\">" + data[i].Comptoir.NomComptoir + "</td>" +
                            "<td style=\"vertical-align:middle; text-align:center\" rowspan=\"" + data[i].ListeDetailCommande.length +"\">" + data[i].SortieCommande.Magasinier.Prenoms + "</td>" +
                            "<td>" + data[i].ListeDetailCommande[v].Article.Designation + "</td>" +
                            "<td style=\"vertical-align:middle;text-align:right\">" + data[i].ListeDetailCommande[v].Quantite + "</td>" +
                            "<td style=\"vertical-align:middle; text-align:center\" rowspan=\"" + data[i].ListeDetailCommande.length +"\">" + dateParts1[1] + "</td>" +
                            "<td style=\"vertical-align:middle; text-align:center\" rowspan=\"" + data[i].ListeDetailCommande.length +"\">" + dateParts2[1] + "</td>" +
                            "<td style=\"vertical-align:middle; text-align:center\" rowspan=\"" + data[i].ListeDetailCommande.length +"\">" + difference[0] + "</td>" +
                            "</tr>");
                        }
                    }
                }
                $("#detail2").modal("show");
            }).complete(function(data) {
                // Hide image container
                $("#loader").remove();
            })
            .fail(function (error) {
                console.log(error);
                alert(alert(error));
            })
    });
}

function getListeCommandeAnnule() {
    $('#detail').on('hidden.bs.modal', function () {
        $('#tbody_detail').text("");
    });
    var date_debut = $("#date_debut").val();
    var date_fin = $("#date_fin").val();
    $(document).ready(function () {
        $.ajax({
            url: '/Stock/GetListeCommandeAnnule/',
            data: "{ 'dateDebut': '" + date_debut + "','dateFin':'" + date_fin + "'}",
            dataType: "json",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            beforeSend:function() {
                $('body').append("<img id=\"loader\" style='top: 45%; position: absolute; height: 100px; width: 100px;background: black;left: 45%;' src=\"/Content/template/dist/img/load/giphy.GIF\"/>");
            }
        })
            .done(function (response) {
                console.log(date_debut + " / " + date_fin);
                console.log(response);
                let data = response;
                for (var i = 0; i < data.length; i++) {
                    var j = i + 1;
                    var dateParts = data[i].DateCommande.split("/");
                    var annee = dateParts[2].split(" ");

                    for (var v = 0; v < data[i].ListeDetailCommande.length; v++) {
                        j = i + 1;
                        if (v >= 1) {
                            $("#tbody_detail").append("<tr>" +
                                "<td style=\"display:none\"></td>" +
                                "<td style=\"display:none\"></td>" +
                                "<td style=\"display:none\"></td>" +
                                "<td style=\"display:none\"></td>" +
                                "<td>" + data[i].ListeDetailCommande[v].Article.Designation + "</td>" +
                                "<td style=\"vertical-align:middle;text-align:right\">" + data[i].ListeDetailCommande[v].Quantite + "</td>" +
                                "</tr>");
                        }
                        else {
                            $("#tbody_detail").append("<tr>" +
                                "<td style=\"vertical-align:middle; text-align:center\" rowspan=\"" + data[i].ListeDetailCommande.length + "\">" + j + "</td>" +
                                "<td style=\"vertical-align:middle; text-align:center\" rowspan=\"" + data[i].ListeDetailCommande.length + "\">" + data[i].Numero + "</td>" +
                                "<td style=\"vertical-align:middle; text-align:center\" rowspan=\"" + data[i].ListeDetailCommande.length + "\">" + dateParts[0] + "/" + dateParts[1] + "/" + annee[0] + "</td>" +
                                "<td style=\"vertical-align:middle; text-align:center\" rowspan=\"" + data[i].ListeDetailCommande.length + "\">" + data[i].Comptoir.NomComptoir + "</td>" +
                                "<td>" + data[i].ListeDetailCommande[v].Article.Designation + "</td>" +
                                "<td style=\"vertical-align:middle;text-align:right\">" + data[i].ListeDetailCommande[v].Quantite + "</td>" +
                                "</tr>");
                        }
                    }
                }
                $("#detail").modal("show");
            })
            .complete(function (data) {
                // Hide image container
                $("#loader").remove();
            })
            .fail(function (error) {
                alert(alert(error));
            })
    });
}

function getStatistiqueMouvement(dateDebut, dateFin) {
    $(document).ready(function () {
        $.ajax({
            url: '/Stock/GetStatCommandesMouvement/',
            data: "{ 'dateDebut': '" + dateDebut + "','dateFin':'" + dateFin + "'}",
            dataType: "json",
            type: "POST",
            contentType: "application/json; charset=utf-8"
        })
            .done(function (response) {
                $("#mouvement").text(response.Nombre);
            })
            .fail(function (error) {
                alert(alert(error));
            })
    });
}

function getStatistiqueCommandeAnnule(dateDebut, dateFin) {
    $(document).ready(function () {
        $.ajax({
            url: '/Stock/GetStatCommandesAnnule/',
            data: "{ 'dateDebut': '" + dateDebut + "','dateFin':'" + dateFin + "'}",
            dataType: "json",
            type: "POST",
            contentType: "application/json; charset=utf-8"
        })
            .done(function (response) {
                $("#nombre_commande_annule").text(response.Nombre);
            })
            .fail(function (error) {
                alert(alert(error));
            })
    });
}

function getStatistiqueDuree(dateDebut, dateFin) {
    $(document).ready(function () {
        $.ajax({
            url: '/Stock/GetStatDuree/',
            data: "{ 'dateDebut': '" + dateDebut + "','dateFin':'" + dateFin + "'}",
            dataType: "json",
            type: "POST",
            contentType: "application/json; charset=utf-8"
        })
            .done(function (response) {
                console.log(msToTime(response));
                var difference = msToTime(response).split('.');
                $("#moyenne_heure").text(difference[0]);
            })
            .fail(function (error) {
                alert(alert(error));
            })
    });
}

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

                    //Statistique durée
                    getStatistiqueDuree(dateDebut, dateFin);
                    //Statistique commande annulé
                    getStatistiqueCommandeAnnule(dateDebut, dateFin);
                    //Statistique mouvements
                    getStatistiqueMouvement(dateDebut, dateFin);
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

                //Statistique durée
                getStatistiqueDuree("", "");

                //Statistique commande annulé
                getStatistiqueCommandeAnnule("", "");

                //Statistique nombre de mouvements
                getStatistiqueMouvement("", "");
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
 