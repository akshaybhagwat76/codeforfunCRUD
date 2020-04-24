import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  createMode = false;

  constructor(private http: HttpClient) { }

  loadCategories() {
    const sub = new Subject<any>();

    this.http.get('/api/category/GetAll').subscribe(x => {
      sub.next(x)
    })

    return sub;

  }

  createCategory(category){
    const sub = new Subject<any>();

    this.http.post('/api/category',category).subscribe(x => {
      sub.next(x)
      sub.complete();
    })

    return sub;
  }

  deleteCategory(categoryName){
    const sub = new Subject<any>();

    this.http.delete('/api/category?categoryName='+categoryName).subscribe(x => {
      sub.next(x)
      sub.complete();
    })

    return sub;
  }


  editCategory(category){
    const sub = new Subject<any>();

    this.http.put('/api/category',category).subscribe(x => {
      sub.next(x)
      sub.complete();
    })

    return sub;
  }

  loadCategoriesWithParents() {
    const sub = new Subject<any>();

    this.http.get('/api/category/GetAllWithParents').subscribe(x => {
      sub.next(x)
    })

    return sub;

  }
}
