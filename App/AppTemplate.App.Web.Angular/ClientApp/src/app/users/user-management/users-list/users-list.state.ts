import { State, Action, Selector, StateContext } from '@ngxs/store';
import { UserModel } from '../user.model';
import { UsersListService } from './users-list.service';
import { tap } from 'rxjs/operators';
import { LoadUsersAction } from './users-list.actions';
import { Injectable } from '@angular/core';
import { ItemsLoadingState } from  'app/core/loading.state';

export interface UsersList extends ItemsLoadingState<UserModel> {
  totalCount: number;
}

const INITIAL_STATE = {
  items: [],
  isLoading: false,
  totalCount: 0
}

@State<UsersList>({
  name: 'usersList',
  defaults: INITIAL_STATE
})
@Injectable()
export class UsersListState {
  constructor(private service: UsersListService) {
  }

  @Selector()
  static items(state: UsersList): UserModel[] {
    return state.items;
  }

  @Selector()
  static totalCount(state: UsersList): number {
    return state.totalCount;
  }

  @Selector()
  static isLoading(state: UsersList): boolean {
    return state.isLoading;
  }

  @Action(LoadUsersAction)
  loadUsers(ctx: StateContext<UsersList>, action: LoadUsersAction) {

    ctx.patchState({
      isLoading: true,
      items: []
    });

    return this.service.search(action.query)
      .pipe(tap(r => {
        ctx.setState({
          isLoading: false,
          items: r.items,
          totalCount: r.totalCount
        })
      }, () => ctx.setState(INITIAL_STATE)));
  }

}
