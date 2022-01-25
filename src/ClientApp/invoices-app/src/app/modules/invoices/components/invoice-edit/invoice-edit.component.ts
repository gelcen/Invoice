import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

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
  invoiceFormGroup: FormGroup;

  paymentMethods: SelectOption[] = [
    { number: 1, name: "Кредитная карта" },
    { number: 2, name: "Дебетовая карта" },
    { number: 3, name: "Электронный чек" },
  ]

  constructor() { 
    this.invoiceFormGroup = new FormGroup({
      invoiceNumber: new FormControl("", [Validators.required, Validators.pattern("^[0-9]*$")]),
      invoiceAmount: new FormControl("", [Validators.required, Validators.pattern(/^\-?\d+((\.|\,)\d+)?$/)]),
      paymentMethod: new FormControl(this.paymentMethods[0]) 
    });
  }

  ngOnInit(): void {
    
  }

  onSubmit(): void {
    if (this.invoiceFormGroup.valid) {
      console.log(this.invoiceFormGroup.controls.invoiceNumber.value);
      console.log(this.invoiceFormGroup.controls.invoiceAmount.value);
      console.log(this.invoiceFormGroup.controls.paymentMethod.value);
    }
  }

  get invoiceNumber() {
    return this.invoiceFormGroup.controls.invoiceNumber;
  }

  get invoiceAmount() {
    return this.invoiceFormGroup.controls.invoiceAmount;
  }
}
