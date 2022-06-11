import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { HttpClientModule } from "@angular/common/http";
import { AppComponent } from "./app.component";
import { ProductListView } from "./views/productListView.component";
import CartView from "./views/cartView.component";
import CheckoutPage from "./pages/checkoutPage.component";
import WebshopPage from "./pages/webshopPage.component";
import LoginPage from "./pages/loginPage.component";
import Webshop from "./shared/Webshop";
import router from "./services/router.services";
import Auth from "./services/auth.service";
import { FormsModule } from "@angular/forms";

@NgModule({
  declarations: [
    AppComponent,
    ProductListView,
    CartView,
    WebshopPage,
    CheckoutPage,
    LoginPage
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    router
  ],
  providers: [
    Webshop,
    Auth
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
