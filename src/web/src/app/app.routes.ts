import { Routes } from '@angular/router';
import { AddPageComponent } from './components/add-page/add-page.component';
import { UpdatePageComponent } from './components/update-page/update-page.component';
import { ProductListComponent } from './components/product-list/product-list.component';

export const routes: Routes = [
    { path: "", component: ProductListComponent},
    { path: "add", component: AddPageComponent},
    { path: "update", component: UpdatePageComponent}
];
