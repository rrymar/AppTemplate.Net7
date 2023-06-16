import { UserModel } from '../user.model';

export class LoadUserAction {
  static readonly type = '[UserDetails] Load';
  constructor(public id: number) {
  }
}

export class SaveUserAction {
  static readonly type = '[UserDetails] Save';
  constructor(public user: UserModel) {

  }
}

export class DeleteUserAction {
  static readonly type = '[UserDetails] Delete';
}

export class ResetUserAction {
  static readonly type = '[UserDetails] Reset';
}
