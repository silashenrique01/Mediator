import { UsersComponent } from './Users/Users.component';
import { RegistrationComponent } from './Auth/Registration/Registration.component';
import { LoginComponent } from './Auth/Login/Login.component';
import { AuthComponent } from './Auth/Auth.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  {
    path: 'auth', component: AuthComponent,
    children: [
    { path: 'login', component: LoginComponent},
    { path: 'registration', component: RegistrationComponent}
   ]
  },
  {
    path: 'user', component: UsersComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
