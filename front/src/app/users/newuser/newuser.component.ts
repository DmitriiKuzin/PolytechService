import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ApiService } from 'src/app/api.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr'
import { Role } from '../user';



@Component({
  selector: 'app-newuser',
  templateUrl: './newuser.component.html',
  styleUrls: ['./newuser.component.css'],

})
export class NewuserComponent implements OnInit {

  constructor(private apiService: ApiService, private router: Router, private toastr: ToastrService) { }


  async ngOnInit() {
    await this.apiService.resetUserFormData()
    this.apiService.getRoles()   
    await this.apiService.refreshDorms()
    await this.apiService.refreshCurrentUser()
   // this.apiService.roles = this.apiService.roles.splice(1,2)
   
   this.apiService.userFormData.dormId = this.apiService.dorms[0].id
  }

  async onSubmit(form: NgForm) {

    if (form.valid) {
      if (this.apiService.userFormData.role=="Operator"||
      this.apiService.userFormData.role=="Admin") {
        this.apiService.userFormData.dormId=null
        this.apiService.userFormData.room=null
      
      }
      this.apiService.postUser().subscribe(
        (res: any) => {
          console.log(res)
          if (res.succeeded) {
            let roleName:string = String(this.apiService.currentUser.role)
            roleName=  roleName.toLowerCase()
            this.router.navigateByUrl(roleName+'-panel/list')
            this.toastr.success("Пользователь успешно создан!")
            this.apiService.resetUserFormData()
            form.reset()
          }

        },
        err => {
          console.log(err)
          if (err.status == 400) { this.toastr.error("Пользователь с таким логином уже существует!") }
        })
    }
   await this.apiService.resetUserFormData()
  }
  onInfa(){
    console.log(this.apiService.userFormData)
   }

   Go(){
     console.log(this.apiService.userFormData)
   }
}
