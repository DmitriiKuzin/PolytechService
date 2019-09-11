import { Component, OnInit } from '@angular/core';
import { ApiService } from '../api.service';
import { ThrowStmt } from '@angular/compiler';
import { ToastrService } from 'ngx-toastr';
import { User, Role } from '../users/user';
import { Router } from '@angular/router';
import { routerNgProbeToken } from '@angular/router/src/router_module';
import { async } from '@angular/core/testing';
import { typeWithParameters } from '@angular/compiler/src/render3/util';

@Component({
  selector: 'app-request',
  templateUrl: './request.component.html',
  styleUrls: ['./request.component.css']
})
export class RequestComponent implements OnInit {

  constructor(private apiService: ApiService,
    private toastr: ToastrService,
    private router: Router) { }
user:User = {userName : "Ivanovich", email:"",fullName:"", role:new Role}
mobile:boolean
undistributed :boolean
opened :boolean
hasRequests: boolean

 async ngOnInit() {
   
    this.apiService.refreshCurrentUser()
   await this.apiService.getRequests()
    if (window.screen.width < 800) { // 768px portrait
      this.mobile = true;
    
    }  
    
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
          this.apiService.getRequests()
        },
        err => {
          return err
        }
      )
  }

async  undistriber(){
if (this.undistributed){
this.undistributed = false
     this.apiService.requests = this.apiService.requestsCopyForSearch  
}

else{
          var reqs =
  await  this.apiService.requestsCopyForSearch.filter(r=>
      r.executorId == null)
       this.apiService.requests = reqs
      this.undistributed = true
  }
}
async  opener(){
if (this.opened){
this.opened = false

     this.apiService.requests = this.apiService.requestsCopyForSearch
    
}
else{
          var reqs =
  await  this.apiService.requestsCopyForSearch.filter(r=>
      r.status.id == 1)
       this.apiService.requests = reqs
      this.opened = true
  }
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
