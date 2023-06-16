import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTable } from '@angular/material/table';
import { UsersListDataSource } from './users-list-datasource';
import { UserModel } from '../user.model';
import { Store } from '@ngxs/store';
import { UsersListState } from './users-list.state';
import { Observable, timer } from 'rxjs';
import { debounce } from 'rxjs/operators';

@Component({
  selector: 'app-users-list',
  templateUrl: './users-list.component.html',
  styleUrls: ['./users-list.component.scss']
})
export class UsersListComponent implements AfterViewInit, OnInit {
  @ViewChild(MatPaginator) paginator: MatPaginator| undefined;
  @ViewChild(MatSort) sort: MatSort | undefined;
  @ViewChild(MatTable) table: MatTable<UserModel> | undefined;
  dataSource: UsersListDataSource | undefined;

  isLoading$: Observable<boolean>;

  displayedColumns = ['id', 'username', 'fullName', 'createdOn', 'email'];

  constructor(private store: Store) {
    this.isLoading$ = this.store.select(UsersListState.isLoading)
      .pipe(debounce(v=> timer(v? 300 : 0)));
  }

  ngOnInit() {
    this.dataSource = new UsersListDataSource(this.store);
  }

  ngAfterViewInit() {
    this.dataSource!.sort = this.sort;
    this.dataSource!.paginator = this.paginator;
    this.table!.dataSource = this.dataSource!;

    window.setTimeout(() => this.dataSource!.load());
  }
}
