import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGaurd } from './core/gaurds/auth.gaurd'; // Corrected spelling

const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' }, // Default to Home page
  
  // Home (Landing Page) - Publicly accessible
  { path: 'home', loadChildren: () => import('./Home/home.module').then(m => m.HomeModule) },

  // Admin Dashboard - Protected by AuthGuard (Only accessible by 'Admin' role)
  { 
    path: 'admin', 
    loadChildren: () => import('./admin/admin.module').then(m => m.AdminModule),
    canActivate: [AuthGaurd],
    data: { roles: ['Admin'] } // Restrict access to Admin role
  },

  // User Dashboard - Protected by AuthGuard (Only accessible by 'User' role)
  { 
    path: 'user', 
    loadChildren: () => import('./user/user.module').then(m => m.UserModule),
    canActivate: [AuthGaurd],
    data: { roles: ['User'] } // Restrict access to User role
  },

  // Authentication Pages (Login/Register) - Publicly accessible
  { path: 'auth', loadChildren: () => import('./auth/auth.module').then(m => m.AuthModule) },

  // Wildcard Route (Redirect unknown paths to Home)
  { path: '**', redirectTo: 'home' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
