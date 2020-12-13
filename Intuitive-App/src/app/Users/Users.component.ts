import { ToastrService } from 'ngx-toastr';
import { User } from '../_models/User';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BsModalService } from 'ngx-bootstrap/modal';
import { UserService } from '../services/user.service';
import { defineLocale } from 'ngx-bootstrap/chronos';
import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { ptBrLocale } from 'ngx-bootstrap/locale';

defineLocale('pt-br', ptBrLocale);

@Component({
  selector: 'app-Users',
  templateUrl: './Users.component.html',
  styleUrls: ['./Users.component.css']
})


export class UsersComponent implements OnInit {

  _filtroLista = '';

  users: User[];
  user: User;
  modoSalvar = '';
  bodyDeletarUser = '';
  usersFiltrados: User[];
  registerForm: FormGroup;

  constructor(
      private userService: UserService,
      private modalService: BsModalService,
      private fb: FormBuilder,
      private localeService: BsLocaleService,
      private toastr: ToastrService
  ) {

      this.localeService.use('pt-br');
   }

  ngOnInit() {
    this.validation();
    this.getUsers();
  }

  openModal(template: any){
    template.show();
  }

  editarUser(User: User, template: any){
    this.modoSalvar = 'put';
    this.openModal(template);
    this.user = User;
    this.registerForm.patchValue(User);
  }

  novoUser(template: any){
    this.modoSalvar = 'post';
    this.openModal(template);
  }

  get filtroLista(): string{
      return this._filtroLista;
  }
  set filtroLista(value: string){
      this._filtroLista = value;
      console.log(this._filtroLista);
      this.usersFiltrados = this._filtroLista ? this.filtrarUsers(this._filtroLista) : this.users;
  }

  filtrarUsers(filtrarPor: string): User[]{
      filtrarPor = filtrarPor.toLocaleLowerCase();

      return this.users.filter(
         User => User.name.toLocaleLowerCase().indexOf(filtrarPor) !== -1
      );
  }



  getUsers(){
    this.userService.getAllUser().subscribe(
        (_Users: User[]) => {
          this.users = _Users;
          this.users = this.users;
          console.log(this._filtroLista);
        }, error => {
            this.toastr.error(`Erro ao listar usuarios: ${error}`);
        }
    );
  }

  validation() {
    this.registerForm = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(100)]],
      dtNasc: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      username: ['', Validators.required],
      password: ['', Validators.required],
    });
  }

  excluirUser(User: User, template: any) {
    this.openModal(template);
    this.user = User;
    this.bodyDeletarUser = `Tem certeza que deseja excluir o usuário: ${this.user.name}, Código: ${this.user.userId}`;

  }

  confirmeDelete(template: any) {
    console.log(template);
    this.userService.deleteUser(this.user).subscribe(
      () => {
        template.hide();
        this.getUsers();
        this.toastr.success(`Sucesso ao deletar usuario`);
      }, error =>
      {
        console.log(error);
        this.toastr.success(`Erro ao deletar usuario`);
      }
    );
  }

  salvarAlteracao(template: any) {
    if (this.registerForm.valid) {

      if(this.modoSalvar === 'post'){
        this.user = Object.assign({}, this.registerForm.value);
        this.userService.postUser(this.user).subscribe(
          (novoUser: User) => {
            template.hide();
            this.getUsers();
            this.toastr.success(`Sucesso ao cadastrar usuario`);
            console.log(novoUser);
          }, error => {
            console.log(error);
            this.toastr.success(`Erro ao cadastrar usuario`);

          }
        );
      }
      else{
        this.user = Object.assign({userId: this.user.userId}, this.registerForm.value);
        this.userService.putUser(this.user).subscribe(
          () => {
            template.hide();
            this.getUsers();
            this.toastr.success(`Sucesso ao editar usuario`);
          }, error => {
            console.log(error);
            this.toastr.success(`Erro ao editar usuario`);
          }
        );
      }
    }
  }
}
