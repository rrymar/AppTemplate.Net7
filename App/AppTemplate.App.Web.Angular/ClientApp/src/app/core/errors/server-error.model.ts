import { HttpErrorResponse } from "@angular/common/http";

export interface ServerError {
  errors: string[];
  requestId: string;
}


export function getErrorMessages(error: Error): string[] {
  if (error instanceof HttpErrorResponse) {
    let serverError = error.error as ServerError;
    return serverError.errors;
  }

  return [error.message];
}
