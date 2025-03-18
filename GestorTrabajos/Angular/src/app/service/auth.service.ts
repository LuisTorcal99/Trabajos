import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { LoginModel } from '../models/loginModel';
import { RegisterModel } from '../models/registerModel';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  readonly baseUrl = 'https://localhost:7000/api/users';
  private loginUrl = `${this.baseUrl}/login`;
  private registerUrl = `${this.baseUrl}/register`;
  private token: string | null = null;

  constructor() {
    // Recuperar el token desde localStorage al inicializar el servicio
    this.token = localStorage.getItem('authToken');
  }

  login(credentials: LoginModel): Observable<any> {
    return new Observable<any>(observer => {
      fetch(this.loginUrl, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(credentials)
      })
      .then(response => response.json())
      .then(data => {
        console.log('Login response:', data);
        if (data?.result?.token) {
          this.setToken(data.result.token);
          localStorage.setItem('userEmail', data.result.user.email); // ✅ Guarda el email
        } else {
          console.warn('⚠️ No se recibió un token válido:', data);
        }
        observer.next(data);
        observer.complete();
      })
      .catch(error => observer.error(error));
    });
  }
  

  register(registroDto: RegisterModel): Observable<any> {
    return new Observable<any>(observer => {
      fetch(this.registerUrl, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(registroDto)
      })
      .then(async response => {
        const data = await response.json();
        console.log('Registro - Respuesta de la API:', data); 
  
        if (response.ok) {
          observer.next(data);
          observer.complete();
        } else {
          observer.error(new Error(data?.message || 'Erroren el registro, compruebe los campos'));
        }
      })
      .catch(error => observer.error(error));
    });
  }
  
  setToken(token: string): void {
    this.token = token;
    localStorage.setItem('authToken', token);
  }

  getToken(): string | null {
    return this.token || localStorage.getItem('authToken');
  }

  logout(): void {
    this.token = null;
    localStorage.removeItem('authToken');
  }
  // ✅ Nuevo método para obtener el ID del usuario desde el token
  getUserId(): number | null {
    const token = this.getToken();
    if (!token) return null;

    try {
      const payload = JSON.parse(atob(token.split('.')[1])); // Decodifica el token
      return payload.userId; // Ajusta según cómo se estructure tu token
    } catch (error) {
      console.error('Error al decodificar el token:', error);
      return null;
    }
  }

  getUserEmail(): string | null {
    return localStorage.getItem('userEmail');
  }  
  
}