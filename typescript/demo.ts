import { Artist } from "./dto/Artist"
// Declaracion de variables
const varString: string = "hola mundo";
let varStringYNumber: string | number = "hola mundo";
varStringYNumber = 321;

// Interfaces y tipos
interface IUser {
    name: string;
    userId: number;
    coloresFavoritos: string[];
    // Propiedad opcional
    lastName?: string;
}

type User = IUser

type UserAdmin = IUser & {
    bankAccount: number;
    artistas: Artist[]
}

const user: User = {
    userId: 123,
    name: "arturo",
    coloresFavoritos: ["rojo"]
};

const adminUser: UserAdmin = {
    bankAccount: 1234,
    userId: 321,
    name: "arturo admin",
    coloresFavoritos: ["amarillo"],
    artistas: [{ artistId: 123, name: "artista" }]
}

// Spread Operator
const arturoAdmin: UserAdmin = {
    ...user,
    bankAccount: 54231,
    lastName: "balbin",
    artistas: [{ artistId: 123, name: "artista" }]
}

function crearUsuario(name: string): User {
    return {
        coloresFavoritos: [],
        name,
        userId: 123
    }
}

// Programacion asincrona

// Esta es una funcion que no devuelve nada
async function funcion1(): Promise<void> {
    const resultado = await funcion2("arturo")
    console.log(resultado);
}

async function funcion2(name: string): Promise<string> {
    return name;
}

funcion1()

// Destructuracion de objetos
function funcion3({ name, artistas, userId }: UserAdmin): void {
    // Imprimir el nombre
    // const nombre = user.name;
    // Obtener el primer elemento de un arreglo
    const primerElemento = artistas[0];
    const [primerElementoAlt, segundoElemento] = artistas;
    console.log(name)
}
