import { pujaModel } from "./pujaModel";

export interface neriModel {
    id: number;
    name: string;
    photo: string;
    price: number;
    pujas: pujaModel[];
}
