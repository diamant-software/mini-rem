import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, of } from 'rxjs';
import { Fruit } from '../../models/fruit.model';
import { FruitApiService } from '../../services/fruit-api.service';
import { FruitStateService } from '../../services/fruit-state.service';

@Component({
    selector: 'mini-rem-fruit-overview',
    templateUrl: './fruit-overview.component.html',
    styleUrls: ['./fruit-overview.component.less']
})
export class FruitOverviewComponent implements OnInit {

    public fruits: Observable<Fruit[]> = of([]);

    constructor(
        private router: Router,
        private fruitStateService: FruitStateService,
        private fruitApiService: FruitApiService) { }

    ngOnInit(): void {
        this.fruits = this.fruitStateService.currentFruits$;
    }

    public sendClassification(fruit: Fruit) {
        fruit.classifiedByUser = false;
        this.fruitApiService.sendClassification(fruit).subscribe(ok => {
            if (ok) {
                fruit.classifiedByUser = true;
            } else {
                fruit.error = "Sender der Klassifikation fehlgeschlagen.";
            }
        });
    }
}
