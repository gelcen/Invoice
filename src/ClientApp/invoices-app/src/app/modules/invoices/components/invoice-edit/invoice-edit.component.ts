import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { switchMap } from 'rxjs/operators';

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
  private isEditing: boolean = false;  
  selectedNumber?: number;

  paymentMethods: SelectOption[] = [
    { number: 1, name: "Кредитная карта" },
    { number: 2, name: "Дебетовая карта" },
    { number: 3, name: "Электронный чек" },
  ]

  constructor(private location: Location,
    private route: ActivatedRoute) { 
    this.invoiceFormGroup = new FormGroup({
      invoiceNumberForm: new FormControl("", [Validators.required, Validators.pattern("^[0-9]*$")]),
      invoiceAmountForm: new FormControl("", [Validators.required, Validators.pattern(/^\-?\d+((\.|\,)\d+)?$/)]),
      paymentMethodForm: new FormControl(this.paymentMethods[0]) 
    });
  }

  ngOnInit(): void {
    this.selectedNumber = Number(this.route.snapshot.paramMap.get('number'));
    console.log(this.selectedNumber);
  }

  onSubmit(): void {
    if (this.invoiceFormGroup.valid) {
      console.log(this.invoiceFormGroup.controls.invoiceNumberForm.value);
      console.log(this.invoiceFormGroup.controls.invoiceAmountForm.value);
      console.log(this.invoiceFormGroup.controls.paymentMethodForm.value);
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
