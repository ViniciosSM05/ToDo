import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MenubarModule } from 'primeng/menubar';
import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';
import { ToastModule } from 'primeng/toast';
import { MyMessageService } from './services/message/my-message.service';
import { MessageService } from 'primeng/api';
import { UserService } from './services/http/user/user.service';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BaseHttpService } from './services/http/base-http.service';
import { AppInterceptor } from './interceptors/app.interceptor';
import { AuthService } from './authentication/auth.service';
import { LoggedInGuard } from './guards/logged-in.guard';
import { GuestGuard } from './guards/guest.guard';
import { TodoService } from './services/http/todo/todo.service';
import { HeaderService } from './services/header/header.service';

@NgModule({
  declarations: [
  ],
  imports: [
    CommonModule,
    MenubarModule,
    InputTextModule,
    ButtonModule,
    ToastModule,
    HttpClientModule,
  ],
  exports: [
  ],
  providers: [
    MessageService,
    MyMessageService,
    BaseHttpService,
    UserService,
    TodoService,
    HeaderService,
    AuthService,
    GuestGuard,
    LoggedInGuard,
    { provide: HTTP_INTERCEPTORS, useClass: AppInterceptor, multi: true },
  ]
})
export class CoreModule { }
