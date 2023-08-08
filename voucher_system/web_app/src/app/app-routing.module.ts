import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {path: '', loadChildren: () => import('../module/voucher/voucher.module').then(x => x.VoucherModule)},
  {path: 'voucher', loadChildren: () => import('../module/voucher/voucher.module').then(x => x.VoucherModule)},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
