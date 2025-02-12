import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { neriModel } from 'src/app/models/neriModel';
import { pujaModel } from 'src/app/models/pujaModel';
import { NeriService } from 'src/app/service/neri.service';

@Component({
  selector: 'app-producto',
  imports: [CommonModule, FormsModule],
  templateUrl: './producto.component.html',
  styleUrls: ['./producto.component.css']
})
export class ProductoComponent implements OnInit {
  route: ActivatedRoute = inject(ActivatedRoute); // Inyección de dependencias para acceder a la ruta
  product: neriModel | undefined; // Objeto que contendrá la información del producto
  valorIngresado: number = 0; // Valor que el usuario ingresa para hacer una puja
  valorMinimo: number = 0; // Mínimo permitido para pujar, inicialmente es el precio del producto
  pujas: pujaModel[] = [];

  // Constructor que inyecta el servicio NeriService para interactuar con la API
  constructor(private neriService: NeriService) {}

// Método que se ejecuta al inicializar el componente
async ngOnInit() {
  // Obtener el ID del producto desde la ruta
  const productId = parseInt(this.route.snapshot.params['id'], 10);

  // Obtener la información del producto
  this.product = await this.neriService.getProductById(productId);

  // Obtener las pujas asociadas al producto
  this.pujas = await this.neriService.getPrecios() ?? []; // Asegurarse de que sea un array

  // Inicializamos el valor del precio más alto
  let precioMasAlto = this.product?.price ?? 0;  // Si no hay producto, el precio es 0

  // Verificar si hay pujas y encontrar la más alta
  for (let puja of this.pujas) {
    if (puja.idSubasta == productId) {  // Si la puja corresponde al producto
      if (puja.precio > precioMasAlto) {  // Si la puja es más alta que el precio actual
        precioMasAlto = puja.precio;  // Actualizamos el precio más alto
      }
    }
  }
  // Actualizamos el valor mínimo con el precio más alto encontrado
  this.valorMinimo = precioMasAlto;
}


  // Método que se ejecuta cuando el usuario hace clic en "Pujar"
  async pujar() {
    if (!this.product) {
      console.error("No hay producto disponible para pujar."); // Si no hay producto, mostramos un error
      return;
    }

    // Verificar que el valor ingresado sea mayor o igual al valor mínimo de puja
    if (this.valorIngresado < this.valorMinimo) {
      console.error("El valor de la puja debe ser mayor o igual al precio actual."); // Mensaje de error si la puja es demasiado baja
      return;
    }

    // Crear una nueva puja con el ID de la subasta (producto) y el precio ingresado 
    const nuevaPuja: pujaModel = {
      idSubasta: this.product.id, // Usamos el ID del producto para la subasta
      precio: this.valorIngresado // El valor de la puja es el que ingresó el usuario
    };

    // Asegurarnos de que `pujas` es un array, y agregar la nueva puja a la lista de pujas
    const nuevasPujas = Array.isArray(this.product.pujas) ? [...this.product.pujas, nuevaPuja] : [nuevaPuja];

    // Crear un objeto para actualizar solo la lista de pujas del producto
    const productoPatch = {
    id: this.product.id, 
    name: this.product.name,
    photo: this.product.photo,
    price: this.product.price,
    pujas: nuevasPujas
};

    try {
      // Llamar al servicio para actualizar el producto con la nueva puja
      const updatedProduct = await this.neriService.updateProduct(this.product.id, productoPatch);
      console.log("Puja realizada con éxito:", updatedProduct);

      // Actualizamos el producto con los datos actualizados
      this.product.pujas = updatedProduct.pujas;

       // Inicializamos el valor mínimo con el primer precio de las pujas
        let precioMasAlto = 0;

        // Recorremos todas las pujas y buscamos el precio más alto
        for (let puja of this.product.pujas) {
          if (puja.precio > precioMasAlto) {
            precioMasAlto = puja.precio;
          }
        }

      // Actualizamos el valor mínimo con el precio más alto encontrado
      this.valorMinimo = precioMasAlto;
      
    } catch (error) {
      console.error("Error al pujar:", error); // Capturamos cualquier error que ocurra al intentar realizar la puja
    }
  }
}
