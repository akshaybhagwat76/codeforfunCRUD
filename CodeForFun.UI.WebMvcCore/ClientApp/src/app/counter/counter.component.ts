import { Component } from '@angular/core';
import { ProductService } from '../services/product.service';
import { OrderService } from '../services/order.service';

@Component({
  selector: 'app-counter-component',
  templateUrl: './counter.component.html'
})
export class CounterComponent {
  products: Array<Product> = [];
  filteredProducts: Array<Product> = [];
  isOrdersShow = false;
  isProductsShow;
  editMode = false;
  creatingMode = false;
  categoryName;
  tableContainer: any;
  lstProducts: any;
  productForEditOrCreate: any = {};
  ajaxResponse: string = '';

  constructor(private productService: ProductService, private orderService: OrderService) {
  }

  ngOnInit() {
    this.tableContainer = true;
    this.isProductsShow = true;
    this.loadProducts();
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
    this.productService.loadProducts().subscribe((x => {
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
    this.productForEditOrCreate = {};
    this.productForEditOrCreate.idNavigation = {};
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

  //Ajax function on key up event

  doAjax(text: any) {
    var xhr = new XMLHttpRequest();
    xhr.onreadystatechange = function () {
      if (this.readyState == 4 && this.status == 200) {
        // Typical action to be performed when the document is ready:
        alert(xhr.responseText);
      }
    };
    xhr.open("GET", '/api/ProductDetails/Ajax?text=' + text.viewModel, true);
    xhr.send();
  }
  setSpan(text: any) {
    this.ajaxResponse = text;
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
    debugger

    this.productService.addProductDetail(this.productForEditOrCreate).subscribe(x => {
      this.fetch();
      this.creatingMode = !this.creatingMode;
      this.tableContainer = true;
      this.isProductsShow = true;
      this.loadProducts();
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
