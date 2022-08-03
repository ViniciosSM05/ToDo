import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { ResponseModel } from '../../models/response/response.model';

@Component({
  selector: 'app-error-response-field',
  templateUrl: './error-response-field.component.html',
  styleUrls: ['./error-response-field.component.scss']
})
export class ErrorResponseFieldComponent implements OnChanges {
  @Input() response!: ResponseModel<any>;
  @Input() field!: string;
  errors: string[] = [];

  constructor() { }

  ngOnChanges(changes: SimpleChanges): void {
    this.setErrors();
  }

  private setErrors(): void {
    if (this.response && this.response.fieldMessages && this.field) {
      const { fieldMessages }  = this.response;
      this.errors = fieldMessages.find(x => x.fieldName.toLowerCase() === this.field.toLowerCase())?.messages ?? [];
    }
  }
}
