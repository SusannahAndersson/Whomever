import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import Webshop from "../services/webshop.service";

@Component({
  selector: "checkout-page",
  templateUrl: "checkoutPage.component.html",
  styleUrls: ['checkoutPage.component.css']
})
export default class CheckoutPage implements OnInit {
  constructor(public webshop: Webshop, private router: Router) {
  }

  public errorMessage = "";

  ngOnInit() {
    if (this.webshop.order.items.length === 0) {
      this.router.navigate(["/"]);
    }
  }

  onCheckout() {
    this.errorMessage = "";
    this.webshop.checkout()
      .subscribe(() => {
        this.webshop.clearOrder();
        this.router.navigate(["/"]);
        alert("Order complete");
      }, (error: any) => {
        console.log(error);
        this.errorMessage = `Unable to complete checkout ${error}`;
      })
  }
}
