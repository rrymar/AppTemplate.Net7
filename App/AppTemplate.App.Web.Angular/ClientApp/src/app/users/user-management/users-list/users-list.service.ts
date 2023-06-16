import { SearchQuery } from 'app/core/search/search-query';
import { UserModel } from '../user.model';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ResultsList } from 'app/core/search/results-list';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class UsersListService {

  constructor(private httpClient: HttpClient) {
  }

  search(query: SearchQuery): Observable<ResultsList<UserModel>> {
    return this.httpClient.post<ResultsList<UserModel>>('api/Users/Search', query);
  }

}
