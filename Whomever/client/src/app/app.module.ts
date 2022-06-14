import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { HttpClientModule } from "@angular/common/http";
import { AppComponent } from "./app.component";
import { ProductListView } from "./views/productListView.component";
import CartView from "./views/cartView.component";
import CheckoutPage from "./pages/checkoutPage.component";
import WebshopPage from "./pages/webshopPage.component";
import LoginPage from "./pages/loginPage.component";
import Webshop from "./services/webshop.service";
import router from "./services/router.service";
import Activator from "./services/activator.service";
import { FormsModule } from "@angular/forms";
import OrderPage from "./pages/orderPage.component";

@NgModule({
  declarations: [
    AppComponent,
    ProductListView,
    CartView,
    WebshopPage,
    CheckoutPage,
    LoginPage,
    OrderPage
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    router
  ],
  providers: [
    Webshop,
    Activator
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
