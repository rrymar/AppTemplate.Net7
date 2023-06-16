import { CrudService } from 'app/core/crud/crud.service';
import { UserModel } from '../user.model';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class UserDetailsService extends CrudService<UserModel> {
  getBasePath = () => 'api/users';

  constructor(httpClient: HttpClient) {
    super(httpClient);
  }
}
