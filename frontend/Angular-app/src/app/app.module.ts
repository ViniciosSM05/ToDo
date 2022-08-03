import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CoreModule } from './core/core.module';
import { SharedModule } from './shared/shared.module';
import { ToastModule } from 'primeng/toast';
import { UserModule } from './modules/user/user.module';
import { TodoModule } from './modules/todo/todo.module';

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    CoreModule,
    SharedModule,
    ToastModule,
    UserModule,
    TodoModule,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
