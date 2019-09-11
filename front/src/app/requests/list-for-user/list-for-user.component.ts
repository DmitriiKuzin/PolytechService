import { Component, OnInit } from '@angular/core';
import { ApiService } from 'src/app/api.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { User, Role } from 'src/app/users/user';

@Component({
  selector: 'app-list-for-user',
  templateUrl: './list-for-user.component.html',
  styleUrls: ['./list-for-user.component.css']
})
export class ListForUserComponent implements OnInit {

  constructor(private apiService: ApiService,
    private toastr: ToastrService,
    private router: Router) { }
user:User = {userName : "Ivanovich", email:"",fullName:"", role:new Role}
 async ngOnInit() {
   await this.apiService.getRequestsForUser()
   this.apiService.refreshCurrentUser()
  }

  gDate(date: string) {
    return date.substr(5, 5) + ' ' + date.substr(11, 5)
  }

  priority(pr: number) {
    if (pr == 1)
      return "high";
    if (pr == 2)
      return "middle";
    if (pr == 3)
      return "low";
  }
  executorName(execut: User) {
    if (execut == null) {
      return "Не распределена"
    }
    else {
      return execut.userName
    }
  }

  onDelete(id: number) {
    this.apiService.deleteRequest(id)
      .subscribe(
        (res: any) => {
          this.toastr.success("Успешно удалено!")
          this.apiService.getRequestsForUser()
        },
        err => {
          return err
        }
      )
  }

 async onRow(id: number) {
  console.log(id)
  this.apiService.resetImages()
 await this.apiService.getReuqestInfo(id)    
await this.apiService.setExecutor() 
let role = String(this.apiService.currentUser.role).toLowerCase()
  this.router.navigateByUrl(role + '-panel/request/edit');    
  window.scroll(0,0)
  }
}
