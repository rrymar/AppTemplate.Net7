import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserManagementModule } from './user-management/user-management.module';


@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    UserManagementModule
  ],
  exports: [
  ]
})
export class UsersModule { }
