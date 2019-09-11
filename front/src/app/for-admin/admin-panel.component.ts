import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ApiService } from '../api.service';
import { NewrequestComponent } from '../requests/newrequest/newrequest.component';

@Component({
  selector: 'app-admin-panel',
  templateUrl: './admin-panel.component.html',
  styleUrls: ['./admin-panel.component.css']
})
export class AdminPanelComponent implements OnInit {

  constructor(private router:Router, private apiService:ApiService) { }

  ngOnInit() {
  }
  Users(){
     this.router.navigateByUrl('users/list');

   // window.location.href='users/list';
  }

  onAddReq(){
    
    this.apiService.resetReqFormData()
    this.router.navigateByUrl('admin-panel/newrequest')
  }
}
