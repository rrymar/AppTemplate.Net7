import { Injectable } from '@angular/core';
import { UserModel } from '../user.model';
import { State, Action, StateContext, Selector } from '@ngxs/store';
import { UserDetailsService } from './users-details.service';
import { LoadUserAction, SaveUserAction, DeleteUserAction, ResetUserAction } from './user-details.actions';
import { EntityLoadingState } from 'app/core/loading.state';
import { tap, mergeMap } from 'rxjs/operators';
import { Navigate } from '@ngxs/router-plugin';

@State<EntityLoadingState<UserModel>>({
  name: 'userDetails'
})
@Injectable()
export class UserDetailsState {
  constructor(private service: UserDetailsService) {
  }

  @Selector()
  static entity(state: EntityLoadingState<UserModel>): UserModel | null {
    return state.entity;
  }

  @Selector()
  static isLoading(state: EntityLoadingState<UserModel>): boolean {
    return state.isLoading;
  }

  @Action(LoadUserAction)
  loadUser(ctx: StateContext<EntityLoadingState<UserModel>>, action: LoadUserAction) {
    ctx.setState({ isLoading: true, entity: null });

    return this.service.get(action.id)
      .pipe(tap(r => this.updateEntity(r, ctx), () => this.onError(ctx)));
  }

  @Action(SaveUserAction)
  saveUser(ctx: StateContext<EntityLoadingState<UserModel>>, action: SaveUserAction) {
    var user = action.user;
    ctx.patchState({ isLoading: true });

    var request = user.id
      ? this.service.update(user.id, user)
      : this.service.create(user);

    return request.pipe(
      tap(r => this.updateEntity(r, ctx), () => this.onError(ctx)),
      mergeMap(() => this.navigateToList(ctx))
    );
  }

  @Action(DeleteUserAction)
  deleteUser(ctx: StateContext<EntityLoadingState<UserModel>>, action: DeleteUserAction) {
    var user = ctx.getState().entity!;

    ctx.patchState({ isLoading: true });

    return this.service.delete(user.id).pipe(
      tap(() => this.updateEntity(null, ctx), () => this.onError(ctx)),
      mergeMap(() => this.navigateToList(ctx))
    );
  }

  @Action(ResetUserAction)
  resetCurrentUser(ctx: StateContext<EntityLoadingState<UserModel>>, action: ResetUserAction) {
    ctx.setState({ isLoading: false, entity: null });
  }

  private navigateToList(ctx: StateContext<EntityLoadingState<UserModel>>) {
    return ctx.dispatch(new Navigate(['users']));
  }

  private onError(ctx: StateContext<EntityLoadingState<UserModel>>) {
    ctx.patchState({ isLoading: false });
  }

  private updateEntity(entity: UserModel | null , ctx: StateContext<EntityLoadingState<UserModel>>) {
    ctx.patchState({
      entity: entity,
      isLoading: false
    });
  }
}
