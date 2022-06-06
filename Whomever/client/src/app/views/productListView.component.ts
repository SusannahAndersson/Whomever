import { Component, OnInit } from "@angular/core";
import { Webshop } from "../services/webshop.service";

@Component({
  selector: "product-list",
  templateUrl: "productListView.component.html",
  styleUrls: ["productListView.component.css"]
})
export default class ProductListView implements OnInit {
  constructor(public webShop: Webshop) {
  }
  ngOnInit(): void {
    this.webShop.loadProducts()
      //starts loadproducts from webshop.service
      .subscribe(() => {
      });
  }
}