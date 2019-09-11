import { Component, OnInit, Input } from '@angular/core';
import { ApiService } from 'src/app/api.service'
import { User, Role } from 'src/app/users/user';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { HttpClient, HttpRequest, HttpEventType, HttpResponse } from '@angular/common/http';
import { stripGeneratedFileSuffix } from '@angular/compiler/src/aot/util';


@Component({
  selector: 'app-admin-new-request',
  templateUrl: './admin-new-request.component.html',
  styleUrls: ['./admin-new-request.component.css']
})
export class AdminNewRequestComponent implements OnInit {
 
  constructor(private apiService: ApiService,
    private toastr: ToastrService,
    private http: HttpClient) { }

  selectedFile: File

  ngOnInit() {
    this.apiService.resetReqFormData();
    this.apiService.getUsers();
    this.apiService.getDorms();
    this.apiService.refreshCurrentUser();
    this.apiService.images = null
  }



 async setFiles(files) {
    this.apiService.files = await files
    if (this.apiService.files != null) {
      this.apiService.uploadFiles(this.apiService.files, this.apiService.currentUser.id)
    }
  }



  public progress: number = 1;
  public message: string;





  onGo(){
    console.log(this.apiService.images)
    
  }

  onImageDelete(imgUrl:string){
    this.apiService.deleteImage(imgUrl)
  }


  onSubmit(form: NgForm) {
    if (form.valid) {
      this.apiService.reqFormData.creatorId = this.apiService.currentUser.id
      this.apiService.postRequest().subscribe(
        (res: any) => {
          console.log(res)
          if (res != 0) {
            
            this.toastr.success("СОЗДАНА!")
            this.ngOnInit()
            form.reset()
            this.apiService.images = null
          }
        },
        err => {
          console.log(err)
        }
      )
    }
  }

  
}
