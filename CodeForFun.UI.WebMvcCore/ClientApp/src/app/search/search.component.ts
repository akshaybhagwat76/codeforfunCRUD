import { Component, OnInit } from '@angular/core';
import { SearchService } from '../services/search.service';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {
tableNames = [];
files = [];
filt = "";
tableProperties:SearchModel;
selectedTable = ""
isTableSelected = true;
selectedTableProperty = ""
  constructor(private searchService:SearchService) { }

  ngOnInit() {
    this.searchService.loadTablesNames().subscribe((x:any[])=>{
      x.forEach(element => {
        this.tableNames.push(element);
      });
    })
  }

  selecTable(name){
    this.selectedTable = name;

    this.searchService.getTableProps(name).subscribe(x=>{
      console.log(x)
        this.tableProperties = {
          Properties:x.properties,
          Entities:x.entities,
          EntityName:x.entityName
        };
        console.log(this.tableProperties)
    })
  }

  selectProperty(property){
    this.selectedTableProperty = property;
  }
  deleteIdsFromArray(array:[]){
    array.forEach
  }

  fileEvent(fileInput:any){
    const formData = new FormData()
    for(var file of fileInput.target.files){
      formData.append(file.name,file)
    }
    this.searchService.searchInFile(formData).subscribe((x:any[])=>{
      x.forEach(element => {
        this.files.push(element);
      });
    })
}

selectSearchType(type){
  if(type == "Table"){
    this.isTableSelected = true;
  }else{
    this.isTableSelected = false;
  }
}
  
}
interface SearchModel{
  Properties:Array<string>,
  Entities:any[],
  EntityName:string
}