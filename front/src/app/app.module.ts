import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from "@angular/forms";
import { AppComponent } from './app.component';
import{HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';
import { NavComponent } from './nav/nav.component';
import { UsersComponent } from './users/users.component';
import { NewuserComponent } from './users/newuser/newuser.component';
import { AppRoutingModule } from './app-routing.module';
import { LoginComponent } from './users/login/login.component';
import { RegistrationComponent } from './users/registration/registration.component';
import { ListComponent } from './users/list/list.component';
import { UserprofileComponent } from './users/userprofile/userprofile.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
 
// import { ToastrModule } from 'ngx-toastr';
import {ToastrModule} from 'node_modules/ngx-toastr';
import { ApiService } from './api.service';
import { AuthInterceptor } from './app-guard/auth.interceptor';
import { AdminPanelComponent } from './for-admin/admin-panel.component';
import { RequestComponent } from './requests/request.component';
import { UserPanelComponent } from './for-user/user-panel.component';
import { EditComponent } from './users/edit/edit.component';
import { NewrequestComponent } from './requests/newrequest/newrequest.component';
import { AdminNewRequestComponent } from './requests/admin-new-request/admin-new-request.component';
import { AdminreqeditComponent } from './requests/admin-edit-request/adminreqedit.component';
import { OperatorPanelComponent } from './for-operator/operator-panel.component';
import { ExecutorPanelComponent } from './for-executor/executor-panel.component';
import { ListForUserComponent } from './requests/list-for-user/list-for-user.component';
import { ForOperatorListComponent } from './users/for-operator-list/for-operator-list.component';
import { ExecuterEditRequestComponent } from './requests/executer-edit-request/executer-edit-request.component';


@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    UsersComponent,
    NewuserComponent,
    LoginComponent,
    RegistrationComponent,
    ListComponent,
    UserprofileComponent,
    AdminPanelComponent,
    RequestComponent,
    UserPanelComponent,
    EditComponent,
    NewrequestComponent,
    AdminNewRequestComponent,
    AdminreqeditComponent,
    OperatorPanelComponent,
    ExecutorPanelComponent,
    ListForUserComponent,
    ForOperatorListComponent,
    ExecuterEditRequestComponent,
    
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    FormsModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot()
  ],
  providers: [ApiService,{
    provide: HTTP_INTERCEPTORS,
    useClass: AuthInterceptor,
    multi: true
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
