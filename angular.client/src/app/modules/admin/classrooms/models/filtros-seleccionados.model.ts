import { DimensionPsicologica } from "../enums/dimensionPsicologica.enum";

export class FiltrosSeleccionados {
    private unidad: number | null = null;
    private grado: number | null = null;
    private seccion: number | null = null;
    private dimension: DimensionPsicologica = DimensionPsicologica.habilidadesSocioemocionales;
    private estadoUnidad: boolean = false;


    constructor(
        unidad?: number,
    ) {
        this.unidad = unidad || null;
    }


    set Unidad(unidad: number) {
        this.unidad = unidad;
        this.estadoUnidad = false;
    }
    set Grado(grado: number) {
        this.grado = grado;
        this.seccion = null;
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

