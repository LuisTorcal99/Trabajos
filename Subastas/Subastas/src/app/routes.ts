import { Routes } from '@angular/router';
import { PageNotFoundComponent } from './pages/page-not-found/page-not-found.component';
import { PrincipalComponent } from './pages/principal/principal.component';
import { ProductoComponent } from './pages/producto/producto.component'; 

const routeConfig: Routes = [
  {
    path: '',
    component: PrincipalComponent,
    title: 'Home page',
  },
  {
    path: 'producto/:id',
    component: ProductoComponent,
    title: 'Producto',
  },
  {
    path: '**',
    component: PageNotFoundComponent,
  },
];

export default routeConfig;
