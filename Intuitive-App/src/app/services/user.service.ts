import { User } from '../_models/User';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  baseUrl = 'https://localhost:5001/user'; //Alterar de acordo com o local host da API

  constructor(private http: HttpClient) { }

    getAllUser(): Observable<User[]>{
        return this.http.get<User[]>(this.baseUrl);
    }


    getUserByName(name: string): Observable<User[]>{
      return this.http.get<User[]>('${this.baseUrl}/getByName/${name}');
    }

    getUserById(userId: number): Observable<User[]>{
      return this.http.get<User[]>('${this.baseUrl}/${userId}');
    }

    criptPassword(user: User){
      return this.http.put(`${this.baseUrl}/${user.userId}`, user);
    }

    postUser(user: User) {
      console.log(user);
      return this.http.post(this.baseUrl, user);
    }

    putUser(user: User) {
      console.log(user);
      return this.http.put(`${this.baseUrl}/${user.userId}`, user);
    }

    deleteUser(user: User){
      console.log(user);
      return this.http.delete(`${this.baseUrl}/${user.name}`);
    }
}
