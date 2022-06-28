import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FruitOverviewResolver } from './resolvers/fruit-overview-resolver';
import { FruitOverviewComponent } from './components/fruit-overview/fruit-overview.component';

const routes: Routes = [
    { path: '', redirectTo: '/fruits', pathMatch: 'full' },
    {
        path: 'fruits', component: FruitOverviewComponent, resolve: {
            fruits: FruitOverviewResolver,
        },
    },
];

@NgModule({
    imports: [RouterModule.forRoot(routes, { useHash: true })],
    exports: [RouterModule]
})
export class AppRoutingModule { }
