import { Component, OnInit } from '@angular/core';
import { ValidateString } from 'src/helper/validate/validate.string';
import { HelperTraffic } from 'src/helper/traffics/helper.traffic';

@Component({
  selector: 'app-voucher',
  templateUrl: './voucher.component.html',
  styleUrls: ['./voucher.component.css']
})
export class VoucherComponent implements OnInit {

  inputVoucherName: string = '';
  inputVoucherValue: string = '';

  redeemedVouchers1: string[] = [];
  redeemedVouchers2: string[] = [];

  constructor(private traffic: HelperTraffic) { }

  ngOnInit() {
  }

  produceVoucher() {
    const apiUrl = 'http://localhost:5353/voucher_producer_api/v1/Voucher/ProduceVoucher';
    let _validate = new ValidateString();
    if(_validate.IsNullOrEmpty(this.inputVoucherName) || _validate.IsNullOrEmpty(this.inputVoucherValue)){
      alert("Value is empty");
      return;
    }

    console.log(this.inputVoucherValue)
      let _data = {
        "voucherName": this.inputVoucherName,
        "voucherValue": this.inputVoucherValue
      };
  
      this.traffic._PostApiCall(apiUrl, _data).subscribe(
        (response) => {
            console.log("API Response:", response);
            if(response == true){
              alert("Voucher Produced");
            }
        },
        (error) => {
        }
      );
  }

  redeemVoucher(clientId: string) {
    const apiUrl = 'http://localhost:5656/voucher_client_api/v1/Voucher/RedeemVoucher';
  
    
    switch(clientId) { 
      case 'client1': { 
        this.traffic._PostApiCall(apiUrl, null).subscribe(
          (response) => {
            if(response.status == '1' && response?.result?.voucherName != null){
              this.redeemedVouchers1.push(response.result.voucherName);
            }
            console.log("API Response:", response);
          },
          (error) => {
          }
        );
         break; 
      } 
      case 'client2': { 
        this.traffic._PostApiCall(apiUrl, null).subscribe(
          (response) => {
            if(response.status == '1' && response?.result?.voucherName != null){
              this.redeemedVouchers2.push(response.result.voucherName);
            }
            console.log("API Response:", response);
          },
          (error) => {
          }
        );
         break; 
      } 
      default: { 
         alert("Invalid");
         break; 
      } 
   } 
  }

}
