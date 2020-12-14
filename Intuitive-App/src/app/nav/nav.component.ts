import { UserService } from './../services/user.service';
import { AuthService } from './../services/auth.service';
import { UserApplication } from './../_models/UserApplication';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss']
})
export class NavComponent implements OnInit {

  nome:string;
  userApplication: UserApplication;

  constructor(
    public router: Router,
    private auth: AuthService,
    private toastr: ToastrService

  ) { }

  ngOnInit() {
  }

  LoggedIn(){
    return this.auth.loggedIn();
  }

  entrar(){
    return this.router.navigate(['/auth/login']);
  }

  logout(){
    localStorage.removeItem('token');
    this.toastr.show('Sessão encerrada!');
    this.router.navigate(['/auth/login']);
  }

  getUsername(nome: string){
    return this.nome = nome
  }

}
