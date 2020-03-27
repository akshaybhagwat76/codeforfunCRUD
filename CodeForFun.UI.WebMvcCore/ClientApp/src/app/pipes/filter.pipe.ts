import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'filter'
})
export class FilterPipe implements PipeTransform {
  elem = "";

  transform(items: any[], arg: any): any {
    var returnItems: any[] = [];
    


    if (arg.filt == undefined || arg.filt == "") {
      return items;
    }
    var keys = items ? Object.keys(items[0]) : null;
    if (arg.name == "") {
      return items.filter(item =>
        Object.keys(item).some(
          k =>
            item[k] != null &&
            item[k]
              .toString()
              .includes(arg.filt)
        )
      );

    } else {
      return items.filter(x => x[arg.name.substring(0, 1).toLowerCase() + arg.name.substring(1)].toString().includes(arg.filt))
    }
  }

}

