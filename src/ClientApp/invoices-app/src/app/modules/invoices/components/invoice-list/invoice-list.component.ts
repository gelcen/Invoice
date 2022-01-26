import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { InvoiceListElementViewModel } from '../../models/invoice-list.model';
import { InvoiceTableRequest } from '../../requests/invoice-table.request';
import { InvoiceService } from '../../services/invoice.service';

@Component({
  selector: 'app-invoice-list',
  templateUrl: './invoice-list.component.html',
  styleUrls: ['./invoice-list.component.css']
})
export class InvoiceListComponent implements OnInit {

  tableRequest: InvoiceTableRequest = {
    filters: "",
    sorts: "",
    page: 0,
    pageSize: 5
  } as InvoiceTableRequest;

  invoices?: InvoiceListElementViewModel[];
  pagesCount?: number;

  constructor(private invoiceService: InvoiceService,
    private router: Router) { }

  ngOnInit(): void {
    this.fetchTable();
  }

  onPageItemClick(pageNumber: number): void {
    this.tableRequest.page = pageNumber;
    this.fetchTable();
  }

  onEditClick(invoice: InvoiceListElementViewModel): void {
    this.router.navigate(['./edit', invoice.number]);
  }

  private fetchTable(): void {
    this.invoiceService.getInvoices(this.tableRequest)
      .subscribe(result => {
        this.invoices = result.data;
        this.pagesCount = result.pagesCount;
      });
  }
}
