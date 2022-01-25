import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { InvoiceEditComponent } from "./components/invoice-edit/invoice-edit.component";
import { InvoiceListComponent } from "./components/invoice-list/invoice-list.component";

const invoicesRoutes: Routes = [
    {
        path: '',
        component: InvoiceListComponent
    },
    {
        path: 'create',
        component: InvoiceEditComponent
    },
    {
        path: 'edit/:number',
        component: InvoiceEditComponent
    }

]

@NgModule({
    imports: [
        RouterModule.forChild(invoicesRoutes)
    ],
    exports: [
        RouterModule
    ]
})
export class InvoicesRoutingModule {}