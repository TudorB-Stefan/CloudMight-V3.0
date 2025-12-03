import { Routes } from '@angular/router';
import { Register } from "../app/features/auth/register/register";
import { Login } from "../app/features/auth/login/login";
import { Dashboard } from "../app/features/dashboard/dashboard";
import { authGuard } from "../app/core/guards/auth-guard";
import { guestGuard } from "../app/core/guards/guest-guard";
import { rootGuard } from "../app/core/guards/root-guard";


export const routes: Routes = [
  {path: '',canActivate: [rootGuard],children: []},
  {path: 'register',component: Register,canActivate: [guestGuard]},
  {path: 'login',component: Login,canActivate: [guestGuard]},
  {path: 'dashboard',component: Dashboard,canActivate: [authGuard]}
];
