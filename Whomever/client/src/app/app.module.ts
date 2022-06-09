import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from "@angular/common/http";
import  AppComponent  from './app.component';
import Webshop  from './services/webshop.service';
import ProductListView from './views/productListView.component';
import CartView  from './views/cartView.component';
import  WebshopPage  from './pages/webshopPage.component';
import CheckoutPage from './pages/checkoutPage.component';
import Router from './router';

@NgModule({
  declarations: [
    AppComponent,
    ProductListView,
    CartView,
    WebshopPage,
    CheckoutPage
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    Router

  ],
  providers: [
    Webshop
  ],
  bootstrap: [AppComponent]
})
export default class AppModule { }
