import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MenuItem } from 'primeng/api';
import { AuthService } from 'src/app/core/authentication/auth.service';
import { HeaderService } from 'src/app/core/services/header/header.service';
import { EnumButtonHeader } from 'src/app/shared/enums/EnumButtonHeader';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
    enumButtonHeader = EnumButtonHeader;
    buttonLogin: EnumButtonHeader = EnumButtonHeader.Enter;
    constructor(private headerService: HeaderService, private authService: AuthService, private router: Router) {
        this.headerService.buttonHeaderSubject.subscribe(value => this.buttonLogin = value);
    }
    
    items!: MenuItem[];
    ngOnInit(): void {
        this.buttonLogin = this.authService.isLoggedIn ? EnumButtonHeader.Quit : EnumButtonHeader.Enter;
        this.items = [
            {
                label:'Contato',
                icon:'pi pi-fw pi-user',
                routerLink: '/contact'
            },    
            {
                label:"ToDo's",
                icon:'pi pi-fw pi-book',
                routerLink: '/todo'
            },         
        ];
    }

    handleDisconnect(): void {
        this.authService.logout();
        this.headerService.buttonHeaderSubject.next(EnumButtonHeader.Enter);
        this.router.navigate(['login', '']);
    }
}
