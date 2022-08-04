import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TodoRoutingModule } from './todo-routing.module';
import { TodoViewComponent } from './todo-view/todo-view.component';
import { TodoTableComponent } from './todo-table/todo-table.component';
import { CardModule } from 'primeng/card';
import { ButtonModule } from 'primeng/button';
import { TableModule } from 'primeng/table';
import { DialogModule } from 'primeng/dialog';
import { InputTextModule } from 'primeng/inputtext';
import { CalendarModule } from 'primeng/calendar';
import { ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from 'src/app/shared/shared.module';
import { ToastModule } from 'primeng/toast';
@NgModule({
  declarations: [
    TodoViewComponent,
    TodoTableComponent
  ],
  imports: [
    CommonModule,
    CardModule,
    TodoRoutingModule,
    ButtonModule,
    TableModule,
    DialogModule,
    InputTextModule,
    CalendarModule,
    ReactiveFormsModule,
    ToastModule,
    SharedModule
  ],
  exports: [
    TodoTableComponent
  ]
})
export class TodoModule { }
