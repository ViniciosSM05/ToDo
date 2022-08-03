import { Component, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { AuthService } from 'src/app/core/authentication/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
    items!: MenuItem[];
    userLoggedIn: boolean = false;
    constructor(authService: AuthService) {
        this.userLoggedIn = authService.isLoggedIn;
    }

    ngOnInit(): void {
        this.items = [
            {
                label:'Contato',
                icon:'pi pi-fw pi-user',
            },    
            {
                label:'Sobre',
                icon:'pi pi-fw pi-info',
            },          
        ];
    }
}
