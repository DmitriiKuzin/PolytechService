import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ApiService } from 'src/app/api.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {

  constructor(private apiService: ApiService,
    private toastr: ToastrService,
    private router: Router) { }

 async ngOnInit() {
    this.apiService.refreshDorms()
  await  this.apiService.resetUserFormData()
  }
 async onSubmit(form: NgForm) {
    if (form.valid) {
     await this.apiService.getRoles()
      this.apiService.userFormData.role = this.apiService.roles.find(r=>r.name=="User")
      this.apiService.registerUser().subscribe(
        (res: any) => {
          if (res.succeeded) {
            this.toastr.success("Для завершения регистрации проверьте email")
            form.reset()
            this.apiService.resetUserFormData()   
            this.router.navigateByUrl('/users/login') 
                    
          };
        },
        err => {

          console.log(err);
        }

      );

    }
  }


}
