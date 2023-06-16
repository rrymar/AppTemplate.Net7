import { DataSource } from '@angular/cdk/collections';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { Observable, merge, Subject } from 'rxjs';
import { UserModel } from '../user.model';
import { Store } from '@ngxs/store';
import { UsersListState } from './users-list.state';
import { LoadUsersAction } from './users-list.actions';
import { takeUntil } from 'rxjs/operators';

export class UsersListDataSource extends DataSource<UserModel> {
  paginator: MatPaginator | undefined;
  sort: MatSort | undefined;

  private disconnected = new Subject();

  constructor(private store: Store) {
    super();
  }

  connect(): Observable<UserModel[]> {
    this.sort!.sortChange
      .pipe(takeUntil(this.disconnected))
      .subscribe(() => this.paginator!.pageIndex = 0);

    merge(this.paginator!.page, this.sort!.sortChange)
      .pipe(takeUntil(this.disconnected))
      .subscribe(() => this.load());

    this.store.select(UsersListState.totalCount)
      .pipe(takeUntil(this.disconnected))
      .subscribe(t => this.paginator!.length = t);

    return this.store.select(UsersListState.items)
      .pipe(takeUntil(this.disconnected));
  }

  load(keyword: string = '') {
    this.store.dispatch(new LoadUsersAction({
      pageIndex: this.paginator!.pageIndex,
      pageSize: this.paginator!.pageSize,
      sortField: this.sort!.active,
      isDesc: this.sort!.direction === 'desc',
      keyword: keyword
    }));
  }

  disconnect() {
    this.disconnected.next();
    this.disconnected.complete();
  }
}
