import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms'; 
import { ObjetoService } from 'src/app/service/objeto.service';
import { CreateObjetoModel } from 'src/app/models/CreateObjetoModel';
import { Location } from '@angular/common';

@Component({
  selector: 'app-anadir',
  standalone: true,
  imports: [FormsModule], 
  templateUrl: './anadir.component.html',
  styleUrls: ['./anadir.component.css']
})
export class AnadirComponent { 
  nuevoObjeto: CreateObjetoModel = { 
    nombre: '', 
    email: '',
  };

  constructor(private objetoService: ObjetoService, private location: Location) {} 

  async crearObjeto() {
    if (this.nuevoObjeto.nombre.trim() === '' && this.nuevoObjeto.email.trim()) {
      alert('El nombre o email es obligatorio.');
      return;
    }

    try {
      const resultado = await this.objetoService.createProduct(this.nuevoObjeto);
      if (resultado) {
        console.log('Producto creado:', resultado);
        alert('Producto creado exitosamente');
      } else {
        alert('Error al crear el producto.');
      }
    } catch (error) {
      console.error('Error al crear el producto:', error);
      alert('Error al conectar con el servidor.');
    }
  }

  goBack() {
    this.location.back();
  }
}
