import { Routes } from '@angular/router';
import { Register } from "../features/auth/register/register";
import { Login } from "../features/auth/login/login";
import { Dashboard } from "../features/dashboard/dashboard";
import { authGuard } from "../core/guards/auth-guard";
import { guestGuard } from "../core/guards/guest-guard";

export const routes: Routes = [
  {path: 'register',component: Register,canActivate: [guestGuard]},
  {path: 'login',component: Login,canActivate: [guestGuard]},
  {path: 'dashboard',component: Dashboard,canActivate: [authGuard]}
];
