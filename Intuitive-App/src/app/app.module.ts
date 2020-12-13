import { ToastrModule} from 'ngx-toastr';
import { TituloComponent } from './_shared/Titulo/Titulo.component';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ModalModule } from 'ngx-bootstrap/modal';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { HttpClientModule} from '@angular/common/http';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { UserService } from './services/user.service';

import { AppComponent } from './app.component';
import { UsersComponent } from './Users/Users.component';
import { RegistrationComponent } from './Auth/Registration/Registration.component';
import { LoginComponent } from './Auth/Login/Login.component';
import { DateTimeFormatPipePipe } from './_helpers/DateTimeFormatPipe.pipe';
import { AuthComponent } from './Auth/Auth.component';

@NgModule({
  declarations: [
    AppComponent,
    UsersComponent,
    DateTimeFormatPipePipe,
    AuthComponent,
    LoginComponent,
    RegistrationComponent,
    TituloComponent
   ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    BsDatepickerModule.forRoot(),
    BsDropdownModule.forRoot(),
    BsDatepickerModule.forRoot(),
    TooltipModule.forRoot(),
    ModalModule.forRoot(),
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ToastrModule.forRoot(),
    ReactiveFormsModule
  ],
  providers: [
    UserService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
