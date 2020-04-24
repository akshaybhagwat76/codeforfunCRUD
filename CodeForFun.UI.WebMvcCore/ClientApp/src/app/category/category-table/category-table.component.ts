import { Component, OnInit, Input } from '@angular/core';
import { CategoryService } from 'src/app/services/category.service';

@Component({
  selector: 'app-category-table',
  templateUrl: './category-table.component.html',
  styleUrls: ['./category-table.component.css']
})
export class CategoryTableComponent implements OnInit {
 categories:any[] = [];
 editMode = false;
 @Input() creatingMode = false;
 categoryForEditOrCreate:any = {};
  constructor(private categoryService:CategoryService) { }

  ngOnInit() {
   this.fetch();
  }

  createCategory(){
    this.categoryService.createCategory(this.categoryForEditOrCreate).subscribe(x=>{
      this.creatingMode = !this.creatingMode;
      this.fetch();
    })
  }

  editCategory(){
    this.categoryService.editCategory(this.categoryForEditOrCreate).subscribe(x=>{
      this.fetch();
    })
  }
  
  selectCategory(categoryName){
    this.categoryForEditOrCreate.parentName = categoryName;
  }



  editModeForCategory(product){
    this.categoryForEditOrCreate.name = product.name;
    this.categoryForEditOrCreate.parentName = product.parentName;
    this.categoryForEditOrCreate.Id = product.id;
    this.editMode  = !this.editMode;
  }

  deleteCategory(category){
    console.log(category)
    this.categoryService.deleteCategory(category).subscribe(x=>{
      this.fetch();
    })
  }

  fetch(){
    this.categories = [];
    this.categoryService.loadCategoriesWithParents().subscribe((x:Array<any>)=>{
      x.forEach(y=>{
        this.categories.push(y);
      })
    })
  }

}
