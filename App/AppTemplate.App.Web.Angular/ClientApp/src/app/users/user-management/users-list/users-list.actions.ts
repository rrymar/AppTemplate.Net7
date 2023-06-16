import { SearchQuery } from 'app/core/search/search-query';

export class LoadUsersAction {
  static readonly type = '[UsersList] Load'
  constructor(public query: SearchQuery) {
  }
}
