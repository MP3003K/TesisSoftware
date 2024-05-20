import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    name: 'filterTable',
    standalone: true,
})
export class FilterTablePipe implements PipeTransform {
    transform(value: any[], ...args: any[]) {
        const [filter] = args;

        return value.filter((e) =>
            (Object.values(e) as string[]).some((e) =>
                e
                    .toString()
                    .normalize('NFD')
                    .replace(/[\u0300-\u036f]/g, '')
                    .toLowerCase()
                    .includes(
                        filter
                            .toString()
                            .normalize('NFD')
                            .replace(/[\u0300-\u036f]/g, '')
                            .toLowerCase()
                    )
            )
        );
    }
}
