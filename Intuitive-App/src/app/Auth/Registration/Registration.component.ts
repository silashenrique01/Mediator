import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import {ToastrService} from 'ngx-toastr';

@Component({
  selector: 'app-Registration',
  templateUrl: './Registration.component.html',
  styleUrls: ['./Registration.component.scss']
})
export class RegistrationComponent implements OnInit {

  registerForm: FormGroup;
  constructor(

    public fb: FormBuilder,
    private toastr: ToastrService
  ) { }

  ngOnInit() {
  }

  validation(){
    this.registerForm = this.fb.group({
      fullName : ['', Validators.required],
        email : ['', Validators.required, Validators.email],
        userName : ['', Validators.required],
        passwords : this.fb.group({

          password : ['', Validators.required, Validators.minLength(4)],
          confirmPassword : ['', Validators.required]
        })

    });
  }

  cadastrarUsuario(){
    console.log('Cadastrar Usuario');
  }

}
