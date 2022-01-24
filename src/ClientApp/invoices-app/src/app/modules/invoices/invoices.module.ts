import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InvoiceListComponent } from './components/invoice-list/invoice-list.component';
import { InvoiceEditComponent } from './components/invoice-edit/invoice-edit.component';
import { HttpClientModule } from '@angular/common/http';
import { ProcessingStatusPipe } from './pipes/processing-status.pipe';



@NgModule({
  declarations: [
    InvoiceListComponent,
    InvoiceEditComponent,
    ProcessingStatusPipe
  ],
  imports: [
    CommonModule,
    HttpClientModule
  ],
  exports: [
    InvoiceListComponent
  ]
})
export class InvoicesModule { }
