import { InvoiceListElementViewModel } from "./invoice-list.model";

export interface InvoiceTableViewModel {
    pagesCount: number;
    data: InvoiceListElementViewModel[]
}