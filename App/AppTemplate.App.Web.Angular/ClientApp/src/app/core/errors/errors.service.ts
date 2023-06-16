import { Injectable } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { MatSnackBar, MatSnackBarConfig } from '@angular/material/snack-bar';
import { ServerError } from './server-error.model';
import { UiError } from './ui-error';

const ERROR_CONFIG: MatSnackBarConfig = {
  duration: 3000,
  horizontalPosition: 'end',
  verticalPosition: 'top',
  panelClass: 'snackbar-error'
}

const WARNING_CONFIG: MatSnackBarConfig = {
  ...ERROR_CONFIG,
  panelClass: 'snackbar-warning'
}

@Injectable({ providedIn: 'root' })
export class ErrorsService {
  constructor(private snackBar: MatSnackBar) { }

  handleError(error: Error) {
    if (error instanceof HttpErrorResponse) {
      this.handleHttpError(error);
    }
    else if (error instanceof UiError) {
      this.snackBar.open(error.message, 'X', ERROR_CONFIG);
    }
    else {
      console.error(error);
    }
  }

  private handleHttpError(error: HttpErrorResponse) {
    if (error.status === 401) {
      //TODO: Redirect to login
    }

    if (error.status === 404) {
      this.snackBar.open('Not Found', 'X', ERROR_CONFIG);
      return;
    }

    const serverError = error && (error.error as ServerError);
    console.error(serverError);

    if (error.status === 403) {
      this.showForbiddenError(serverError);
      return;
    }

    if (error.status >= 500 || (error.status === 0 && !serverError.errors)) {
      this.showInternalServerError(error);
      return;
    }

    this.showCustomError(serverError, error);
    return;
  }

  private showInternalServerError(error: HttpErrorResponse) {
    const message = `Error Code: ${error.status}; Oops! Something went wrong.`;
    this.snackBar.open(message, 'X', ERROR_CONFIG);
  }

  private showForbiddenError(serverError: ServerError) {
    const message = serverError.errors.length == 0
      ? 'Access Forbidden'
      : 'Access Forbidden: ' + serverError.errors[0];
    this.snackBar.open(message, 'X', ERROR_CONFIG);
  }

  private showCustomError(serverError: ServerError, error: HttpErrorResponse) {
    for (let index in serverError.errors) {
      let message = serverError.errors[index];

      if (message === '')
        message = error.statusText;

      if (error.status == 422) {
        this.snackBar.open(message.slice(0, 200), 'X', WARNING_CONFIG);
      }
      else {
        this.snackBar.open(message.slice(0, 200), 'X', ERROR_CONFIG);
      }
    }
  }
}

