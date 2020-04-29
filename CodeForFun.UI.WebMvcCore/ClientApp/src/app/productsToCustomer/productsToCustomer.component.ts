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
  editMode = false;

  constructor(private productsToCustomerService: ProductsToCustomerService, private productsService: ProductService,private customerService:CustomerService) { }

  ngOnInit() {
    this.creatingMode = this.productsToCustomerService.isCreationMode;
    this.fetch();
  }

  createProductsToCustomer(){
    this.productsToCustomerService.update(this.productsToCustomerForEditOrCreate).subscribe(x=>{
      this.fetch();
    })
  }

  selectProduct(name) {
    this.productsToCustomerForEditOrCreate.productName = name
  }
  selectCustomer(name){
    this.productsToCustomerForEditOrCreate.customerName = name
  }

  editModeForProductsToCustomer(productToCustomer) {
    this.productsToCustomerForEditOrCreate.customerName = productToCustomer.customer.name 
    this.productsToCustomerForEditOrCreate.Id = productToCustomer.productsToCustomerId
    this.productsToCustomerForEditOrCreate.productName = productToCustomer.product.name
    this.editMode = !this.editMode;
  }

  loadCustomers(){
    this.customerService.getAllCustomers().subscribe(x=>{
      this.customers = x;
    })
  }

  editProductToCustomer(){
    this.productsToCustomerService.update(this.productsToCustomerForEditOrCreate).subscribe(x=>{
      this.fetch();
    })
  }

  loadProducts() {
    this.productsService.loadProducts().subscribe(x => {
    this.products = x;
    })
  }

  deleteProductsToCustomer(productsToCustomerId) {
    this.productsToCustomerService.deleteProductsToCustomer(productsToCustomerId).subscribe(x=>{
      this.fetch();
    })
  }

  fetch(){
    this.productsToCustomer = [];
    this.productsToCustomerService.loadProductsToCustomer().subscribe((x: Array<any>) => {
      x.forEach(elemet => {
        this.productsToCustomer.push(elemet)
      })
    })
  }
}
