import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { User } from 'src/app/models/user.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private baseUrl = `${environment.localApiURL}/user`;  // Ensure `apiUrl` is set in `environment.ts`

  constructor(private http: HttpClient) {}

  register(user: User): Observable<any> {
    return this.http.post(`${this.baseUrl}/register`, user);
  }

//   login(credentials: { email: string, password: string }): Observable<any> {
//     return this.http.post(`${this.baseUrl}/login`, credentials);
//   }

//   logout(): void {
//     localStorage.removeItem('token');
//   }

//   getToken(): string | null {
//     return localStorage.getItem('token');
//   }

//   isAuthenticated(): boolean {
//     return !!this.getToken();
//   }

}
