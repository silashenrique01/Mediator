import { FormGroup, FormBuilder } from '@angular/forms';
import { NavComponent } from './../../nav/nav.component';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'src/app/services/auth.service';
import { JwtHelperService } from '@auth0/angular-jwt';

@Component({
  selector: 'app-Login',
  templateUrl: './Login.component.html',
  styleUrls: ['./Login.component.scss']
})
export class LoginComponent implements OnInit {

  titulo = "Login";
  model:any = {};
  username: string;
  nav: NavComponent;
  jwtHelper = new JwtHelperService();
  decodedToken: any;


  constructor(
    public router: Router,
    private auth: AuthService,
    private toastr: ToastrService

  ) { }

  ngOnInit() {
    if(localStorage.getItem('token') != null){
      var token = localStorage.getItem('token');
      this.decodedToken = this.jwtHelper.decodeToken(token);
      sessionStorage.setItem('username', this.decodedToken.unique_name);
      this.router.navigate(['/user']);
    }
  }

  login(){
    this.auth.login(this.model).subscribe(
      () => {
        this.router.navigate(['/user']);

      },
      error => {
        this.toastr.error('Erro ao fazer o login');
      }
    );
  }

}
