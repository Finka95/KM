import { Gender } from "../enums/gender";
import Photo from "./photo";
import { Subscription } from "./subscription";

export interface User {
  id: string;
  fusionUserId : string;
  firstName : string;
  lastName : string;
  age : number;
  city : string;
  description : string;
  gender: Gender;
  email: string;
  photos: Photo[]
};

export interface UserWithSubscription extends User {
  subscription: Subscription
};