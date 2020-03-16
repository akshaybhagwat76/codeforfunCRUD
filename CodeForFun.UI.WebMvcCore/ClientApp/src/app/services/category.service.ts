import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  constructor(private http: HttpClient) { }

  loadCategories() {
    const sub = new Subject<any>();

    this.http.get('/api/category/GetAll').subscribe(x => {
      sub.next(x)
      sub.complete();
    })

    return sub;

  }
}
