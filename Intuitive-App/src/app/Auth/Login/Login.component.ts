import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-Login',
  templateUrl: './Login.component.html',
  styleUrls: ['./Login.component.scss']
})
export class LoginComponent implements OnInit {

  titulo = "Login";
  model:any = {};

  constructor(public router: Router) { }

  ngOnInit() {
  }

  login(){
    console.log();
  }

}
