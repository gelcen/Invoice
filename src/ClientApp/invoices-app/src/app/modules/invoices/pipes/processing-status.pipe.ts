import { Pipe, PipeTransform } from '@angular/core';

@Pipe({name: 'processingStatus'})
export class ProcessingStatusPipe implements PipeTransform {
  transform(value: number): string {
    let result: string = "";
    switch (value) {
        case 1:
            result = "Новый";
            break;
        case 2:
            result = "Оплачен";
            break;
        case 3:
            result = "Отменён";
            break;
        default:
            break;
    }
    return result;
  }
}