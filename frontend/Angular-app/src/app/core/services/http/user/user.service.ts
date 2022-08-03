import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BaseHttpService } from '../base-http.service';
import { UserSaveModel } from 'src/app/shared/models/user/user-save.model';
import { ResponseModel } from 'src/app/shared/models/response/response.model';
import { UserSaveResponseModel } from 'src/app/shared/models/user/user-save-response.model';

@Injectable({
  providedIn: 'root'
})
export class UserService extends BaseHttpService {
  constructor(private http: HttpClient) {
    super();
  }

  register(obj: UserSaveModel) : Observable<ResponseModel<UserSaveResponseModel>> {
    return this.http.post<ResponseModel<UserSaveResponseModel>>(`${this.baseUrl}/user/save`, obj);
  }

}
