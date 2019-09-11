import { Component, OnInit } from '@angular/core';
import { ApiService } from '../api.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-operator-panel',
  templateUrl: './operator-panel.component.html',
  styleUrls: ['./operator-panel.component.css']
})
export class OperatorPanelComponent implements OnInit {

  constructor(private apiService:ApiService) { }

mobile:boolean
  ngOnInit() {

    
  }


}
