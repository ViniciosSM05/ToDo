import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CardModule } from 'primeng/card';
import { InputTextModule } from 'primeng/inputtext';
import { PasswordModule } from 'primeng/password';
import { ButtonModule } from 'primeng/button';

import { UserRoutingModule } from './user-routing.module';
import { UserRegisterComponent } from './user-register/user-register.component';
import { ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from '../../shared/shared.module';
import { UserLoginComponent } from './user-login/user-login.component'

@NgModule({
  declarations: [
    UserRegisterComponent,
    UserLoginComponent
  ],
  imports: [
    CommonModule,
    CardModule,
    InputTextModule,
    PasswordModule,
    ButtonModule,
    ReactiveFormsModule,
    UserRoutingModule,
    SharedModule,
  ],
  exports: [
    UserRegisterComponent
  ]
})
export class UserModule { }
