import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { InvoiceListElementViewModel } from '../../models/invoice-list.model';
import { InvoiceTableRequest } from '../../requests/invoice-table.request';
import { InvoiceService } from '../../services/invoice.service';

enum ColumnSortState {
  Unsorted,
  Inc,
  Desc
}

interface Column {
  name: string;
  columnName: string;
  sortState: ColumnSortState;
}

@Component({
  selector: 'app-invoice-list',
  templateUrl: './invoice-list.component.html',
  styleUrls: ['./invoice-list.component.css']
})
export class InvoiceListComponent implements OnInit {
  columns: Column[] = [
    { 
      name: "number",
      columnName: "Номер",
      sortState: ColumnSortState.Unsorted
    },
    { 
      name: "createdAt",
      columnName: "Создан",
      sortState: ColumnSortState.Unsorted
    },
    { 
      name: "processingStatus",
      columnName: "Статус",
      sortState: ColumnSortState.Unsorted
    },
    { 
      name: "amount",
      columnName: "Сумма",
      sortState: ColumnSortState.Unsorted
    },
    { 
      name: "paymentMethod",
      columnName: "Способ оплаты",
      sortState: ColumnSortState.Unsorted
    },
  ];

  sortsList: Array<Column> = [];

  pageSizes: Array<number> = [3, 5, 10 ];
  selectedPageSize: number = this.pageSizes[1];

  tableRequest: InvoiceTableRequest = {
    filters: "",
    sorts: "",
    page: 0,
    pageSize: 5
  } as InvoiceTableRequest;

  invoices?: InvoiceListElementViewModel[];
  pagesCount?: number;
  searchFilter: string = "";

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
  
  onPageSizeChange(event: any): void {
    this.selectedPageSize = event;
    this.tableRequest.pageSize = this.selectedPageSize;
    this.fetchTable();
  }

  onColumneClick(column: Column): void {
    switch (column.sortState) {
      case ColumnSortState.Unsorted:
        column.sortState = ColumnSortState.Inc;
        break;
      case ColumnSortState.Inc:
        column.sortState = ColumnSortState.Desc;
        break;
      case ColumnSortState.Desc:
        column.sortState = ColumnSortState.Unsorted;
        break;
    }

    if (column.sortState !== ColumnSortState.Unsorted) {
      if (!this.sortsList.includes(column)) {
        this.sortsList.push(column);
      }
    }
    else {
      this.sortsList = this.sortsList.filter(c => c.name != column.name);
    }

    const sorts: string[] = [];
    
    for (let i = 0; i < this.sortsList.length; i++) {
      const el = this.sortsList[i];
      const sortString = el.sortState == ColumnSortState.Desc ? 
        "-" + el.name : el.name;
      sorts.push(sortString);
    }

    this.tableRequest.sorts = sorts.join(',');
    
    this.fetchTable();
  }

  onSearchClick(): void {
    console.log(this.tableRequest);
    this.tableRequest.filters = this.searchFilter;
    console.log(this.tableRequest);
    this.fetchTable();
  }

  private fetchTable(): void {
    this.invoiceService.getInvoices(this.tableRequest)
      .subscribe(result => {
        this.invoices = [];
        this.invoices = result.data;
        this.pagesCount = result.pagesCount;
      });
  }
}
