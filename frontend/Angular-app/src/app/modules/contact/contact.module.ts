import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ContactRoutingModule } from './contact-routing.module';
import { ContactViewComponent } from './contact-view/contact-view.component';
import { CardModule } from 'primeng/card';


@NgModule({
  declarations: [
    ContactViewComponent
  ],
  imports: [
    CommonModule,
    CardModule,
    ContactRoutingModule
  ]
})
export class ContactModule { }
