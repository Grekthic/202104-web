// Mensajes de consola
// console.log("La suma de 2 + 2 es", 2 + 2)
// console.info("La suma de 2 + 2 es", 2 + 2)
// console.error("La suma de 2 + 2 es", 2 + 2)
// console.warn("La suma de 2 + 2 es", 2 + 2)

// Variables
var a = 1
const b = 1
let c = 1
// b = 2
c = 3

// Funciones
function funcA(param1) {
    console.log("Ejecucion de la funcA con el parametro 1", param1)
}

funcA("Hola funcion!")

const funcB = function(param2) {
    console.log("Ejecucion de la funcB con el parametro 2", param2)
}

funcB("Hola funcion B!")

const funcC = (param3) => {
    console.log("Ejecucion de la funcC con el parametro 3", param3)
}

funcC("Hola funcion C!")

// Tipo de variable number
let numberA = 100
console.log("Valor de numberA inicial", numberA)

// Tipo de variable string
const stringA = "100"

console.log("Number A es igual a string A?", numberA === stringA)

// Tipo de variable bool
const boolB = false

// Tipo de variable objeto
const objA = { atributoA: 100 }

numberA = "hola"
console.log("Valor de numberA", numberA)

// Operadores
console.log("Number A es igual a string A?", numberA === stringA)

// Condicionales
if(1 === "1") {
    console.log("1 = 1")
} else {
    console.log("1 diferente 1")
}

const unoEsIgualAUno = 1 === "1" ? "si" : "no"
console.log("unoEsIgualAUno", unoEsIgualAUno)

// Arreglos o listas
const arregloNumeros = [1,2,3,4,5,"seis",false]

console.log("El elemento en el indice 3 es ", arregloNumeros[3])

arregloNumeros.push("nuevo elemento")

// El pop elimina y obtiene el ultimo valor del arreglo
console.log("El nuevo elemento es", arregloNumeros.pop())

// Recorrer los arreglos
arregloNumeros.forEach((elemento, index)=>{
    console.log(`El elemento en el indice ${index} es ${elemento}`)
    // console.log("El elemento en el indice" + index + " es " + elemento)
})

// Objetos
const auto = {
    marca: "BMW",
    modelo: "modelo 1",
    anio: 1990,
    color: "rojo"
}

console.log("El atributo color del auto es", auto.color)

auto.puertas = 2 // auto["puertas"] = 2

console.log("El atributo puertas del auto es", auto.puertas)