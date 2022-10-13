function TornaBotaoAtualizarQuantidadeVisivel(id, visible) {

    const atualizaQuantidadeButton = document.querySelector("button[data-itemId='" + id + "']");

    if (visible == true) {
        atualizaQuantidadeButton.style.display = "inline-block";
    }
    else {
        atualizaQuantidadeButton.style.display = "none";
    }
}