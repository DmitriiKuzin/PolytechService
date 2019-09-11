import { Component, OnInit } from '@angular/core';
import { ApiService } from 'src/app/api.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-newrequest',
  templateUrl: './newrequest.component.html',
  styleUrls: ['./newrequest.component.css']
})
export class NewrequestComponent implements OnInit {

  constructor(private apiService: ApiService,
    private router: Router,
    private toastr: ToastrService) { }
  enableList:boolean = false
  step: number =1;

  async ngOnInit() {      
    await this.apiService.refreshCurrentUser()
    await this.apiService.resetReqFormData()
  }

  async setFiles(files) {
    this.apiService.files = await files
    if (this.apiService.files != null) {
      this.apiService.uploadFiles(this.apiService.files, this.apiService.currentUser.id)
    }    
  }
       

     async onSubmit(form: NgForm) {
      this.apiService.reqFormData.creatorId = await this.apiService.currentUser.id
      this.apiService.reqFormData.dormId = await this.apiService.currentUser.dormId
      this.apiService.reqFormData.room = await this.apiService.currentUser.room
      console.log(this.apiService.reqFormData) 
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
                this.router.navigateByUrl('user-panel/requests')
              }
            },
            err => {
              console.log(err)
            }
          )
        }
      }

  async onClick(event: any){
    let id = event.target.id 
    this.apiService.reqFormData.category = this.apiService.categories[id]   
    this.step=2
    console.log(id)
  }

  
  onChangeStep(){
    this.step = 0;

  }
  goBack(){
    this.step--
  }
  onEnable(){
    this.enableList=true;
  }
GO(){
  console.log(this.apiService.reqFormData)
}
}
