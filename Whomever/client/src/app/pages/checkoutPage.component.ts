import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import Webshop  from "../services/webshop.service";


@Component({
  templateUrl: "checkoutPage.component.html",
  styleUrls: ['checkoutPage.component.css']
})
export default class CheckoutPage implements OnInit {
  constructor(public webshop: Webshop, private router: Router) {
  }

  ngOnInit() {
    if (this.webshop.order.items.length === 0) {
      this.router.navigate(["/"]);
    }
  }
}
