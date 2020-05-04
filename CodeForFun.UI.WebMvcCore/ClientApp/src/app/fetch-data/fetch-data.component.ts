import { Component, Inject, Input } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ProductService } from '../services/product.service';
import { CustomerService } from '../services/customer.service';
import { ProductsToCustomerService } from '../services/productsToCustomer.service';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  productsToCustomer: Array<any> = [];
  @Input() creatingMode = false;
  productsToCustomerForEditOrCreate: any = {};
  products: any[] = [];
  customers: any[] = [];
  editMode = false;
  tableContainer: any;

  constructor(private productsToCustomerService: ProductsToCustomerService, private productsService: ProductService, private customerService: CustomerService) { }

  ngOnInit() {
    this.tableContainer = true;
    this.creatingMode = this.productsToCustomerService.isCreationMode;
    this.fetch();
  }

  createProductMode() {
    this.tableContainer = false;
    this.productsToCustomerForEditOrCreate = {};
    this.creatingMode = !this.creatingMode;
  }

  createProductsToCustomer() {
    debugger
    this.productsToCustomerService.createNewOrder(this.productsToCustomerForEditOrCreate).subscribe(x => {
      debugger
      this.fetch();
      this.creatingMode = !this.creatingMode;
      this.productsToCustomerForEditOrCreate = null;
      this.productsToCustomerService.isCreationMode = false;
      this.tableContainer = true;
    })
  }

  selectProduct(name) {
    this.productsToCustomerForEditOrCreate.productName = name
  }
  selectCustomer(name) {
    this.productsToCustomerForEditOrCreate.customerName = name
  }

  editModeForProductsToCustomer(productToCustomer) {
    this.productsToCustomerForEditOrCreate.customerName = productToCustomer.customer.name
    this.productsToCustomerForEditOrCreate.Id = productToCustomer.productsToCustomerId
    this.productsToCustomerForEditOrCreate.productName = productToCustomer.product.name
    this.editMode = !this.editMode;
  }

  loadCustomers() {
    this.customerService.getAllCustomers().subscribe(x => {
      this.customers = x;
    })
  }

  editProductToCustomer() {
    this.productsToCustomerService.update(this.productsToCustomerForEditOrCreate).subscribe(x => {
      this.editMode = !this.editMode;
      this.fetch();
    })
  }

  loadProducts() {
    this.productsService.loadProducts().subscribe(x => {
      this.products = x;
    })
  }

  deleteProductsToCustomer(productsToCustomerId) {
    debugger
    this.productsToCustomerService.deleteProductsToCustomer(productsToCustomerId).subscribe(x => {
      this.fetch();
    })
  }

  fetch() {
    debugger
    this.productsToCustomer = [];
    this.productsToCustomerService.loadProductsToCustomer().subscribe((x: Array<any>) => {
      debugger
      x.forEach(elemet => {
        this.productsToCustomer.push(elemet)
      })
    })
  }
}

interface WeatherForecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}
