export class User {
    nombre!: string;
    apellido!: string;
    email!: string;
    fechaNacimiento!: Date;
    telefono!: string;
    country!: string;
    info!: boolean;

    constructor(nombre: any, apellido: any, email: any, fechaNacimiento: any, telefono: any, country: any, info: any) {
        this.nombre = nombre;
        this.apellido = apellido
        this.email = email
        this.fechaNacimiento = fechaNacimiento
        this.telefono = telefono
        this.country = country
        this.info = info
    }
}