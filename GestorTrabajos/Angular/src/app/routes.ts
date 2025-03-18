import { Routes } from '@angular/router';
import { PageNotFoundComponent } from './pages/page-not-found/page-not-found.component';
import { HomeComponent } from './pages/home/home.component';
import { FormularioComponent } from './pages/formulario/formulario.component';
import { LoginComponent } from './pages/login/login.component';
import { RegisterComponent } from './pages/register/register.component';
import { EstadoComponent } from './pages/estado/estado.component';


const routeConfig: Routes = [
  {
    path: '',
    component: LoginComponent,
    title: 'Login page',
  },
  {
    path: 'registro',
    component: RegisterComponent,
    title: 'Register page',
  },
  {
    path: 'principal',
    component: HomeComponent,
    title: 'Home page',
  },
  {
    path: 'formulario',
    component: FormularioComponent,
    title: 'formulario',
  },
  {
    path: 'estado',
    component: EstadoComponent,
    title: 'estado',
  },
  {
    path: '**',
    component: PageNotFoundComponent,
  },
];

export default routeConfig;
