import { Component, OnInit } from '@angular/core';
import { ProductsToCustomerService } from '../services/productsToCustomer.service';

@Component({
  selector: 'app-productsToCustomer',
  templateUrl: './productsToCustomer.component.html',
  styleUrls: ['./productsToCustomer.component.css']
})
export class ProductsToCustomerComponent implements OnInit {
  productsToCustomer: Array<any> = [];
  constructor(private productsToCustomerService:ProductsToCustomerService) { }

  ngOnInit() {
   this.productsToCustomerService.loadProductsToCustomer().subscribe((x: Array<any>) => {
      x.forEach(elemet => {
        console.log(x);
        this.productsToCustomer.push(elemet)
      })
    }) 
  }

  editModeForProductsToCustomer(productToCustomer){

  }

  deleteProductsToCustomer(productsToCustomerId){
    
  }
}
