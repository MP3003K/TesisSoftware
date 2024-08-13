import { DimensionPsicologica } from "../enums/dimensionPsicologica.enum";

export class FiltrosSeleccionados {
    private unidad: number | null = null;
    private unidadNumero: number | null = null;
    private unidadNombre: string;
    private estadoUnidad: boolean = false;
    private grado: number | null = null;
    private seccion: number | null = null;
    private seccionN: string;
    private dimension: DimensionPsicologica = DimensionPsicologica.habilidadesSocioemocionales;

    constructor(
        unidad?: number,
    ) {
        this.unidad = unidad || null;
    }


    set Unidad(unidad: number) {
        this.unidad = unidad;
        this.estadoUnidad = false;
    }

    set UnidadNumero(unidadNumero: number) {
        this.unidadNumero = unidadNumero;
    }

    set UnidadNombre(unidadNombre: string) {
        this.unidadNombre = unidadNombre;
    }
    set Grado(grado: number) {
        this.grado = grado;
        this.seccion = null;
    }
    set Seccion(seccion: number) {
        this.seccion = seccion;
    }
    set SeccionN(seccionN: string) {
        this.seccionN = seccionN;
    }
    set Dimension(dimension: DimensionPsicologica) {
        this.dimension = dimension;
    }
    get Unidad() {
        return this.unidad;
    }
    get UnidadNumero() {
        return this.unidadNumero;
    }
    get UnidadNombre() {
        return this.unidadNombre;
    }
    set EstadoUnidad(estadoUnidad: boolean) {
        this.estadoUnidad = estadoUnidad;
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
    get SeccionN() {
        return this.seccionN;
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

