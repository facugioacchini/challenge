export class Activities {
    fechaActividad!: Date;
    nombreCompleto!: string;
    detalle!: string;

    constructor(fechaActividad: any, nombreCompleto: any, detalle: any) {
        this.fechaActividad = fechaActividad;
        this.nombreCompleto = nombreCompleto
        this.detalle = detalle
    }
}