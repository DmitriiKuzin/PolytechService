export class User {
    // id:number;

    fullName: string;
    email: string;
    userName?: string;
    role?: Role;
    id?: string;
    password?: string;
    dormId?:number;
    room?:number
}

export class Role {
    id?: number;
    name?: string;
    normalizedName?: string;
    rusName?:string
}

export class Category {
    id: number;
    name: string
}