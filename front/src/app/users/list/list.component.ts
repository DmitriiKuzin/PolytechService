import { Component, OnInit } from '@angular/core';
import { ApiService } from 'src/app/api.service'
import { User } from '../user';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';


@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit {

  i: number = 1;
  selectedUser: string
  clickedid: number;
  mobile: boolean

  constructor(private apiService: ApiService,
    private toastr: ToastrService,
    private router: Router) { }



  ngOnInit() {
    this.i = 0;
    this.apiService.getUsers()
    if (window.screen.width < 800) { // 768px portrait
      this.mobile = true;
    }
  }

    openrow(users: User) {
      if (this.clickedid == this.i) { this.clickedid = -1 } else {
        this.clickedid = this.i
      }
    }

    onClick(user: User) {
      this.selectedUser = user.userName;
      this.apiService.userFormData = user;
      this.router.navigateByUrl('admin-panel/edit')
    }

    onDelete(id: string) {
      this.apiService.deleteUser(id)
        .subscribe(res => {
          this.toastr.warning('Пользователь удален');
          this.apiService.getUsers();
        },
          err => {
            console.log(err);
          })
    }

  }

