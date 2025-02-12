import { Injectable } from '@angular/core';
import { neriModel } from '../models/neriModel';
import { ProductoComponent } from '../pages/producto/producto.component';
import { pujaModel } from '../models/pujaModel';

@Injectable({
  providedIn: 'root' 
})
export class NeriService {
  neriList: ProductoComponent[];
  readonly baseUrl = 'http://localhost:7000/api/Subastas';
  readonly baseUrlPujas = 'http://localhost:7000/api/Puja';
  constructor() {
    this.neriList= [];
   }

   async getAllProduct(): Promise<neriModel[]> {
    const data = await fetch(this.baseUrl);
    return (await data.json()) ?? [];
  }

  async getProductById(id: number): Promise<neriModel | undefined> {
    const data = await fetch(`${this.baseUrl}/${id}`);
    return (await data.json()) as neriModel | undefined;
  }

  async getPrecios(): Promise<pujaModel[]> {
    const data = await fetch(this.baseUrlPujas);
    return (await data.json()) ?? []; 
}

  async updateProduct(id: number, partialProduct: Partial<neriModel>): Promise<neriModel> {
    const response = await fetch(`${this.baseUrl}/${id}`, {
        method: "PATCH",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(partialProduct)
    });

    return await response.json();
}

async createProduct(product: neriModel): Promise<neriModel> {
  const response = await fetch(this.baseUrl, {
      method: "POST",
      headers: {
          "Content-Type": "application/json"
      },
      body: JSON.stringify(product)
  });

  return await response.json();
}

}
