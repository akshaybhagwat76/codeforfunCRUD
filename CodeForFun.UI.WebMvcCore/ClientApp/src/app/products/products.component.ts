import { Component, OnInit } from '@angular/core';
import { ProductService } from '../services/product.service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {
  products: Array<Product> = [];
  filteredProducts: Array<Product> = [];
  editMode = false;
  creatingMode = false;
  categoryName;
  productForEditOrCreate:any = {};

  constructor(private productService: ProductService) {
  }

  ngOnInit() {
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
createProductMode(){
  this.creatingMode = !this.creatingMode;
}

  deleteProduct(productId){
    console.log(productId);
    this.productService.delete(productId).subscribe(x=>{
      this.fetch();
    })
  }

  fetch() {
    this.productService.loadCategories().subscribe((x: []) => {
      this.products = x;
      this.filteredProducts = x;
    })
  }

  saveSelectedCategory($event){
    this.categoryName = $event;
  }

  createProduct(){
    this.productForEditOrCreate.categoryName = this.categoryName;

    this.productService.add(this.productForEditOrCreate).subscribe(x=>{
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
