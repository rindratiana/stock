$('#mdp2').keyup(function () {
    if ($('#mdp2').val() != $('#mdp1').val()) {
        $("#message_mdp").show();
        $("#message_mdp").text("Vérifiez bien votre nouveau mot de passe");
        $("#mdp2").addClass("form-control is-invalid");
    }
    else {
        $("#mdp2").removeClass("form-control is-invalid");
        $("#mdp2").addClass("form-control");
        $("#message_mdp").hide();
    }
});