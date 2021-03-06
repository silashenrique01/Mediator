import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = 'https://localhost:5001/auth/'; //Alterar de acordo com o local host da API
  jwtHelper = new JwtHelperService();
  decodedToken: any;
  constructor(private http: HttpClient) { }

  login(model: any) {
    return this.http.post(`${this.baseUrl}login`, model).pipe(
      map((response: any) => {
        const user = response;
        console.log(user);
        if(user.success){
        localStorage.setItem('token', user.token);
        this.decodedToken = this.jwtHelper.decodeToken(user.token);
        sessionStorage.setItem('username', this.decodedToken.unique_name);
        }
      })
    )


  }

  register(model: any){
    return this.http.post(`${this.baseUrl}register`, model)

  }

  loggedIn(){
    const token = localStorage.getItem('token');
    return !this.jwtHelper.isTokenExpired(token);
  }

}
