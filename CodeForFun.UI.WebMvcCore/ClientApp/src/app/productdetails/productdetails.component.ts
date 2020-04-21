import { Component, OnInit } from '@angular/core';
import { ProductService } from '../services/product.service';
import { OrderService } from '../services/order.service';

@Component({
  selector: 'app-productdetails',
  templateUrl: './productdetails.component.html'
})
export class ProductsDetailsComponent implements OnInit {
  products: Array<Product> = [];
  filteredProducts: Array<Product> = [];
  isOrdersShow = false;
  isProductsShow;
  editMode = false;
  creatingMode = false;
  categoryName;
  tableContainer: bool=true;
  lstProducts: any;
  productForEditOrCreate: any = {};

  constructor(private productService: ProductService, private orderService: OrderService) {
  }

  ngOnInit() {
    
    this.isProductsShow = true;
    this.fetch();
  }

  editModeForProduct(product) {
    this.loadProducts();
    this.editMode = !this.editMode;
    this.productForEditOrCreate = product;
  }

  editProduct() {
    this.productService.editProductDetails(this.productForEditOrCreate).subscribe(x => {
        this.editMode = false;
      })
  }

  loadProducts() {
    this.productService.loadCategories().subscribe((x => {
      this.lstProducts = x;
    }))
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
    this.tableContainer = false;  
    this.loadProducts();
    this.creatingMode = !this.creatingMode;
  }

  OrdersMode(orderMode) {
    if (orderMode == "getAll")
      this.orderService.productId = null;

    this.isOrdersShow = !this.isOrdersShow;
    this.isProductsShow = !this.isProductsShow;
  }
  deleteProductDetail(productId) {
    this.productService.deleteProductDetail(productId).subscribe(x => {
      this.fetch();
    })

  }
  fetch() {
    this.productService.getAllProductDetails().subscribe((x: []) => {
      this.products = x;
      this.filteredProducts = x;
    })
  }

  saveSelectedCategory($event) {
    this.categoryName = $event;
  }

  createProduct() {
    //this.productService.addProductDetail(this.productForEditOrCreate).subscribe(x => {
    //  this.fetch();
    //  this.creatingMode = !this.creatingMode;
    //})
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
