import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { NavbarComponent } from 'src/app/components/navbar/navbar.component';
import { AuthService } from 'src/app/service/auth.service';
import { PropuestaService } from 'src/app/service/propuesta.service';

@Component({
  selector: 'app-estado',
  imports: [NavbarComponent, CommonModule, FormsModule],
  templateUrl: './estado.component.html',
  styleUrls: ['./estado.component.css']
})
export class EstadoComponent {
  propuestas: any[] = [];
  userEmail: string | null = localStorage.getItem('userEmail'); // Obtener el email del usuario logueado desde localStorage

  constructor(private propuestaService: PropuestaService, private authService: AuthService) {}

  ngOnInit() {
    // Obtener todas las propuestas
    this.propuestaService.getPropuestas().then(allPropuestas => {
      // Filtrar propuestas que coincidan con el email del usuario logueado
      const filteredPropuestas = allPropuestas.filter((propuesta: any) => propuesta.email === this.userEmail);
      this.propuestas = filteredPropuestas;
    }).catch(error => console.error('Error obteniendo todas las propuestas:', error));
  }

  // Retornar la clase según el estado de la propuesta
  getEstadoClass(estado: string): string {
    switch (estado) {
      case 'Enviada':
        return 'enviada';
      case 'Aceptada':
        return 'aceptada';
      case 'Rechazada':
        return 'rechazada';
      case 'Requiere Ampliacion':
        return 'informacion-adicional';
      default:
        return '';
    }
  }

// Método para guardar los cambios en la propuesta
async guardarCambios(propuesta: any) {
  try {
    // Hacer un put para actualizar la propuesta
    const updatedPropuesta = {
      ...propuesta,
      estado: 'Enviada', // Cambiar el estado después de la ampliación
    };

    await this.propuestaService.updatePropuesta(updatedPropuesta);
    alert('Cambios guardados con éxito.');
  } catch (error) {
    console.error('Error al guardar los cambios:', error);
    alert('Hubo un error al guardar los cambios. Por favor, inténtalo de nuevo.');
  }
}
}
