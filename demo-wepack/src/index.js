// Importar la libreri jQuery
import $ from "jquery"

function agregarTexto() {
    // Obtener el div por id usando jQuery
    $("#contenedor").append("<p>Este es cualquier texto</p>")
}

/**
 * Esta funcion nos sirve para obtener los blogs por AJAX y pintarlos en el
 * control blogs-tbody
 */
function obtenerBlogs() {
    // Realizar la consulta AJAX (XML HTTP REQUESTS -> XHR)
    // Usar un callback para procesar la respuestas del servicio
    $.ajax({
        method: "GET", // POST, GET, PUT, DELETE
        url: "https://localhost:5001/blogs"
    }).done(function(data){
        // Comprobar que data es un arreglo
        if(!Array.isArray(data)){
            console.warn("La respuesta del servicio no es un arreglo")
            return;
        }
        // Asumir que data es un arreglo
        // Recorrer data y agregar un elemento tipo: <tr><td>Id</td><td>Url</td><td>Rating</td></tr>
        const arregloHtmlFilas = data.map(function(elemento){
            return `<tr><td>${elemento.blogId}</td><td>${elemento.url}</td><td>${elemento.rating}</td></tr>`
        }) // ["string1", "string2"......]

        // Obtener el tbody donde queremos inyectar el html (las filas) 
        $("#blogs-tbody").append(arregloHtmlFilas.join()) // -> "string1string2string3"
    })
}

// Funcion document load de jQuery
$(function(){
    // Agregar el codigo al evento click del boton
    $("#btn-agregar").on("click", function() {
        agregarTexto()
    })

    $("#btn-obtener").on("click", function() {
        obtenerBlogs()
    })
})