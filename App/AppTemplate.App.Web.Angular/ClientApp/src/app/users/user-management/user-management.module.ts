import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { UsersListComponent } from './users-list/users-list.component';
import { NgxsModule } from '@ngxs/store';
import { UsersListState } from './users-list/users-list.state';
import { Routes, RouterModule } from '@angular/router';
import { UserDetailsState } from './user-details/user-details.state';
import { UserDetailsComponent } from './user-details/user-details.component';
import { ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

export const routes: Routes =
  [{
    path: 'users',
    children: [
      {
        path: '',
        component: UsersListComponent,
      },
      {
        path: ':id',
        component: UserDetailsComponent,
        data: { reuse: false }
      },
      {
        path: 'new',
        component: UserDetailsComponent,
        data: { reuse: false }
      }
    ]
  }]

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    NgxsModule.forFeature([UsersListState, UserDetailsState]),
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
    MatIconModule,
    MatProgressSpinnerModule,
    ReactiveFormsModule
  ],
  declarations: [UsersListComponent, UserDetailsComponent]
})
export class UserManagementModule { }
