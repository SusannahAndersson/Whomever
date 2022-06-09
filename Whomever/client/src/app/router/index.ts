import { RouterModule } from "@angular/router";
import WebshopPage from "../pages/webshopPage.component";
import CheckoutPage from "../pages/checkoutPage.component";
import LoginPage from "../pages/loginPage.component";
import Auth from "../services/auth.service";


const routes = [
  { path: "", component: WebshopPage },
  { path: "checkout", component: CheckoutPage, canActivate: [Auth] },
  { path: "login", component: LoginPage },
  { path: "**", redirectTo: "/" }
];
//creates routes
const Router = RouterModule.forRoot(routes, { useHash: false });
//exports
export default Router;
