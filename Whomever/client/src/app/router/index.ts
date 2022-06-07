//import { Router, RouterModule } from "@angular/router";
import { RouterModule } from "@angular/router";
import CheckoutPage from "../pages/checkoutPage.component";
import { WebshopPage } from "../pages/webshopPage.component";

const routes = [
  { path: "", component: WebshopPage },
  { path: "checkout", component: CheckoutPage }
];
//creates routes
const Router = RouterModule.forRoot(routes, { useHash: false });
//exports
export default Router;
