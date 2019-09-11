import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NewuserComponent } from './users/newuser/newuser.component'
import { RouterModule, Routes } from '@angular/router';
import { UsersComponent } from './users/users.component';
import { LoginComponent } from './users/login/login.component';
import { RegistrationComponent } from './users/registration/registration.component'
import { ListComponent } from './users/list/list.component';
import { UserprofileComponent } from './users/userprofile/userprofile.component';
import { AuthGuard } from './app-guard/auth.guard';
import { AdminPanelComponent } from './for-admin/admin-panel.component';
import { RequestComponent } from './requests/request.component';
import { UserPanelComponent } from './for-user/user-panel.component';
import { EditComponent } from './users/edit/edit.component';
import { NewrequestComponent } from './requests/newrequest/newrequest.component';
import { AdminNewRequestComponent } from './requests/admin-new-request/admin-new-request.component';
import { AdminreqeditComponent } from './requests/admin-edit-request/adminreqedit.component';
import { OperatorPanelComponent } from './for-operator/operator-panel.component'
import { ListForUserComponent } from './requests/list-for-user/list-for-user.component';
import { ForOperatorListComponent } from './users/for-operator-list/for-operator-list.component';
import { ExecutorPanelComponent } from './for-executor/executor-panel.component';
import { ExecuterEditRequestComponent } from './requests/executer-edit-request/executer-edit-request.component';



const routes: Routes = [
  { path: '', redirectTo: '/users/login', pathMatch: 'full' },
  {
    path: 'users', component: UsersComponent,
    children: [
      { path: 'newuser', component: NewuserComponent },
      { path: 'login', component: LoginComponent },
      { path: 'registration', component: RegistrationComponent },
    ]
  },

  { path: 'users/userprofile', component: UserprofileComponent, canActivate: [AuthGuard] },


  {
    path: 'admin-panel', component: AdminPanelComponent,//, canActivate: [AuthGuard], data: { permittedRoles: ['Admin'] },
    children: [
      { path: 'list', component: ListComponent },
      { path: 'requests', component: RequestComponent },
      { path: 'newuser', component: NewuserComponent },
      { path: 'edit', component: EditComponent },
      { path: 'request/edit', component: AdminreqeditComponent },
      { path: 'newrequest', component: AdminNewRequestComponent },
    ]
  },
  {
    path: 'operator-panel', component: OperatorPanelComponent,//, canActivate: [AuthGuard], data: { permittedRoles: ['Admin'] },
    children: [
      { path: 'list', component: ForOperatorListComponent },
      { path: 'requests', component: RequestComponent },
      { path: 'newuser', component: NewuserComponent },
      { path: 'edit', component: EditComponent },
      { path: 'request/edit', component: AdminreqeditComponent },
      { path: 'newrequest', component: AdminNewRequestComponent },
    ]
  },
  {
    path: 'executor-panel', component: ExecutorPanelComponent, //canActivate: [AuthGuard], data: { permittedRoles: ['User'] } },
    children: [      
      { path: 'requests', component: ListForUserComponent },
      { path: 'request/edit', component: ExecuterEditRequestComponent }
    ]
  },
  {
    path: 'user-panel', component: UserPanelComponent, //canActivate: [AuthGuard], data: { permittedRoles: ['User'] } },
    children: [
      { path: 'newrequest', component: NewrequestComponent },
      { path: 'requests', component: ListForUserComponent }
    ]
  }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})


export class AppRoutingModule { }
