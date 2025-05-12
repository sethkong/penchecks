import { HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { throwError, Observable } from 'rxjs';
import { Output, EventEmitter, Injectable } from '@angular/core';
import { ErrorMessage } from '../errors/error-message.model';
import { ApiResponse } from './http-response.model';

@Injectable({
  providedIn: 'root'
})
export class HttpService {
  private _apiUrl: string = '';

  get apiUrl(): string {
    return this._apiUrl;
  }
 
  set apiUrl(value: string) {
    this._apiUrl = value;
  }

  @Output() hasErrors = new EventEmitter<ErrorMessage>();

  constructor() {
    this.apiUrl = window.localStorage.getItem('apiUrl') || this.apiUrl;
  }

  protected getCommonHttpHeaders(): HttpHeaders {
    return new HttpHeaders({})
      .append('Content-Type', 'application/json')
      .append('Accept', 'application/json');
  }

  getEndpoint(resource: string): string {
    return `https://${this._apiUrl.trim()}/api/${resource.trim()}`;
  }

  protected handleError(error: HttpErrorResponse): Observable<any> {
    if (error.error instanceof ErrorEvent) {
      console.error(`An error occurred: ${error.error.message}`);
    } else {
      console.error(`Backend returned code: ${error.status}, body was: ${error.error}`);
    }
    return throwError(() => new Error('Something bad happened. Please try again later.'));
  }

  handleResponse<T>(response: ApiResponse<T>): T {
    if (response.errors && response?.errors?.message?.length !== 0) {
      this.hasErrors.emit(new ErrorMessage(
        response.errors.message,
        response.errors.code,
        response.errors.status
      ));
    }
    if (response.hasOwnProperty('error')) {
      this.hasErrors.emit(new ErrorMessage(response.errors?.toString()));
    }
    return response.data != null && response.data != undefined ? <T>response.data : <T>response;
  }

  handleResponseError(error: any): void {
    console.error(`HTTP response error: ${JSON.stringify(error)}`);
    this.hasErrors.emit(new ErrorMessage(JSON.stringify(error)));
  }
}
