import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import Webshop from "../shared/Webshop";

@Component({
  selector: "checkout-page",
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

  onCheckout() {
    this.webshop.errorMessage = "";
    this.webshop.checkout()
      .subscribe(() => {
        this.webshop.clearOrder();
        this.router.navigate(["/"]);
        alert("Order complete");
      }, () =>
        this.webshop.errorMessage = "Unable to complete checkout");
  }
}
