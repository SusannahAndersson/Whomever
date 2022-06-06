import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from "@angular/common/http";

import { AppComponent } from './app.component';
import { Webshop } from './services/webshop.service';
import ProductListView from './views/productListView.component';

@NgModule({
  declarations: [
    AppComponent,
    ProductListView
  ],
  imports: [
    BrowserModule,
    HttpClientModule
  ],
  providers: [
    Webshop
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
