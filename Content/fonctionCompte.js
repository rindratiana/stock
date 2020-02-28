function validerMiseAjour(nom, prenom, nouveau_mdp1) {
    $(document).ready(function () {
        $.ajax({
            url: '/Utilisateur/UpdateProfil/',
            data: "{ 'nom': '" + nom + "','prenom': '" + prenom + "','mdp': '" + nouveau_mdp1 + "'}",
            dataType: "json",
            type: "POST",
            contentType: "application/json; charset=utf-8"
        })
            .done(function (response) {
                $("#message2").text("Mise à jour profil avec succès, veuillez vous connecter");
                $("#message2").css("color", "green");
                $("#modal_message2").modal("show");
            })
            .fail(function (error) {
                alert(alert(error));
            })
    });
}
function testAncienMdp(nom, prenom, ancien_mdp, nouveau_mdp1, nouveau_mdp2) {
    $(document).ready(function () {
        $.ajax({
            url: '/Utilisateur/TestAncienMdp/',
            data: "{ 'mdp': '" + ancien_mdp + "'}",
            dataType: "json",
            type: "POST",
            contentType: "application/json; charset=utf-8"
        })
            .done(function (response) {
                if (response.IdUtilisateur == null) {
                    $("#message").text("Le mot de passe entré est incorrect");
                    $("#message").css("color", "red");
                    $("#modal_message").modal("show");
                }
                else {
                    if (nouveau_mdp1 != nouveau_mdp2) {
                        $("#message").text("Les mots de passe saisis ne sont pas identiques");
                        $("#message").css("color", "red");
                        $("#modal_message").modal("show");
                    }
                    else {
                        validerMiseAjour(nom, prenom,nouveau_mdp1);
                    }
                }
            })
            .fail(function (error) {
                alert(alert(error));
            })
    });
}
function updateAccount() {
    var nom = $("#nom").val();
    var prenom = $("#prenom").val();
    var ancien_mdp = $("#ancien_mdp").val();
    var nouveau_mdp1 = $("#mdp1").val();
    var nouveau_mdp2 = $("#mdp2").val();
    if (nom == "" || prenom == "" || ancien_mdp == "" || nouveau_mdp1 == "" || nouveau_mdp2 == "") {
        $("#message").text("Tous les champs sont obligatoires");
        $("#message").css("color", "red");
        $("#modal_message").modal("show");
    }
    else {
        testAncienMdp(nom, prenom, ancien_mdp, nouveau_mdp1, nouveau_mdp2);
    }
}

function parametreCompte() {
    $("#div_parametre").show();
    $("#settings").show();
}