import { Component } from '@angular/core';
import { ApiService } from './api.service';


     
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  
})

export class AppComponent {

 

constructor(private apiService: ApiService) {}

 async ngOnInit(){
    
  if (window.screen.width < 800) { // 768px portrait
    this.apiService.mobile = true;
  }

  }
}

