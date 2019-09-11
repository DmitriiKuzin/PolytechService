import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ApiService } from 'src/app/api.service';
import { NgForm } from '@angular/forms';
import { User } from '../user';


@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})
export class EditComponent implements OnInit {

  constructor(private apiService: ApiService, private router: Router, private toastr: ToastrService) { }
  user: User;
  async ngOnInit() {
    await this.apiService.getRoles()
    await this.apiService.refreshDorms()
    console.log(this.apiService.userFormData)
  }

  onSubmit(form: NgForm) {
    if (form.valid) {
      
      this.apiService.updateUser().subscribe(
        (res: any) => {

          if (res.succeeded) {
            this.toastr.success("Пользователь обновлен!")
            this.router.navigateByUrl('admin-panel/list')
          }

        },
        err => {

          this.toastr.error("Ошибка!")
          return err
        })
    }
  }

  op(){
    console.log(this.apiService.userFormData)
  }

}
