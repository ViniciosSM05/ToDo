import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { ResponseModel } from 'src/app/shared/models/response/response.model';
import { TodoSaveResponseModel } from 'src/app/shared/models/todo/todo-save-response.model';
import { TodoSaveModel } from 'src/app/shared/models/todo/todo-save.model';
import { TodoTableModel } from 'src/app/shared/models/todo/todo-table.model';
import { BaseHttpService } from '../base-http.service';

@Injectable({
  providedIn: 'root'
})
export class TodoService extends BaseHttpService {
  constructor(private http: HttpClient) {
    super();
  }

  getTodosByUser(userId: string) : Observable<ResponseModel<TodoTableModel[]>> {
    return this.http.get<ResponseModel<TodoTableModel[]>>(`${this.baseUrl}/todo/getTodosByUser/${userId}`);
  }

  save(obj: TodoSaveModel) : Observable<ResponseModel<TodoSaveResponseModel>> {
    return this.http.post<ResponseModel<TodoSaveResponseModel>>(`${this.baseUrl}/todo/save`, obj);
  }

  remove(id: string) : Observable<ResponseModel<number>> {
    return this.http.delete<ResponseModel<number>>(`${this.baseUrl}/todo/remove/${id}`);
  }
}
