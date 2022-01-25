import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Invoice } from '../../models/invoice.model';
import { InvoiceService } from '../../services/invoice.service';

@Component({
  selector: 'app-invoice-list',
  templateUrl: './invoice-list.component.html',
  styleUrls: ['./invoice-list.component.css']
})
export class InvoiceListComponent implements OnInit {

  invoices: Invoice[] = [];

  constructor(private invoiceService: InvoiceService,
    private router: Router) { }

  ngOnInit(): void {
    this.invoiceService.getInvoices().subscribe(result =>
      this.invoices = result);
  }

  onEditClick(invoice: Invoice): void {
    this.router.navigate(['./edit', invoice.number]);
  }
}
