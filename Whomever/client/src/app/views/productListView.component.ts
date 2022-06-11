import { Component, OnInit } from "@angular/core";
import Webshop from "../shared/Webshop";

@Component({
  selector: "product-list",
  templateUrl: "productListView.component.html",
  styleUrls: ["productListView.component.css"]
})
export class ProductListView implements OnInit {
  constructor(public webshop: Webshop) {
  }

  //async loadproducts from webshop.service
  async ngOnInit() {
    this.webshop.loadProducts()
      .subscribe();
  }
}
