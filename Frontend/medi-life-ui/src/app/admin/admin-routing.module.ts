import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { AuthGaurd } from '../core/gaurds/auth.gaurd';

const routes: Routes = [
  { path: 'dashboard', component: DashboardComponent, canActivate: [AuthGaurd] } // Protected Route
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
