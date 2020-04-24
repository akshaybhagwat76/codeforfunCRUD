import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProductsToCustomerService {

  constructor(private http: HttpClient) { }


loadProductsToCustomer(){
  const sub = new Subject<any>();

  this.http.get('/api/ProductsToCustomer/GetAll').subscribe(x => {
    sub.next(x)
  })

  return sub;

}

}
