import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../service/auth.service';
import { RegisterModel } from '../../models/registerModel';
import { firstValueFrom } from 'rxjs';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
  standalone: true,
  imports: [FormsModule, CommonModule]
})
export class RegisterComponent {
  name: string = '';
  apellidos: string = '';
  email: string = '';
  password: string = '';
  confirmPassword: string = '';

  constructor(private authService: AuthService, private router: Router) {}

  async register() {
    if (!this.name ||!this.apellidos || !this.email || !this.password || !this.confirmPassword) {
      alert('Todos los campos son obligatorios.');
      return;
    }

    if (this.password !== this.confirmPassword) {
      alert('Las contraseñas no coinciden.');
      return;
    }

    const registerModel: RegisterModel = {
      name: this.name,
      apellidos:this.apellidos,
      email: this.email,
      password: this.password,
      rol: 'alumno'
    };

    try {
      await firstValueFrom(this.authService.register(registerModel));
      alert('Usuario registrado con éxito');
      this.router.navigate(['/']);
    } catch (error: any) {
      alert(error.message);
    }
  }

  goToLogin() {
    this.router.navigate(['/']);
  }
}