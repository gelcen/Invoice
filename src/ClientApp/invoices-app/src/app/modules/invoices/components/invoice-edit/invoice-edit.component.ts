import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { InvoiceService } from '../../services/invoice.service';
import { InvoiceEditViewModel } from "src/app/modules/invoices/models/invoice-edit.model";

interface SelectOption {
  number: number;
  name: string;
}

@Component({
  selector: 'app-invoice-edit',
  templateUrl: './invoice-edit.component.html',
  styleUrls: ['./invoice-edit.component.css']
})
export class InvoiceEditComponent implements OnInit {
  private invoice: InvoiceEditViewModel = {} as InvoiceEditViewModel;
  invoiceFormGroup: FormGroup;
  private isEditing: boolean = false;  
  selectedNumber?: number;

  paymentMethods: SelectOption[] = [
    { number: 1, name: "Кредитная карта" },
    { number: 2, name: "Дебетовая карта" },
    { number: 3, name: "Электронный чек" },
  ]

  constructor(private location: Location,
    private route: ActivatedRoute,
    private invoiceService: InvoiceService) { 
    this.invoiceFormGroup = new FormGroup({
      invoiceNumberForm: new FormControl("", [Validators.required, Validators.pattern("^[0-9]*$")]),
      invoiceAmountForm: new FormControl("", [Validators.required, Validators.pattern(/^\-?\d+((\.|\,)\d+)?$/)]),
      paymentMethodForm: new FormControl(this.paymentMethods[0]) 
    });
  }

  ngOnInit(): void {
    let number = this.route.snapshot.paramMap.get('number');
    if (number === null) {
      this.isEditing = false;
    }
    else {
      this.isEditing = true;
      this.selectedNumber = Number(number);
      this.invoiceService.getInvoiceByNumber(this.selectedNumber)
        .subscribe(result => {
          this.invoice = result;
          this.invoiceNumberForm.setValue(this.invoice.number);
          this.invoiceAmountForm.setValue(this.invoice.amount);
          this.invoiceFormGroup.controls.paymentMethodForm.setValue(
            this.paymentMethods.find((m) => {
              return m.number === result.paymentMethod
            })
          );
        });
    }
  }

  onSubmit(): void {
    if (this.invoiceFormGroup.valid) {
      console.log(this.invoiceFormGroup.controls.invoiceNumberForm.value);
      console.log(this.invoiceFormGroup.controls.invoiceAmountForm.value);
      console.log(this.invoiceFormGroup.controls.paymentMethodForm.value);

      let invoice = {
        number: this.invoiceFormGroup.controls.invoiceNumberForm.value,
        amount: this.invoiceFormGroup.controls.invoiceAmountForm.value,
        paymentMethod: this.invoiceFormGroup.controls.paymentMethodForm.value.number
      } as InvoiceEditViewModel;

      if (this.isEditing) {
        this.invoiceService.updateInvoice(invoice).subscribe(
          _ => this.location.back()
        );
      }
      else {
        this.invoiceService.addInvoice(invoice).subscribe(
          _ => {
            this.invoiceFormGroup.reset();
            this.invoiceFormGroup.controls.paymentMethodForm.setValue(this.paymentMethods[0]);
          }
        );
      }
    }
  }

  onCloseClick(): void {
    this.location.back();
  }

  get invoiceNumberForm() {
    return this.invoiceFormGroup.controls.invoiceNumberForm;
  }

  get invoiceAmountForm() {
    return this.invoiceFormGroup.controls.invoiceAmountForm;
  }
}
