import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/core/authentication/auth.service';
import { TodoService } from 'src/app/core/services/http/todo/todo.service';
import { MyMessageService } from 'src/app/core/services/message/my-message.service';
import { getConfigCalendarBR } from 'src/app/shared/helpers/calendar-br.helper';
import { CalendarConfigModel } from 'src/app/shared/models/helper/calendar/calendar-config.model';
import { ResponseModel } from 'src/app/shared/models/response/response.model';
import { TodoTableModel } from 'src/app/shared/models/todo/todo-table.model';
import { PrimeNGConfig } from 'primeng/api';

@Component({
  selector: 'app-todo-table',
  templateUrl: './todo-table.component.html',
  styleUrls: ['./todo-table.component.scss']
})
export class TodoTableComponent implements OnInit {
  calendar_br: CalendarConfigModel = getConfigCalendarBR();
  displayModalSave: boolean = false;
  todos!: TodoTableModel[];
  constructor(
    private todoService: TodoService, 
    private authService: AuthService,
    private messageService: MyMessageService,
    private config: PrimeNGConfig) 
  { 
    this.config.setTranslation(this.calendar_br);
  }

  ngOnInit(): void {
    this.refreshTable();    
  }

  refreshTable(): void {
    this.todoService.getTodosByUser(this.authService.userId)
      .subscribe({
        error: (resp: ResponseModel<TodoTableModel[]>) => {
          this.messageService.showErrorMessage(resp.error);
        },
        next: (resp) => {
          this.todos = resp.data;
        }
      });
  }

  showDialogSave(): void {
    this.displayModalSave = true;
  }
}
