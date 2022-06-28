import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Fruit } from '../models/fruit.model';

@Injectable({
    providedIn: 'root'
})
export class FruitStateService {

    constructor() { }

    // readonly and private subjects to prevent overwriting subject and instead only allow "pushing" to stream
    private readonly currentFruits: BehaviorSubject<Fruit[]> = new BehaviorSubject<Fruit[]>([]);

    // provide subject as observable to prevent working on subject
    readonly currentFruits$ = this.currentFruits.asObservable();


    get fruits(): Fruit[] {
        return this.currentFruits.getValue();
    }

    set fruits(fruits: Fruit[]) {
        this.currentFruits.next(fruits);
    }
}
