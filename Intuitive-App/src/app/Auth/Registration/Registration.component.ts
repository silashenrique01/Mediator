import { NavComponent } from './../../nav/nav.component';
import { Router } from '@angular/router';
import { AuthService } from './../../services/auth.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import {ToastrService} from 'ngx-toastr';
import { UserApplication } from 'src/app/_models/UserApplication';

@Component({
  selector: 'app-Registration',
  templateUrl: './Registration.component.html',
  styleUrls: ['./Registration.component.scss']
})
export class RegistrationComponent implements OnInit {

  user: UserApplication;
  registerForm: FormGroup;

  constructor(
    public router: Router,
    private auth: AuthService,
    public fb: FormBuilder,
    private toastr: ToastrService
  ) { }

  ngOnInit() {
    this.validation();
  }

  validation(){

    this.registerForm = this.fb.group({
      fullName: ['', Validators.required],
        email: ['', [Validators.required, Validators.email]],
        userName: ['', Validators.required],
        passwords: this.fb.group({

          password: ['', [Validators.required, Validators.minLength(4)]],
          confirmPassword: ['', Validators.required]
        }, { validator: this.compareSenhas})

    });
  }

  compareSenhas(fb: FormGroup){
    const confirmarSenhaCtrl = fb.get('confirmPassword');
    if(confirmarSenhaCtrl.errors == null || 'mismatch' in confirmarSenhaCtrl.errors){
      if(fb.get('password').value != confirmarSenhaCtrl.value){
        confirmarSenhaCtrl.setErrors({mismatch: true});
      }else{
        confirmarSenhaCtrl.setErrors(null);
      }
    }

  }

  cadastrarUsuario(){
    if(this.registerForm.valid){
      this.user = Object.assign(
        {
          password: this.registerForm.get('passwords.password').value},
          this.registerForm.value);
          this.auth.register(this.user).subscribe(
            () => {
              this.router.navigate(['/auth/login']);
              this.toastr.success('Cadastro realizado com sucesso');

            },
            error =>{
              if(error.code == 'DuplicateUserName'){
                    this.toastr.error('Cadastro Duplicado!');
              }else{
                this.toastr.error(`Erro no cadastro! CODE: ${error}`);
              }
            });
    }
  }

}
