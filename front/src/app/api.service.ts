import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpRequest, HttpEventType } from '@angular/common/http';
import { User, Role, Category } from './users/user'
import { Validators } from '@angular/forms';
import { Request, Dorm, Priority, Status } from './requests/requsetModel'
import { Router } from '@angular/router';
import { resolve } from 'url';




//import { resolveSoa } from 'dns';


@Injectable({
  providedIn: 'root'
})
export class ApiService {
  public roles: Role[]


  public users: User[];
  public usersCopyForSearch = this.users
  userexample: User = {
    userName: "",
    fullName: "",
    email: "",
    password: "",
    role: new Role
  }
  currentUser: User
  userFormData = this.userexample

  categories: Category[] = [
    { id: 1, name: "Сантехника" },
    { id: 2, name: "Электрика" },
    { id: 3, name: "Мебель" },
  ]

  priorities: Priority[] = [
    { id: 1, name: "Высокий" },
    { id: 2, name: "Средний" },
    { id: 3, name: "Низкий" },
  ]

  statuses: Status[] = [
    { id: 1, name: "Открыта" },
    { id: 2, name: "Закрыта" },
    { id: 3, name: "Закрыта неуспешно" },
    { id: 4, name: "Пауза" },
  ]

  files: File[]

  executors: User[]


  public requests: Request[]
  //reqFormData: Request  
  reqExample: Request = {
    statusId: 1,
    creatorId: "",
    category: this.categories[1],
    description: "",
    executor: this.userexample,
    priority: this.priorities[2],
    priorityId: 2,
    room: null,
    title: "",
    dormId: 1,
    dorm: new Dorm,
    categoryId: 1
  }
  public requestsCopyForSearch: Request[]
  reqFormData = this.reqExample

  dorms: Dorm[]
  images: string[]

  request: Request = {
    statusId: 1,
    creatorId: "",
    category: this.categories[1],
    description: "",
    executor: this.userexample,
    priority: this.priorities[2],
    priorityId: 2,
    room: null,
    title: "",
    dormId: 1,
    dorm: new Dorm,
    categoryId: 1
  }
  public progress: number;

  apiURL: string = 'http://localhost:5000/api/'


  search: string
  dontGet: boolean
  hasUndistributed: boolean = false
  requestsLoaded: Promise<boolean>

  public mobile: boolean



  async getDorms() {

    await this.http.get(this.apiURL + 'Request/Dorms')
      .toPromise()
      .then(res => this.dorms = res as Dorm[]);
    return this.dorms

  }




  constructor(private http: HttpClient,
    private router: Router) {

  }

  async refreshCurrentUser() {
    await this.http.get(this.apiURL + 'UserProfile')
      .toPromise()
      .then(res => this.currentUser = res as User)
  }

  async resetUserFormData() {

    this.userFormData = await this.userexample
  }
  resetReqFormData() {
    this.reqFormData = this.reqExample
  }

  async getUsers() {
    await this.http.get(this.apiURL + 'User')
      .toPromise()
      .then(res => this.users = res as User[])
    this.usersCopyForSearch = this.users
    this.executors = await this.users.filter(user => user.role.name == "Executor")

  }

  postUser() {
    console.log(this.userFormData)
    return this.http.post(this.apiURL + 'User/Register', this.userFormData);
  }
  registerUser() {
    console.log(this.userFormData)
    return this.http.post(this.apiURL + 'Auth/Register', this.userFormData);
  }


  updateUser() {
    return this.http.put(this.apiURL + 'User', this.userFormData);
  }

  deleteUser(id: string) {
    return this.http.delete(this.apiURL + 'User/' + id);
  }


  login(userFormData) {
    return this.http.post(this.apiURL + 'Auth/Login', userFormData);
  }
  onLogout() {
    localStorage.removeItem('token');
    this.router.navigate(['users/login'])
  }

  roleMatch(allowedRoles): boolean {
    var isMatch = false;
    var payLoad = JSON.parse(window.atob(localStorage.getItem('token').split('.')[1]));
    var userRole = payLoad.role;
    allowedRoles.forEach(element => {
      if (userRole == element) {
        isMatch = true;
        return false;
      }
    });
    return isMatch;
  }
  async getRoles() {
    await this.http.get(this.apiURL + 'User/Roles')
      .toPromise()
      .then(res => this.roles = res as Role[])
    
  }

  async refreshDorms() {
    await this.http.get(this.apiURL + 'Request/Dorms')
      .toPromise()
      .then(res => this.dorms = res as Dorm[])
  }



  async getRequests() {

    this.http.get(this.apiURL + 'Request')
      .toPromise()
      .then(res => this.requests = res as Request[])
      .then(res => this.requestsCopyForSearch = res as Request[])
      .then(res => this.checkIfHasUndistributed())
      .then(res => {
        if (this.requests.length > 0)
          this.requestsLoaded = Promise.resolve(true)
        else
          this.requestsLoaded = Promise.resolve(false)
      })

  }

  checkIfHasUndistributed() {
    if (this.requests.filter(r => r.executor == null).length > 0)
      new Promise(resolve => this.hasUndistributed = true)
    else
      new Promise(resolve => this.hasUndistributed = false)
  }



  getRequestsForUser() {
    this.http.get(this.apiURL + 'Request/User')
      .toPromise()
      .then(res => this.requests = res as Request[])
      .then(res => this.requestsCopyForSearch = res as Request[])
      .then(res => {
        if (this.requests.length > 0)
          this.requestsLoaded = Promise.resolve(true)
        else
          this.requestsLoaded = Promise.resolve(false)
      })
  }

  deleteRequest(id: number) {
    return this.http.delete(this.apiURL + 'Request/' + id)
  }

  postRequest() {
    return this.http.post(this.apiURL + 'Request', this.reqFormData)
  }

  refreshCategories() {

  }


  uploadFiles(files, id) {
    if (files.length === 0)
      return;

    const formData = new FormData();

    for (let file of files)

      formData.append(file.name, file)
    formData.append(id, id)

    const uploadReq = new HttpRequest('POST', this.apiURL + `Request/UploadFiles`, formData, {
      reportProgress: true,
    });

    this.http.request(uploadReq).subscribe(event => {
      if (event.type === HttpEventType.UploadProgress)
        this.progress = Math.round(100 * event.loaded / event.total)
      if (this.progress == 100) { this.getImages(id) }
      else if (event.type === HttpEventType.Response)
        console.log(event.body.toString())
    });
  }

  putRequest() {
    return this.http.put(this.apiURL + 'Request', this.request)
  }

  async getReuqestInfo(id) {
    await this.http.get(this.apiURL + 'Request/getRequestInfo/' + id)
      .toPromise()
      .then((res: any) => this.request = res as Request)
    this.getImages(id)
  }

  setExecutor() {
    this.request.executor = new User()
  }

  resetImages() {
    this.images = null
  }
  async getImages(id) {
    await this.http.get(this.apiURL + 'Request/getRequestFiles/' + id)
      .toPromise()
      .then((res: any) => this.images = res as string[])
  }

  async deleteImage(imgUrl: string) {
    var imgUrlObject = { url: imgUrl }
    await this.http.post(this.apiURL + 'Request/deleteFile', imgUrlObject)
      .toPromise()
      .then()

    this.images.splice(this.images.indexOf(imgUrl), 1)

  }

  async getExecutors() {
    await this.getUsers()
    this.users = await this.users.filter(u => u.role.name == "Executor")

  }

  searchRequests() {
    if (this.router.url.includes('requests')) {
      if (this.search.length <1) {
        if ( String(this.currentUser.role)=="Admin"){
          this.getRequests(); console.log(String(this.currentUser.role))}
         else this.getRequestsForUser()
      }

      this.requests = this.requestsCopyForSearch.filter(r =>
        r.title.includes(this.search)
        || r.description.includes(this.search))
    } else {
      if (this.search == "") {       
        this.getRequests()         
      }

      this.users = this.usersCopyForSearch.filter(r => r.userName.includes(this.search) || r.fullName.includes(this.search))
    }
  }

  async showExecutorRequests(executorName: string) {

    await this.setRequestsLoaded()

    this.router.navigate(['operator-panel/requests'])


      this.requestsLoaded = Promise.resolve(false)
      await this.sleep(100)
      if (this.requests.length > 0) {
        this.requests = this.requestsCopyForSearch.filter(r => { if (r.executor != null) return r.executor.userName == executorName })
      }  
   
  }

  setRequestsLoaded() {
    this.requestsLoaded = Promise.resolve(false)
  }

  sleep(ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
  }

}
