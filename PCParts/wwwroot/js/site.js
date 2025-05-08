// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function filtrarLocalizacoes(){
    let elementoFiltro = document.getElementById('filtro');
    let filtro = elementoFiltro.value.toUpperCase();
    let filtroDropdown = document.getElementById('filtroDropdown');
    let elementos = filtroDropdown.getElementsByTagName('a');
    for(let i = 0; i < elementos.length; i++){
        let value = elementos[i].textContent.toUpperCase();
        if (filtro === "" || value.indexOf(filtro) > -1) {
            elementos[i].style.display = "";
        } else {
            elementos[i].style.display = "none";
        }
    }
}