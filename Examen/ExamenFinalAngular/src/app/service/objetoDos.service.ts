import { Injectable } from '@angular/core';
import { ObjetoDosDTO } from '../models/ObjetoDosDTO';
import { CreateObjetoModel } from '../models/CreateObjetoModel';

@Injectable({
  providedIn: 'root'
})
export class ObjetoService {
  readonly baseUrl = 'http://localhost:7000/api/ObjetoDos';

  constructor() {}

  private getAuthHeaders(): { [key: string]: string } {
    const token = localStorage.getItem('token');
    return {
      'Authorization': `Bearer ${token}`,
      'Content-Type': 'application/json'
    };
  }

  async getAllProduct(): Promise<ObjetoDosDTO[]> {
    const response = await fetch(this.baseUrl, {
      method: 'GET',
      headers: this.getAuthHeaders()
    });
    return (await response.json()) ?? [];
  }

  async getObjetoDosByIds(ids: number[]): Promise<ObjetoDosDTO[]> {
    const promises = ids.map(id => 
      fetch(`${this.baseUrl}/${id}`, {
        method: 'GET',
        headers: this.getAuthHeaders()
      })
        .then(response => response.json())
    );
  
    return Promise.all(promises);
  }

  async getProductById(id: number): Promise<ObjetoDosDTO | undefined> {
    const response = await fetch(`${this.baseUrl}/${id}`, {
      method: 'GET',
      headers: this.getAuthHeaders()
    });
    return (await response.json()) as ObjetoDosDTO | undefined;
  }

  async updateProduct(id: number, partialProduct: Partial<ObjetoDosDTO>): Promise<ObjetoDosDTO> {
    const response = await fetch(`${this.baseUrl}/${id}`, {
      method: "PATCH",
      headers: this.getAuthHeaders(),
      body: JSON.stringify(partialProduct)
    });

    return await response.json();
  }

// cambiar model
  async createProduct(product: CreateObjetoModel): Promise<CreateObjetoModel> {
    const response = await fetch(this.baseUrl, {
      method: "POST",
      headers: this.getAuthHeaders(),
      body: JSON.stringify(product)
    });

    return await response.json();
  }
  
  async deleteProduct(id: number): Promise<boolean> {
    const response = await fetch(`${this.baseUrl}/${id}`, {
      method: "DELETE",
      headers: this.getAuthHeaders()
    });
  
    return response.ok; 
  }
  
}
