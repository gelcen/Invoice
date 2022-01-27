import { NgModule } from "@angular/core";
import { Router, RouterModule, Routes } from "@angular/router";

const appRoutes: Routes = [
    {
        path: 'invoice',
        loadChildren: () => import('./modules/invoices/invoices.module').then(m => m.InvoicesModule)
    },
    {
        path: '', 
        redirectTo: '/invoice',
        pathMatch: 'full'
    }
];

@NgModule({
    imports: [
        RouterModule.forRoot(appRoutes)
    ],
    exports: [
        RouterModule
    ]
})
export class AppRoutingModule {}