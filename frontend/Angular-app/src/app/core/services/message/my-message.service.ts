import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MessageService } from 'primeng/api';
import { HTTPCODE } from 'src/app/shared/enums/HTTPCODE';

@Injectable({
  providedIn: 'root'
})
export class MyMessageService {
  constructor(private messageService: MessageService) { }

  showConfirm(key:string, type:string, summary:string, detail:string) {
    this.messageService.clear(key);
    this.messageService.add({ key, severity: type, summary, detail, sticky: true });
  }

  showWarningMessage(msg: string, keep: boolean = false): void {
    this.messageService.add({severity:'warn', summary:'ATENÇÃO', detail: msg, sticky: keep });
  }

  showSuccessMessage(msg: string, keep: boolean = false): void {
    this.messageService.add({severity:'success', summary: 'SUCESSO', detail: msg, sticky: keep });
  }

  showErrorMessage(msg: string, error: HttpErrorResponse): void {
    this.clear();
    switch (error.status){
      case HTTPCODE.NOT_RESPONSE: this._showErrorMessage(`SERVER ERROR - ${error.message}`, true); break;
      case HTTPCODE.INTERNAL_SERVER_ERROR: this._showErrorMessage(`SERVER ERROR - ${error.message}`, true); break;
      case HTTPCODE.BADREQUEST: this.showWarningMessage(msg); break;
      default: this.showWarningMessage(msg); break;
    }   
  }

  private _showErrorMessage(msg: string, keep: boolean = false): void {
    this.messageService.add({severity:'error', summary:'ERRO', detail: msg, sticky: keep });
  }

  clear(key?:string): void {
    this.messageService.clear(key);
  }
}
