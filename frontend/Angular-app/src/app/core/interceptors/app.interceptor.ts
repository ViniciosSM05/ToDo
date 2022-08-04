import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';
import { HTTPCODE } from 'src/app/shared/enums/HTTPCODE';
import { MyMessageService } from '../services/message/my-message.service';

@Injectable()
export class AppInterceptor implements HttpInterceptor {
  constructor(private messageService: MyMessageService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    const API_TOKEN = localStorage.getItem("token") ?? "";
    const requestCopy = request.clone({ 
      setHeaders: { 
        Authorization: `Bearer ${API_TOKEN}`, 
      } 
    });

    return next.handle(requestCopy)
      .pipe(
        catchError((error: HttpErrorResponse) => {
          switch (error.status){
            case HTTPCODE.NOT_RESPONSE: break;
            case HTTPCODE.INTERNAL_SERVER_ERROR: break;
            case HTTPCODE.NOT_FOUND: break;
            case HTTPCODE.UNAUTHORIZED: break;
            case HTTPCODE.FORBIDDEN: break;
            default: break;
          }         
          return throwError(() => error);
        })
      );
  }
}
