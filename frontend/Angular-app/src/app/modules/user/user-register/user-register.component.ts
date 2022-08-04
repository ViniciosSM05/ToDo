import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { BaseForm } from 'src/app/core/classes/BaseForm';
import { UserService } from 'src/app/core/services/http/user/user.service';
import { MyMessageService } from 'src/app/core/services/message/my-message.service';
import { ResponseModel } from 'src/app/shared/models/response/response.model';
import { UserSaveResponseModel } from 'src/app/shared/models/user/user-save-response.model';
import { UserSaveModel } from 'src/app/shared/models/user/user-save.model';

@Component({
  selector: 'app-user-register',
  templateUrl: './user-register.component.html',
  styleUrls: ['./user-register.component.scss']
})
export class UserRegisterComponent extends BaseForm<UserSaveResponseModel> implements OnInit {
    form = this.fb.group({
        name: ["", Validators.required], 
        email: ["", [Validators.required, Validators.email]],
        password: ["", Validators.required],
        passwordConfirm: ["", Validators.required]
    })

    constructor(
        private fb: FormBuilder, 
        private userService: UserService,
        private messageService: MyMessageService,
        private router: Router)
    { 
        super();
    }
        
    ngOnInit(): void {

    }

    handleRegister(): void {
        const { valid, value } = this.form;
        if (valid) {
            const obj: UserSaveModel = {
                email: value.email ?? "",
                name: value.name ?? "",
                password: value.password ?? "",
                passwordConfirm: value.passwordConfirm ?? ""
            };

            this.userService.register(obj).subscribe({
                error: (resp: HttpErrorResponse) => {
                    if (resp.error) this.setResponse = resp.error;
                    this.messageService.showErrorMessage("Ops! Houve inconsistências", resp);
                },
                next: resp => {
                    this.setResponse = resp;
                    this.messageService.showSuccessMessage("Cadastro efetuado!");
                    setTimeout(() =>  this.router.navigate(['login', resp.data.email]), 2500);
                },
            });
        }
    }
}
