import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { ProductService } from '../services/product.service';
import { OrderService } from '../services/order.service';
import { CategoryTableComponent } from '../category/category-table/category-table.component';
import { CategoryService } from '../services/category.service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {
  products: Array<Product> = [];
  filteredProducts: Array<Product> = [];
  isOrdersShow = false;
  isProductsShow;
  editMode = false;
  isCategoryShow = false;
  isProductsToCustomer = false;
  creatingMode = false;
  categoryName;
 categoryCreationMode
  productForEditOrCreate: any = {};
  tableName = "Product"

  constructor(private productService: ProductService, private orderService: OrderService,
    private categoryService:CategoryService,private categoryTable:CategoryTableComponent    ) {
  }

  ngOnInit() {
    this.isProductsShow = true;
    this.tableName = "Product"
    this.fetch();
  }

  editModeForProduct(product) {
    this.editMode = !this.editMode;
    this.productForEditOrCreate = product;
  }

  editProduct() {
    console.log(this.productForEditOrCreate);
    this.productService.edit(this.productForEditOrCreate).subscribe(x => {
      this.editMode = !this.editMode;
      this.fetch();
    })
  }

  selectCategory($event) {
    this.filteredProducts = this.products;

    if ($event != "All") {
      this.filteredProducts = this.filteredProducts.filter(x => {
        return x.categoryName == $event;
      })
    }

  }

  getOrdersByProduct(productId) {
    this.orderService.productId = productId;
    this.OrdersMode("");
  }

  createProductMode() {
    this.creatingMode = !this.creatingMode;
  }
  createCategoryMode(){
   this.categoryCreationMode = true;
  }

  OrdersMode(orderMode) {
    if (orderMode == "getAll")
      this.orderService.productId = null;

   this.hideOtherTables();
   this.isOrdersShow = !this.isOrdersShow;
   this.tableName = "Orders"
  }

  deleteProduct(productId) {
    this.productService.delete(productId).subscribe(x => {
      this.fetch();
    })
  }

  fetch() {
    this.productService.loadCategories().subscribe((x: []) => {
      this.products = x;
      this.filteredProducts = x;
    })
  }

  saveSelectedCategory($event) {
    this.categoryName = $event;
  }
  categoryMode(){
    this.hideOtherTables();
    this.isCategoryShow = !this.isCategoryShow;
    this.tableName = "Category"
  }

  productsToCustomerMode(){
    this.hideOtherTables();
   this.isProductsToCustomer = !this.isProductsToCustomer
    this.tableName = "Products To Customer"
  }

  hideOtherTables(){
    this.isOrdersShow = false;
    this.isProductsShow = false;
    this.isCategoryShow = false;
    this.isProductsToCustomer = false;
  }

  productMode(){
    this.hideOtherTables();
    this.isProductsShow = !this.isProductsShow;
    this.tableName = "Product"
  }

  createProduct() {
    this.productForEditOrCreate.categoryName = this.categoryName;

    this.productService.add(this.productForEditOrCreate).subscribe(x => {
      this.fetch();
      this.creatingMode = !this.creatingMode;
    })
  }
}

interface Product {
  id: any;
  code?: string;
  name?: string;
  unitPrice?: any;
  dateRegister?: any;
  isActive?: boolean;
  categoryName?: string;
}
