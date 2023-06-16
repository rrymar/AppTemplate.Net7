export interface User {
  id: number;
  username: string;
  firstName: string;
  lastName: string;
  fullName: string;
  email: string;
  isSystemUser: boolean;
  createdOn: Date;
}
