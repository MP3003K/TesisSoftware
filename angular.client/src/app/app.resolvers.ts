import { forkJoin } from 'rxjs';
import { NavigationService } from 'app/core/navigation/navigation.service';
import { inject } from '@angular/core';

export const initialDataResolver = () => {
    const navigationService = inject(NavigationService);
    return forkJoin([
        navigationService.get(),
    ]);
};
