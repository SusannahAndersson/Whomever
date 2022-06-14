import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import Webshop from "../services/webshop.service";

@Component({
  selector: "order-page",
  templateUrl: "orderPage.component.html",
  styleUrls: ["orderPage.component.css"]
})

export default class OrderPage implements OnInit {
  public title = "Orders"
  constructor(public webshop: Webshop, private router: Router) {
  }
  public errorMessage = "";

  async ngOnInit() {
    this.router.navigate(["/order"]);
    this.webshop.loadOrder()
      //this.webshop.loadOrderItem()
      .subscribe();
  }
}
