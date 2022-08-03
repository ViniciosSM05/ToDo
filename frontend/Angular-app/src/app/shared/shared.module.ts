import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MenubarModule } from 'primeng/menubar';
import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';
import { ErrorTagComponent } from './components/error-tag/error-tag.component';
import { AutoFocusDirective } from './directives/autofocus/auto-focus.directive';
import { ErrorResponseFieldComponent } from './components/error-response-field/error-response-field.component';
import { HeaderComponent } from './components/layout/header/header.component';

@NgModule({
  declarations: [
    HeaderComponent,
    AutoFocusDirective,
    ErrorTagComponent,
    ErrorResponseFieldComponent,
  ],
  imports: [
    CommonModule,
    MenubarModule,
    InputTextModule,
    ButtonModule,
  ],
  exports: [
    HeaderComponent,
    ErrorTagComponent,
    ErrorResponseFieldComponent
  ]
})
export class SharedModule { }
