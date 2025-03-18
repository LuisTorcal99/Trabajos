import { Injectable } from '@angular/core';
import { PropuestaModel } from '../models/propuestaModel';
import { PropuestaGetModel } from '../models/propuestaGetModel';

@Injectable({
  providedIn: 'root'
})
export class PropuestaService {

  readonly baseUrl = 'https://localhost:7000/api/Propuesta/enviar';
  readonly baseUrl_get = 'https://localhost:7000/api/Propuesta';

  constructor() { }

  private getAuthHeaders(): { [key: string]: string } {
    const token = localStorage.getItem('token');
    if (!token) {
      throw new Error('No se encontró un token de autenticación.');
    }
    return {
      'Authorization': `Bearer ${token}`,
      'Content-Type': 'application/json'
    };
  }

  async postPropuesta(propuesta: PropuestaModel): Promise<PropuestaModel> {
    try {
      const response = await fetch(this.baseUrl, {
        method: "POST",
        headers: this.getAuthHeaders(), // ✅ Se usa el token aquí
        body: JSON.stringify(propuesta)
      });

      if (!response.ok) {
        throw new Error(`Error ${response.status}: ${response.statusText}`);
      }

      return await response.json();
    } catch (error) {
      console.error('Error en postPropuesta:', error);
      throw error; // Se vuelve a lanzar para manejarlo en el componente
    }
  }

  async updatePropuesta(propuesta: PropuestaGetModel): Promise<PropuestaGetModel> {
    try {
      const response = await fetch(`${this.baseUrl_get}/${propuesta.id}`, {
        method: 'PUT',
        headers: this.getAuthHeaders(),
        body: JSON.stringify(propuesta)
      });

      if (!response.ok) {
        throw new Error(`Error ${response.status}: ${response.statusText}`);
      }

      return await response.json();
    } catch (error) {
      console.error('Error al actualizar propuesta:', error);
      throw error; 
    }
  }

  async getPropuestaById(id: number): Promise<PropuestaGetModel | undefined> {
    try {
      const response = await fetch(`${this.baseUrl}/${id}`, {
        method: 'GET',
        headers: this.getAuthHeaders()
      });

      if (!response.ok) {
        throw new Error(`Error ${response.status}: ${response.statusText}`);
      }

      return await response.json();
    } catch (error) {
      console.error(`Error al obtener propuesta con ID ${id}:`, error);
      return undefined;
    }
  }

  async getPropuestas(): Promise<PropuestaModel[]> {
    try {
      const response = await fetch(`${this.baseUrl_get}`, {
        method: 'GET',
        headers: this.getAuthHeaders()
      });
      if (!response.ok) {
        throw new Error(`Error ${response.status}: ${response.statusText}`);
      }
      return await response.json();
    } catch (error) {
      console.error('Error al obtener todas las propuestas:', error);
      return [];
    }
  }

  // Método para obtener las propuestas del usuario logueado, comparando el email
  async getPropuestasPorEmail(): Promise<PropuestaModel[]> {
    try {
      const userEmail = localStorage.getItem('userEmail');
      if (!userEmail) {
        throw new Error('No se encontró el email del usuario en localStorage.');
      }

      const response = await fetch(`${this.baseUrl_get}/usuario/email/${userEmail}`, {
        method: 'GET',
        headers: this.getAuthHeaders()
      });

      if (!response.ok) {
        throw new Error(`Error ${response.status}: ${response.statusText}`);
      }

      return await response.json();
    } catch (error) {
      console.error('Error al obtener las propuestas por email:', error);
      return [];
    }
  }
}