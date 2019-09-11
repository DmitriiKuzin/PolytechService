import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ApiService } from 'src/app/api.service';
import { Router } from '@angular/router';
// import { ToastrService } from 'ngx-toastr';
import { ToastrService } from 'node_modules/ngx-toastr'
import { UsersComponent } from '../users.component'


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  formModel = {
    UserName: null,
    Password: null
  }
  constructor(private welcome: UsersComponent,
    private apiService: ApiService,
    private router: Router,
    private toastr: ToastrService) { }

  ngOnInit() {
    
    if (localStorage.getItem('token') != null) {
      var payLoad = JSON.parse(window.atob(localStorage.getItem('token').split('.')[1]));
      let userRole: String = payLoad.role;
      this.router.navigateByUrl(userRole.toLowerCase() + '-panel');
    }
  }

  onSubmit(form: NgForm) {
    this.apiService.login(form.value).subscribe(
      (res: any) => {
        localStorage.setItem('token', res.token);
        this.apiService.refreshCurrentUser();
        var payLoad = JSON.parse(window.atob(localStorage.getItem('token').split('.')[1]));
        let userRole: String = payLoad.role;
        this.router.navigateByUrl(userRole.toLowerCase() + '-panel/requests')
      },
      err => {
        if (err.status == 400)
          this.toastr.error('Неправильное имя пользователя или пароль');
        else
          console.log(err);
      }
    );
  }

}
