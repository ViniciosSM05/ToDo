import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { UserLoginResponseModel } from 'src/app/shared/models/auth/user-login-response.model';
import { UserLoginModel } from 'src/app/shared/models/auth/user-login.model';
import { JwtHelperService } from '@auth0/angular-jwt';
import { UserInfoModel } from 'src/app/shared/models/user/user-info';
import { ResponseModel } from 'src/app/shared/models/response/response.model';

const jwtHelper = new JwtHelperService();
@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(private http: HttpClient) { }

  login(obj: UserLoginModel): Observable<ResponseModel<UserLoginResponseModel>> {
    return this.http.post<ResponseModel<UserLoginResponseModel>>('http://localhost:5010/api/user/login', obj)
      .pipe(
        map(resp => {
          this.setAuth(resp.data);
          return resp;
        })
      );
  }

  logout(): void {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
  }

  get isLoggedIn(): boolean {   
    const token = localStorage.getItem('token');
    return !jwtHelper.isTokenExpired(token ?? "");
  }

  get isLoggedOut(): boolean {
    return !this.isLoggedIn
  }

  get user(): UserInfoModel | null {
    const userStr = localStorage.getItem('user');
    return userStr ? JSON.parse(userStr) as UserInfoModel : null;
  }

  get userId() : string {
    return this.user?.id ?? "";
  }

  private setAuth(result: UserLoginResponseModel): void {
    if (result && result.token) {
      localStorage.setItem('token', result.token);
      delete result['token'];
      localStorage.setItem('user', JSON.stringify(result));     
    }
  }
}
