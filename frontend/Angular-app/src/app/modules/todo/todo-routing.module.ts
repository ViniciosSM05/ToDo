import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoggedInGuard } from 'src/app/core/guards/logged-in.guard';
import { TodoViewComponent } from './todo-view/todo-view.component';

const routes: Routes = [ 
  { path: 'todo', component: TodoViewComponent, canActivate: [LoggedInGuard] }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TodoRoutingModule { }
