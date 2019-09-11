import { Component, OnInit } from '@angular/core';
import { ApiService } from 'src/app/api.service';
import { User } from 'src/app/users/user';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-adminreqedit',
  templateUrl: './adminreqedit.component.html',
  styleUrls: ['./adminreqedit.component.css']
})
export class AdminreqeditComponent implements OnInit {
executort:User
emptyExecutor:boolean
  constructor(private apiService:ApiService,
    private toastr:ToastrService,
    private router:Router) { }

  async ngOnInit() {
    this.emptyExecutor = false
    await this.apiService.resetReqFormData();
    await this.apiService.getUsers();
    await this.apiService.getDorms();
    await this.apiService.refreshCurrentUser();
  
    if (this.apiService.request.executor == null) this.emptyExecutor = true
  }
  
  onGo(){

    console.log(this.apiService.request)
this.emptyExecutor = true
  }

   gDate(date: string) {
     if(date==null){
      return "-"
     } else 
    return  date.substr(5, 5) + ' ' + date.substr(11, 5)
  }

  executorName(execut: User) {
    if (execut == null) {
      return "Не распределена"
    }
    else {
      return execut.userName
    }

  }

  onSubmit(form: NgForm) {
    if (form.valid) {          
      this.apiService.putRequest().subscribe(
        (res:any) => { 
          console.log(res)             
          if (res ==2) {
            this.router.navigateByUrl(String(this.apiService.currentUser.role).toLowerCase() + '-panel/requests')
            this.toastr.success("Заявка обновлена!")
          }

        },
        err => {
          console.log(err)
          if (err.status == 400) { this.toastr.error("Обновление не удалось :(") }
        })
    }
    this.apiService.getRequests()
  }


}
