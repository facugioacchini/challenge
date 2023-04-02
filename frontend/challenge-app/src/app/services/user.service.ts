import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { User } from '../models/user';

@Injectable()
export class UserService {
    constructor(private http: HttpClient) { }

    url = environment.url;

    public createUser(user: User) {
        return this.http.post<User>(`${this.url}User/SaveUser`, {
                Nombre: user.nombre, 
                Apellido: user.apellido, 
                Email: user.email, 
                FechaNacimiento: user.fechaNacimiento, 
                Telefono: user.telefono, 
                Pais: user.country,
                Info: user.info
        }, {observe: "response"});
    }
}