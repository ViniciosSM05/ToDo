import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { GuestGuard } from 'src/app/core/guards/guest.guard';
import { UserLoginComponent } from './user-login/user-login.component';
import { UserRegisterComponent } from './user-register/user-register.component';

const routes: Routes = [
  { path: 'register', component: UserRegisterComponent, canActivate: [GuestGuard] },
  { path: 'login', component: UserLoginComponent, canActivate: [GuestGuard] },
  { path: 'login/:email', component: UserLoginComponent, canActivate: [GuestGuard] },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserRoutingModule { }
