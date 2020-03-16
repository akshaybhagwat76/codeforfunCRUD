import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private http: HttpClient) { }

  loadCategories() {
    const sub = new Subject<any>();

    this.http.get('/api/product/GetAll').subscribe(x => {
      sub.next(x)
      sub.complete();
    })

    return sub;

  }

  edit(product) {
    const sub = new Subject<any>();

    this.http.put('/api/product/', product).subscribe(x => {
      sub.next(x)
      sub.complete();
    })

    return sub;

  }

  delete(productId) {
    const sub = new Subject<any>();

    this.http.delete('/api/product?productId=' + productId).subscribe(x => {
      sub.next(x)
      sub.complete();
    })

    return sub;
  }

  add(product){
    const sub = new Subject<any>();

    this.http.post('/api/product/',product).subscribe(x => {
      sub.next(x)
      sub.complete();
    })

    return sub;
  }
}
