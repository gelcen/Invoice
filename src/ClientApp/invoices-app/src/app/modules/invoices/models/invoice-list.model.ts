export interface InvoiceListElementViewModel {
    number: number;
    amount: number;
    createdAt: Date;
    modifiedAt: Date;
    processingStatus: number;
    paymentMethod: number;
}