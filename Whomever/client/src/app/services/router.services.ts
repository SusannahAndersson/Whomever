import { RouterModule } from "@angular/router";
import CheckoutPage from "../pages/checkoutPage.component";
import LoginPage from "../pages/loginPage.component";
import WebshopPage from "../pages/webshopPage.component";
import Auth from "./auth.service";

const routes = [
  { path: "", component: WebshopPage },
  { path: "checkout", component: CheckoutPage, canActivate: [Auth] },
  { path: "login", component: LoginPage },
  { path: "**", redirectTo: "/" }
];

//creates routes
const router = RouterModule.forRoot(routes, {useHash: false});

export default router;
