import { User, Category } from '../users/user';

export class Request {
    id?:number;
    statusId:number;
    status?: Status;
    priorityId:number;
    priority:Priority;
    dormId:number;
    dorm?:Dorm;
    room:number;
    creatorId:string;
    creator?:User;
    title:string;
    description: string;
    executorId?:string;
    executor?:User;
    category:Category;
    categoryId?:number;
    lifecycleId?: number;
    lifecycle?:Lifecycle;
    files?:File[]
}
export class Dorm{
    id:number;
    address:string
}
export class Lifecycle{
    id:number;
    opened:Date;
    distributed: Date;
    closed:Date
}
export class Priority{
    id: number;
    name : string
}

export class Status{
    id:number;
    name:string
}
