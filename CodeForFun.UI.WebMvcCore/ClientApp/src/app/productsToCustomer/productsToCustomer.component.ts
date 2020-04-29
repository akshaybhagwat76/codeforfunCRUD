import { Component, OnInit, Input } from '@angular/core';
import { ProductsToCustomerService } from '../services/productsToCustomer.service';
import { ProductService } from '../services/product.service';
import { CustomerService } from '../services/customer.service';

@Component({
  selector: 'app-productsToCustomer',
  templateUrl: './productsToCustomer.component.html',
  styleUrls: ['./productsToCustomer.component.css']
})
export class ProductsToCustomerComponent implements OnInit {
  productsToCustomer: Array<any> = [];
  @Input() creatingMode = false;
  productsToCustomerForEditOrCreate: any = {};
  products: any[] = [];
  customers: any[] = [];
  constructor(private productsToCustomerService: ProductsToCustomerService, private productsService: ProductService,private customerService:CustomerService) { }

  ngOnInit() {
    this.creatingMode = this.productsToCustomerService.isCreationMode;
    this.fetch();
  }

  createProductsToCustomer(){
    this.productsToCustomerService.createNewOrder(this.productsToCustomerForEditOrCreate).subscribe(x=>{

    })
  }

  selectProduct(name) {
    this.productsToCustomerForEditOrCreate.productName = name
  }
  selectCustomer(name){
    this.productsToCustomerForEditOrCreate.customerName = name
  }

  editModeForProductsToCustomer(productToCustomer) {

  }

  loadCustomers(){
    this.customerService.getAllCustomers().subscribe(x=>{
      console.log(x);
      this.customers = x;
    })
  }

  loadProducts() {
    this.productsService.loadProducts().subscribe(x => {
    this.products = x;
      console.log(this.products)
    })
  }

  deleteProductsToCustomer(productsToCustomerId) {

  }

  fetch(){
    this.productsToCustomerService.loadProductsToCustomer().subscribe((x: Array<any>) => {
      x.forEach(elemet => {
        this.productsToCustomer.push(elemet)
      })
    })
  }
}
