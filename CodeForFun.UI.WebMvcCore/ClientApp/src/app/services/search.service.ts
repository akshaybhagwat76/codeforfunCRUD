import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SearchService {

  constructor(private http: HttpClient) { }

  loadTablesNames() {
    const sub = new Subject<any>();

    this.http.get('/api/Select/GetAll').subscribe(x => {
      sub.next(x)
      sub.complete();
    })

    return sub;

  }

  searchInFile(files) {
    const sub = new Subject<any>();

    this.http.post('/api/Select/SearchInFiles', files).subscribe(x => {
      sub.next(x)
      sub.complete();
    })

    return sub;

  }


  getTableProps(table) {
    const sub = new Subject<any>();

    this.http.get('/api/Select/GetProperties?name=' + table).subscribe(x => {
      sub.next(x)
      sub.complete();
    })

    return sub;

  }
}
