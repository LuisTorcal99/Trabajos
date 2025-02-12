import { Component } from '@angular/core';
import { NeriService } from 'src/app/service/neri.service'; 
import { neriModel } from '../../models/neriModel';
import { CommonModule } from '@angular/common';
import { ProductoComponent } from '../../component/productoSub/productoSub.component';


@Component({
  selector: 'app-principal',
  imports: [CommonModule, ProductoComponent],
  templateUrl: './principal.component.html',
  styleUrls: ['./principal.component.css']
})
export class PrincipalComponent {

  neriList: neriModel[] = [];

  constructor(private neriService: NeriService) { 
    this.neriService.getAllProduct().then((neriList: neriModel[]) => {
      this.neriList = neriList;
  });
}
}
