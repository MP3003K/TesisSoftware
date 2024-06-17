export interface ButtonToogle {
    id: string,
    texto: string,
    accion: () => void,
    claseActiva: string;
    claseInactiva: string;
    esVisible: () => boolean;
}
