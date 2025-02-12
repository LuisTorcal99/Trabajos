import {Component} from '@angular/core';
import {PrincipalComponent} from './pages/principal/principal.component';
import {RouterModule} from '@angular/router';

@Component({
  selector: 'app-root',
  imports: [RouterModule],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  title = 'Subasta';
}
