import { Component, OnInit } from '@angular/core';
import { ApiService } from '../api.service';


@Component({
  selector: 'app-executor-panel',
  templateUrl: './executor-panel.component.html',
  styleUrls: ['./executor-panel.component.css']
})
export class ExecutorPanelComponent implements OnInit {

  constructor(private apiService:ApiService) { }

  ngOnInit() {
  }

}
