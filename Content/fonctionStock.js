$(document).ready(function () {
    $.ajax({
        url: '/Utilisateur/TestEtatMdp/',
        data: "{}",
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8"
    })
        .done(function (response) {
            console.log(response);
            if (response == "ko") {
                $("#modal_mdp").modal("show");
            }
        })
        .fail(function (error) {
            alert(error);
        })
});

function autocomplete2(idbinome) {
    var id = "#binome" + idbinome;
    $(id).autocomplete({
        maxShowItems: 5,
        source: function (request, response) {
            $.ajax({
                url: '/Stock/AutoCompleteBinome/',
                data: "{ 'nom': '" + request.term + "'}",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    response($.map(data, function (item) {
                        return item;
                    }))
                },
                error: function (response) {
                    alert(response.responseText);
                },
                failure: function (response) {
                    alert(response.responseText);
                }
            });
        },
        minLength: 1
    });
}

function playSound(mysound) {
    $("#chatAudio")[0].play();
    $("#chatAudio").prop('muted', false);
}

function validerSortie(binome, magasinier, numero_ticket) {
    $(document).ready(function () {
        $.ajax({
            url: '/Stock/Sortie/',
            data: "{ 'numero_ticket': '" + numero_ticket + "','id_magasinier':'" + magasinier.IdUtilisateur + "','id_binome':'" + binome.IdUtilisateur + "'}",
            dataType: "json",
            type: "POST",
            contentType: "application/json; charset=utf-8"
        })
            .done(function (response) {

                //Changer le nombre de notification en notification -1
                var indexPrecedent = parseInt($('#notif').text());
                var indexActuel = indexPrecedent - 1;
                if (indexActuel < 0) {
                    indexActuel = 0;
                }
                $('#notif').text(indexActuel.toString());
                $('#notif2').text(indexActuel.toString() + " notification(s)");

                //Faire disparaître la ligne
                $("#corps" + numero_ticket + "").remove();
                $("#notif" + numero_ticket + "").remove();

                //Message succès
                $("#message").text("Sortie avec succes");
                $("#message").css("color", "green");
                $("#modal_message").modal("show");
            })
            .fail(function (error) {
                alert(alert(error));
            })
    });
}

function searchMagasinier(mot_de_passe, binome,numero_ticket) {
    $(document).ready(function () {
        $.ajax({
            url: '/Utilisateur/GetMagasinierByMdp/',
            data: "{ 'mdp': '" + mot_de_passe + "'}",
            dataType: "json",
            type: "POST",
            contentType: "application/json; charset=utf-8"
        })
            .done(function (response) {
                if (response.IdUtilisateur == null) {
                    //Message binôme introuvable
                    $("#message").text("Magasinier introuvable");
                    $("#message").css("color", "red");
                    $("#modal_message").modal("show");
                }
                else {
                    let magasinier = response;
                    validerSortie(binome, magasinier, numero_ticket);
                }
            })
            .fail(function (error) {
                alert(alert(error));
            })
    });
}

function searchBinome(nom_binome, numero_ticket,mot_de_passe) {
    $(document).ready(function () {
        $.ajax({
            url: '/Utilisateur/GetBinome/',
            data: "{ 'nomBinome': '" + nom_binome + "'}",
            dataType: "json",
            type: "POST",
            contentType: "application/json; charset=utf-8"
        })
            .done(function (response) {
                if (response.IdUtilisateur == null) {
                    //Message binôme introuvable
                    $("#message").text("Binôme introuvable");
                    $("#message").css("color", "red");
                    $("#modal_message").modal("show");
                }
                else {
                    searchMagasinier(mot_de_passe, response, numero_ticket);
                }
            })
            .fail(function (error) {
                alert(alert(error));
            })
    });
}

function sortie(numero_ticket) {
    var nom_binome = $("#binome" + numero_ticket).val();
    var mot_de_passe = $("#magasinier" + numero_ticket).val();
    searchBinome(nom_binome, numero_ticket, mot_de_passe);
}

function getListeArticle(numero_ticket) {
    $(document).ready(function () {
        $.ajax({
            url: '/Vente/GetListeCommandeStock/',
            data: "{ 'numerocomplete': '" + numero_ticket + "'}",
            dataType: "json",
            type: "POST",
            contentType: "application/json; charset=utf-8"
        })
            .done(function (response) {
                console.log(response);
                let data = response;
                $("#carte").append("<div id=\"corps" + numero_ticket + "\" class=\"row\"><div class=\"col-sm-12\"><div class=\"card card-primary\"><div class=\"card-header\"><h3 class=\"card-title\">Ticket n* " + numero_ticket+"</h3></div><div class=\"card-body\">" +
                    "<div id=\"" + numero_ticket + "\" class=\"row\"><div class= \"col-sm-8\"><div class=\"form-group\"><label for=\"designation\">Désignation</label></div></div><div class=\"col-sm-2\"><div class=\"form-group\"><label for=\"quantite\">Quantités</label></div></div><div class=\"col-sm-2\"><div class=\"form-group\"><label for=\"comptoir\">Comptoir</label></div></div></div>" +
                    "</div><div class=\"card-footer\"><button type=\"submit\" onclick=\"sortie(" + numero_ticket + ")\" class=\"btn btn-primary\">Sortie</button></div></div></div></div>");
                for (var i = 0; i < data.length; i++) {
                    $("#" + numero_ticket+ "").append("<div class=\"col-sm-8\"><div class=\"form-group\"><input type=\"text\" class=\"form-control\" value=\"" + data[i].Article.Designation + "\" disabled></div></div>");
                    $("#" + numero_ticket + "").append("<div class=\"col-sm-2\"><div class=\"form-group\"><input type=\"text\" class=\"form-control\" value=\"" + data[i].Quantite + "\" disabled></div></div>");
                    $("#" + numero_ticket + "").append("<div class=\"col-sm-2\"><div class=\"form-group\"><input type=\"text\" class=\"form-control\" value=\"" + data[i].Comptoir.NomComptoir + "\" disabled></div></div>");
                }
                $("#" + numero_ticket + "").append(
                    "<div class=\"col-sm-2\">" +
                    "<div class=\"form-group\">"+
                    "<label for=\"binome\">Binôme</label>" +
                    "<input id=\"binome" + numero_ticket + "\" type=\"text\" class=\"form-control\" onkeypress=\"autocomplete2(" + numero_ticket + ")\" placeholder=\"Binôme\" name=\"binome\">" +
                    "</div></div >");
                $("#" + numero_ticket + "").append("<div class=\"col-sm-2\">"+
                    "<div class=\"form-group\">"+
                    "<label for=\"magasinier\">Magasinier</label>"+
                    "<input id=\"magasinier" + numero_ticket + "\" type=\"password\" class=\"form-control\" placeholder=\"Mot de passe\" name=\"magasinier\">"+
                    "</div></div >");
            })
    }
    );
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
        listeNotif.append("<div id=\"notif" + numero_ticket + "\"><a href=\"javascript:getListeArticle(" + numero_ticket + ")\" class=\"dropdown-item\"><i class=\"fas fa-envelope mr-2\"></i>" + numero_ticket + "</a><div class=\"dropdown-divider\"></div></div>");
        
    }
    $.connection.hub.start();
});
