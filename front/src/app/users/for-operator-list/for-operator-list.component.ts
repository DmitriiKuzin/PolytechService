import { Component, OnInit } from '@angular/core';
import { ApiService } from 'src/app/api.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { User } from '../user';

@Component({
  selector: 'app-for-operator-list',
  templateUrl: './for-operator-list.component.html',
  styleUrls: ['./for-operator-list.component.css']
})
export class ForOperatorListComponent implements OnInit {

  i: number = 1;
  selectedUser: string
  clickedid: number;
  mobile:boolean
  itemIndex :number
  itemsHidden :boolean

  constructor(private apiService: ApiService,
    private toastr: ToastrService,
    private router: Router) { }


  ngOnInit() {
    this.i = 0;
    this.apiService.getRoles()
    this.apiService.getExecutors();
    if (window.screen.width < 800) { // 768px portrait
      this.mobile = true;
    }
  }

  openrow(users: User) {
    if (this.clickedid == this.i) { this.clickedid = -1 } else {
      this.clickedid = this.i
    }
  }

  onClick(user: User) {
    this.selectedUser = user.userName;
    this.apiService.userFormData = user;
    this.router.navigateByUrl('operator-panel/edit')
  }

  onDelete(id: string) {
    this.apiService.deleteUser(id)
      .subscribe(res => {
        this.toastr.warning('Пользователь удален');
        this.apiService.getUsers();
      },
        err => {
          console.log(err);
        })
  }
 async filterByRole(){
    await this.apiService.users.sort((a:User,b:User)=>{
      var nameA=a.role.name.toLowerCase(), nameB=b.role.name.toLowerCase()
  if (nameA < nameB) //сортируем строки по возрастанию
    return -1
  if (nameA > nameB)
    return 1
  return 0 // Никакой сортировки
  })
  document.getElementById("hrole").innerHTML = "Роль ↓"
  }


  bum(id){
    this.itemIndex = id
    
  }
  
  test(id){
    console.log(id)
  }
}
