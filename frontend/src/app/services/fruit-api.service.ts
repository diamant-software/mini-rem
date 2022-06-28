import { catchError, map } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { environment } from '../../environments/environment';
import { Fruit } from "../models/fruit.model";

/*declare var require: any;*/

@Injectable({
    providedIn: 'root'
})
export class FruitApiService {
    constructor(private http: HttpClient) { }

    private getFruitRecognitionService = () => environment.fruitRecognitionServiceUrl;

    loadFruits(): Observable<Fruit[]> {
        return this.http.get<Fruit[]>(this.getFruitRecognitionService())
            .pipe(
                map((m: Fruit[]) => { return m }),
                catchError(() => of([]))
            );
    }

    sendClassification(fruit: Fruit): Observable<boolean> {
        let url = this.getFruitRecognitionService() + "/" + fruit.id + "/classify";
        let body = { type: fruit.type };
        return this.http.post<string>(url, body)
            .pipe(
                map(x => { return true }),
                catchError(() => of(false))

            );
    }

    //loadFruits(): Observable<Fruit[]>  {
    //    return of(JSON.parse(JSON.stringify(require("../mocks/mock-fruits.json"))));
    //}
}
