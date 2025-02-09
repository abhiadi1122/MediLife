import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { User, UserLoginRequest } from 'src/app/models/user.model';
import {jwtDecode} from 'jwt-decode';
import { Router } from '@angular/router';
import{routeConstants} from 'src/app/shared/constant/routeConstants'

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private baseUrl = `${environment.localApiURL}/user`;  // API URL

  constructor(private http: HttpClient, private router: Router) {}

  /** ✅ Register New User */
  register(user: User): Observable<any> {
    return this.http.post(`${this.baseUrl}/register`, user);
  }

  /** ✅ Login and Get Token */
  login(user: UserLoginRequest): Observable<any> {
    return this.http.post(`${this.baseUrl}/login`, user);
  }

  /** ✅ Store Token in LocalStorage */
  storeToken(token: string) {
    localStorage.setItem('jwtToken', token);
    this.redirectUser();
  }

  /** ✅ Retrieve Token */
  getToken(): string | null {
    return localStorage.getItem('jwtToken');
  }

  /** ✅ Check If User is Authenticated */
  isAuthenticated(): boolean {
    debugger;
    return !!this.getToken();
  }

  /** ✅ Decode Token and Get User Role */
  getUserRole(): string | null {
    const token = this.getToken();
    if (!token) return null;

    try {
      const decodedToken: any = jwtDecode(token);
      return decodedToken.role || null;
    } catch (error) {
      console.error('Invalid token:', error);
      return null;
    }
  }

  /** ✅ Logout and Clear Token */
  logout() {
    localStorage.removeItem('jwtToken');
    this.router.navigate([routeConstants.Auth_Login_URL]);
  }

  /** ✅ Redirect Based on User Role */
  private redirectUser() {
    debugger;
    const userRole = this.getUserRole();
    
    if (userRole === 'Admin') {
      this.router.navigate([routeConstants.Admin_Dashboard_URL]); // Redirect Admin
    } else if (userRole === 'User') {
      this.router.navigate([routeConstants.User_Dashboard_URL]); // Redirect Normal User
    } else {
      this.router.navigate([routeConstants.Auth_Login_URL]); // Redirect to Login on Error
    }
  }
}
