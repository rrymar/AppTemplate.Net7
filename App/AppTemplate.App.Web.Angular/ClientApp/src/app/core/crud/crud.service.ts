import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export abstract class CrudService<T> {
  constructor(protected httpClient: HttpClient) { }

  abstract getBasePath(): string;

  protected getUrl(path: string | number = ''): string {
    const url = this.getBasePath();

    return path === '' ? url : `${url}/${path}`;
  }

  get(id: string | number = ''): Observable<T> {
    return this.httpClient.get<T>(this.getUrl(id));
  }

  getList(): Observable<Array<T>> {
    return this.httpClient.get<Array<T>>(this.getUrl());
  }

  create(entity: T): Observable<T> {
    return this.httpClient.post<T>(this.getUrl(), entity);
  }

  update(id: string | number, entity: T): Observable<T> {
    return this.httpClient.put<T>(this.getUrl(id), entity);
  }

  delete(id: string | number): Observable<object> {
    return this.httpClient.delete(this.getUrl(id));
  }
}
