import { Injectable } from '@angular/core';
import { ObjetoTresDTO } from '../models/ObjetoTresDTO';  // Importar el nuevo DTO
import { CreateObjetoModel } from '../models/CreateObjetoModel';  // Modelo de creación (si es necesario)

@Injectable({
  providedIn: 'root'
})
export class ObjetoTresService {
  readonly baseUrl = 'http://localhost:7000/api/ObjetoTres';  // Base URL para los objetos tres

  constructor() {}

  private getAuthHeaders(): { [key: string]: string } {
    const token = localStorage.getItem('token');
    return {
      'Authorization': `Bearer ${token}`,
      'Content-Type': 'application/json'
    };
  }

  // Obtener todos los objetos Tres
  async getAllProduct(): Promise<ObjetoTresDTO[]> {
    const response = await fetch(this.baseUrl, {
      method: 'GET',
      headers: this.getAuthHeaders()
    });
    return (await response.json()) ?? [];
  }

  // Obtener un objeto Tres por ID
  async getProductById(id: number): Promise<ObjetoTresDTO | undefined> {
    const response = await fetch(`${this.baseUrl}/${id}`, {
      method: 'GET',
      headers: this.getAuthHeaders()
    });
    return (await response.json()) as ObjetoTresDTO | undefined;
  }

  // Obtener objetos Tres relacionados a través de los IDs en `idObjeto`
  async getRelatedObjects(ids: number[]): Promise<ObjetoTresDTO[]> {
    // Llamar a la API para obtener los objetos tres relacionados
    const response = await fetch(`${this.baseUrl}/getByIds`, {
      method: 'POST',
      headers: this.getAuthHeaders(),
      body: JSON.stringify({ ids })  // Enviar los IDs de los objetos tres relacionados
    });

    return (await response.json()) ?? [];
  }

  // Actualizar un objeto Tres
  async updateProduct(id: number, partialProduct: Partial<ObjetoTresDTO>): Promise<ObjetoTresDTO> {
    const response = await fetch(`${this.baseUrl}/${id}`, {
      method: "PATCH",
      headers: this.getAuthHeaders(),
      body: JSON.stringify(partialProduct)
    });
    return await response.json();
  }

  // Crear un nuevo objeto Tres
  async createProduct(product: CreateObjetoModel): Promise<CreateObjetoModel> {
    const response = await fetch(this.baseUrl, {
      method: "POST",
      headers: this.getAuthHeaders(),
      body: JSON.stringify(product)
    });

    return await response.json();
  }

  // Eliminar un objeto Tres
  async deleteProduct(id: number): Promise<boolean> {
    const response = await fetch(`${this.baseUrl}/${id}`, {
      method: "DELETE",
      headers: this.getAuthHeaders()
    });

    return response.ok;
  }
}
