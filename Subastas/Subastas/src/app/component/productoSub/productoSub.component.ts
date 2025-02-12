import { Component,Input} from '@angular/core';
import { neriModel } from '../../models/neriModel';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-productoSub',
  imports: [RouterModule, CommonModule],
  templateUrl: './productoSub.component.html',
  styleUrls: ['./productoSub.component.css']
})
export class ProductoComponent{

  @Input() neriModel!: neriModel;

}
