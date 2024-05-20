import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    name: 'filterTable',
    standalone: true,
})
export class FilterTablePipe implements PipeTransform {
    transform(value: any[], ...args: any[]) {
        const [filter] = args;

        return value.filter((e) =>
            Object.values(e).some((e) =>
                e.toString().toLowerCase().includes(filter.toLowerCase())
            )
        );
    }
}
