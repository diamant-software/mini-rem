import { Injectable } from "@angular/core";
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot } from "@angular/router";
import { Observable, tap } from "rxjs";
import { Fruit } from "../models/fruit.model";
import { FruitApiService } from "../services/fruit-api.service";
import { FruitStateService } from "../services/fruit-state.service";

@Injectable({ providedIn: 'root' })
export class FruitOverviewResolver implements Resolve<Fruit[]> {
    constructor(
        private fruitApiService: FruitApiService,
        private fruitStateService: FruitStateService) { }

    resolve(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot
    ): Observable<Fruit[]> | Promise<Fruit[]> | Fruit[] {
        return this.fruitApiService.loadFruits().pipe(tap(
            fruits => this.fruitStateService.fruits = fruits
        ));
    }
}
