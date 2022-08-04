import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/core/authentication/auth.service';
import { TodoService } from 'src/app/core/services/http/todo/todo.service';
import { MyMessageService } from 'src/app/core/services/message/my-message.service';
import { getConfigCalendarBR } from 'src/app/shared/helpers/calendar-br.helper';
import { ResponseModel } from 'src/app/shared/models/response/response.model';
import { TodoTableModel } from 'src/app/shared/models/todo/todo-table.model';
import { PrimeNGConfig } from 'primeng/api';
import { BaseForm } from 'src/app/core/classes/BaseForm';
import { TodoSaveResponseModel } from 'src/app/shared/models/todo/todo-save-response.model';
import { HttpErrorResponse } from '@angular/common/http';
import { FormBuilder, Validators } from '@angular/forms';
import { TodoSaveModel } from 'src/app/shared/models/todo/todo-save.model';

@Component({
  selector: 'app-todo-table',
  templateUrl: './todo-table.component.html',
  styleUrls: ['./todo-table.component.scss']
})
export class TodoTableComponent extends BaseForm<TodoSaveResponseModel> implements OnInit {
  minDate: Date = new Date(); displayModalSave: boolean = false; 
  form = this.fb.group({
    id: [""],
    code: ["", Validators.required], 
    description: ["", [Validators.required]],
    date: [new Date(), Validators.required],
  });

  constructor(
    private fb: FormBuilder,
    private todoService: TodoService, 
    private authService: AuthService,
    private messageService: MyMessageService,
    private config: PrimeNGConfig) 
  { 
    super();
    this.config.setTranslation(getConfigCalendarBR());
  }

  ngOnInit(): void {
    this.refreshTable();    
  }

  todos!: TodoTableModel[];
  refreshTable(): void {
    this.todoService.getTodosByUser(this.authService.userId)
      .subscribe({
        error: (resp: HttpErrorResponse) => {
          this.messageService.showErrorMessage("Erro ao buscar dados", resp);
        },
        next: resp => {
          this.todos = resp.data;
        }
      });
  }

  openDialogSave(obj?: TodoTableModel): void {
    this.setDisplayDialogSave(true);
    this.form.setValue({ 
      id: obj?.id ?? "", 
      code: obj?.code ?? "", 
      description: obj?.description ?? "",
      date: obj?.date ? new Date(`${obj.date.toString()}Z`) : null
    });
  }

  hideDialogSave(): void {
    this.setDisplayDialogSave(false);
  }

  private setDisplayDialogSave(visible: boolean): void {
    this.displayModalSave = visible;
  }

  handleSave(): void {
    const { valid, value } = this.form;
    if (valid) {
      const obj: TodoSaveModel = {
        id: value.id || null,
        code: value.code ?? "",
        description: value.description ?? "",
        date: value.date ?? new Date(),
        userId: this.authService.userId
      };

      this.todoService.save(obj)
        .subscribe({
          error: (resp: HttpErrorResponse) => {
            this.setResponse = resp.error;
            this.messageService.showErrorMessage("Ops! Houve inconsistências", resp);
          },
          next: resp => {
            this.messageService.showSuccessMessage(`ToDo ${value.id ? 'alterado' : 'adicionado'} com sucesso!`)
            this.nextSave(resp);
          }
        });
    }
  }

  private nextSave(resp: ResponseModel<TodoSaveResponseModel>): void {
    this.setResponse = resp;
    this.refreshTable();
    this.hideDialogSave();
    this.form.reset();
  }

  idRemove?:string;
  openRemoveMessage(id: string): void {
    this.idRemove = id;
    this.messageService
      .showConfirm('message_remove', 'warn', 'Tem certeza que deseja excluir este ToDo?', 'Clique em "sim" para continuar');
  }

  handleRemove(): void {
    if (this.idRemove)
      this.todoService.remove(this.idRemove)
          .subscribe({
            error: (resp: HttpErrorResponse) => {
              this.setResponse = resp.error;
              this.messageService.showErrorMessage("Ops! Houve inconsistências", resp);
            },
            next: () => {
              this.messageService.showSuccessMessage("Exclusão efetuada com sucesso!");
              this.refreshTable();
              this.closeRemove();
            }
          });
  }

  closeRemove(): void {
    this.idRemove = undefined;
    this.messageService.clear('message_remove');
  }
}
