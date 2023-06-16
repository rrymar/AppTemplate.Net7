import { ErrorHandler, Injectable, Injector } from '@angular/core';
import { ErrorsService } from './errors.service';

@Injectable()
export class GlobalErrorsHandler implements ErrorHandler {
  constructor(private injector: Injector) { }

  handleError(error: Error) {
    const service = this.injector.get(ErrorsService);
    service.handleError(error);
  }
}
