import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { NavbarComponent } from 'src/app/components/navbar/navbar.component';


@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule,NavbarComponent],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {

}
