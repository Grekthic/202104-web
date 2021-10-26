// Importar la libreri jQuery
import $ from "jquery"

function agregarTexto() {
    // Obtener el div por id usando jQuery
    $("#contenedor").append("<p>Este es cualquier texto</p>")
}

// Funcion document load de jQuery
$(function(){
    // Agregar el codigo al evento click del boton
    $("#btn-agregar").on("click", function() {
        agregarTexto()
    })
})