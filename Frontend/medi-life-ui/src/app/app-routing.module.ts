import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGaurd } from './core/gaurds/auth.gaurd';

const routes: Routes = [
  { path: '', redirectTo: 'auth/register', pathMatch: 'full' }, // Default to register
  { path: 'admin', loadChildren: () => import('./admin/admin.module').then(m => m.AdminModule) }, // Lazy load admin module
  { path: 'user', loadChildren: () => import('./user/user.module').then(m => m.UserModule) }, // Lazy load user module
  { path: 'auth', loadChildren: () => import('./auth/auth.module').then(m => m.AuthModule) }  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
