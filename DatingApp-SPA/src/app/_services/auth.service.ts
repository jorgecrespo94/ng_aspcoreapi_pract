import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';

// root module = app.module
@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = 'http://localhost:5000/api/auth/';
constructor(private http: HttpClient) { }

login(model: any) {
  return this.http.post(
    this.baseUrl + 'login',
    model
  ).pipe(
    map((response: any) => {
      const user = response;
      if (user) {
        localStorage.setItem('token', user.token);
        localStorage.setItem('username', user.username);
      }
    })
  );
}

register(model: any) {
  return this.http.post(this.baseUrl + 'register', model)s;
}

}
// pipe - allow us to chain rxgx operator from a observables of a response
