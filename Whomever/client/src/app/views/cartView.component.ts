import { Component } from "@angular/core";
import { Webshop } from "../services/webshop.service";

@Component({
  selector: "cart",
  templateUrl: "cartView.component.html",
  styleUrls: ["cartView.component.css"]
})
export class CartView {
  constructor(public webShop: Webshop) {
  }
}
