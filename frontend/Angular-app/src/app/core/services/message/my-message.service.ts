import { Injectable } from '@angular/core';
import { MessageService } from 'primeng/api';

@Injectable({
  providedIn: 'root'
})
export class MyMessageService {
  constructor(private messageService: MessageService) { }

  showWarningMessage(msg: string, keep: boolean = false): void {
    this.messageService.add({severity:'warn', summary:'ATENÇÃO', detail: msg, sticky: keep });
  }

  showErrorMessage(msg: string, keep: boolean = false): void {
    this.messageService.add({severity:'error', summary:'ERRO', detail: msg, sticky: keep });
  }

  showSuccessMessage(msg: string, keep: boolean = false): void {
    this.messageService.add({severity:'success', summary: 'SUCESSO', detail: msg, sticky: keep });
  }

  clear(): void {
    this.messageService.clear();
  }
}
