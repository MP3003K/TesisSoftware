import { DimensionPsicologica } from "../enums/dimensionPsicologica.enum";

export class FiltrosSeleccionados {
    public unidad: number | null = null;
    public grado: number | null = null;
    public seccion: number | null = null;
    public dimension: DimensionPsicologica = DimensionPsicologica.habilidadesSocioemocionales;
    public estadoUnidad: boolean = false;


    constructor(
        unidad?: number,
    ) {
        this.unidad = unidad || null;
    }


    set Unidad(unidad: number) {
        this.unidad = unidad;
    }
    set Grado(grado: number) {
        this.grado = grado;
    }
    set Seccion(seccion: number) {
        this.seccion = seccion;
    }
    set Dimension(dimension: DimensionPsicologica) {
        this.dimension = dimension;
    }
    set EstadoUnidad(estadoUnidad: boolean) {
        this.estadoUnidad = estadoUnidad;
    }
    get Unidad() {
        return this.unidad;
    }
    get Grado() {
        return this.grado;
    }
    get Seccion() {
        return this.seccion;
    }
    get Dimension() {
        return this.dimension;
    }
    get EstadoUnidad() {
        return this.estadoUnidad;
    }

    /**
     * Valida que todos los filtros hayan sido registrados.
     * @returns boolean
     */
    succeeded() {
        if (this.unidad && this.grado && this.seccion) {
            return true;
        }
        return false;
    }

}

