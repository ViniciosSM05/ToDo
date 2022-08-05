import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ContactViewComponent } from './contact-view/contact-view.component';

const routes: Routes = [
  { path: 'contact', component: ContactViewComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ContactRoutingModule { }
