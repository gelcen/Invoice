import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { environment } from 'src/environments/environment';
import { Invoice } from '../models/invoice.model';

@Injectable({
  providedIn: 'root'
})
export class InvoiceService {
  
  private readonly apiUrl: string = environment.baseApiUrl + "invoice";

  constructor(private http: HttpClient) { }

  getInvoices(): Observable<Invoice[]> {
    return this.http.get<Invoice[]>(this.apiUrl)
      .pipe(
        catchError(this.handleError<Invoice[]>("getInvoices"))
      );
  }

  private handleError<T> (operation = 'operation', result?:T) {
    return (error:any):Observable<T> => {
      
      console.log(operation + ' failed');
      console.log(error);

      return of(result as T);
    }
  }
}


