import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { InvoiceListElementViewModel } from '../../models/invoice-list.model';
import { InvoiceService } from '../../services/invoice.service';

@Component({
  selector: 'app-invoice-list',
  templateUrl: './invoice-list.component.html',
  styleUrls: ['./invoice-list.component.css']
})
export class InvoiceListComponent implements OnInit {

  invoices: InvoiceListElementViewModel[] = [];

  constructor(private invoiceService: InvoiceService,
    private router: Router) { }

  ngOnInit(): void {
    this.invoiceService.getInvoices().subscribe(result =>
      this.invoices = result);
  }

  onEditClick(invoice: InvoiceListElementViewModel): void {
    this.router.navigate(['./edit', invoice.number]);
  }
}
