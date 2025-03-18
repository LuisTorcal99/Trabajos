export interface UserModel {
   
    statusCode: number;
 
    isSuccess: boolean;
  
    errorMessages: Array<object>;

    result: Result;
}

export interface Result {
  
    token: string;
}

