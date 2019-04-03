import { Photo } from './photo';

export interface User {
  id: number;
  username: string;
  knowAs: string;
  age: number;
  gender: string;
  created: Date;
  lastActive: Date;
  photoUrl: string;
  city: string;
  country: string;
  interests?: string;
  lookingFor?: string;
  photo?: Photo[];
}
