import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { HomeRoutingModule } from './home-routing.module';
import { HomeViewComponent } from './home-view/home-view.component';
import { CardModule } from 'primeng/card';


@NgModule({
  declarations: [
    HomeViewComponent
  ],
  imports: [
    CommonModule,
    CardModule,
    HomeRoutingModule
  ]
})
export class HomeModule { }
