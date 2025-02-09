export interface User {    
    fullName: string;
    userName: string;
    email: string;
    mobileNumber: string;
    PasswordHash: string;    
  }

  export interface UserLoginRequest {        
    userName: string;    
    password: string;    
  }
  