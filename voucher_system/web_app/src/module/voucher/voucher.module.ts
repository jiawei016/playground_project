import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { VoucherComponent } from './voucher.component';
import { FormsModule } from '@angular/forms';

const routes: Routes = [
    { path: '', component: VoucherComponent },
    { path: 'management', component: VoucherComponent }
];

@NgModule({
  declarations: [VoucherComponent],
  imports: [
    FormsModule,
    CommonModule,
    RouterModule.forChild(routes)
  ]
})
export class VoucherModule { }