import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'src/app/core/authentication/auth.service';
import { HeaderService } from 'src/app/core/services/header/header.service';
import { MyMessageService } from 'src/app/core/services/message/my-message.service';
import { EnumButtonHeader } from 'src/app/shared/enums/EnumButtonHeader';
import { UserLoginResponseModel } from 'src/app/shared/models/auth/user-login-response.model';
import { UserLoginModel } from 'src/app/shared/models/auth/user-login.model';
import { ResponseModel } from 'src/app/shared/models/response/response.model';

@Component({
  selector: 'app-user-login',
  templateUrl: './user-login.component.html',
  styleUrls: ['./user-login.component.scss']
})
export class UserLoginComponent implements OnInit {
  form = this.fb.group({
    email: [""],
    password: [""],
  })

  constructor(
    private fb: FormBuilder, 
    private headerService: HeaderService,
    private authService: AuthService,
    private messageService: MyMessageService,
    private router: Router,
    private activatedRoute: ActivatedRoute
    ) { }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe(params => {
      if (params && params["email"]) this.form.controls.email.setValue(params['email']);
    });
  }

  handleLogin(): void {
    const { value } = this.form;

    const obj: UserLoginModel = {
        email: value.email ?? "",
        password: value.password ?? ""
    };

    this.authService.login(obj).subscribe({
      error: (resp: HttpErrorResponse) => {
        this.messageService.showErrorMessage("Credenciais inválidas", resp);
      },
      next: () => {
        this.headerService.buttonHeaderSubject.next(EnumButtonHeader.Quit);
        this.router.navigate(['todo']);      
      },
  });
  }
}
