import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Store } from '@ngxs/store';
import { SaveUserAction, LoadUserAction, ResetUserAction, DeleteUserAction } from './user-details.actions';
import { ActivatedRoute } from '@angular/router';
import { UserDetailsState } from './user-details.state';
import { untilDestroyed } from 'ngx-take-until-destroy';
import { Observable, timer } from 'rxjs';
import { debounce } from 'rxjs/operators';

@Component({
  selector: 'app-user-details',
  templateUrl: './user-details.component.html',
  styleUrls: ['./user-details.component.scss']
})
export class UserDetailsComponent implements OnInit, OnDestroy {
  constructor(private store: Store,
    private route: ActivatedRoute) {

    this.isLoading$ = this.store.select(UserDetailsState.isLoading)
      .pipe(debounce(v => timer(v ? 300 : 0)));
  }

  private id: number = 0;

  isLoading$: Observable<boolean>;

  userForm: FormGroup = new FormGroup({
    username: new FormControl('', Validators.required),
    firstName: new FormControl(''),
    lastName: new FormControl(''),
    email: new FormControl(''),
  });

  ngOnInit() {
    const idParam = this.route.snapshot.paramMap.get('id');
    this.id = Number(idParam) || 0;

    this.store.select(UserDetailsState.entity)
      .pipe(untilDestroyed(this))
      .subscribe(r => {
        this.userForm.patchValue(r ?? {});
      });

    if (this.id != 0) {
      this.store.dispatch(new LoadUserAction(this.id))
    }
  }

  onSubmit() {
    if (this.userForm.invalid)
      return;

    this.store.dispatch(new SaveUserAction({
      id: this.id,
      ...this.userForm.value
    }));
  }

  delete() {
    this.store.dispatch(new DeleteUserAction());
  }

  ngOnDestroy(): void {
    this.store.dispatch(new ResetUserAction());
  }
}
