import { RouterModule } from "@angular/router";
import CheckoutPage from "../pages/checkoutPage.component";
import LoginPage from "../pages/loginPage.component";
import WebshopPage from "../pages/webshopPage.component";
import Activator from "./activator.service";
import OrderPage from "../pages/orderPage.component";

const routes = [
  { path: "", component: WebshopPage },
  { path: "checkout", component: CheckoutPage, canActivate: [Activator] },
  { path: "login", component: LoginPage },
  { path: "order", component: OrderPage, canActivate: [Activator] },
  { path: "**", redirectTo: "/" }
];

//creates routes
const router = RouterModule.forRoot(
  routes, {
  useHash: false
});

export default router;
