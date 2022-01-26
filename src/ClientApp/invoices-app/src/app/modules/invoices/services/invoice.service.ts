import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { environment } from 'src/environments/environment';
import { InvoiceEditViewModel } from '../models/invoice-edit.model';
import { InvoiceListElementViewModel } from '../models/invoice-list.model';
import { InvoiceTableViewModel } from '../models/invoice-table.model';
import { InvoiceTableRequest } from '../requests/invoice-table.request';

@Injectable({
  providedIn: 'root'
})
export class InvoiceService {
  
  private readonly apiUrl: string = environment.baseApiUrl + "invoice";

  constructor(private http: HttpClient) { }

  getInvoices(tableRequest: InvoiceTableRequest): Observable<InvoiceTableViewModel> {
    const params = new HttpParams()
      .set("filters", tableRequest.filters)
      .set("sorts", tableRequest.sorts)
      .set("page", tableRequest.page.toString())
      .set("pageSize", tableRequest.pageSize.toString());

    return this.http.get<InvoiceTableViewModel>(this.apiUrl, {params:params})
      .pipe(
        catchError(this.handleError<InvoiceTableViewModel>("getInvoices"))
      );
  }

  getInvoiceByNumber(number: number): Observable<InvoiceEditViewModel> {
    return this.http.get<InvoiceEditViewModel>(this.apiUrl + "/" + number)
      .pipe(
        catchError(this.handleError<InvoiceEditViewModel>("getInvoiceByNumber"))
      );
  }

  addInvoice(invoice: InvoiceEditViewModel): Observable<any> {
    return this.http.post<InvoiceEditViewModel>(this.apiUrl, invoice)
      .pipe(
        catchError(this.handleError<InvoiceEditViewModel>('addInvoice'))
      );
  }

  updateInvoice(invoice: InvoiceEditViewModel): Observable<any> {
    return this.http.put(this.apiUrl, invoice)
      .pipe(
        catchError(this.handleError<any>('updateInvoice'))
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


