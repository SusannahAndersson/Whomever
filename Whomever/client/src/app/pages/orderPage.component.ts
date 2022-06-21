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
  orderId: number;

  constructor(public webshop: Webshop, private router: Router) {
  }
  async ngOnInit() {
    this.router.navigate(["/order"]);
    this.webshop.loadOrder()
      .subscribe();
  }

  public errorMessage = "";

  onDeleteOrder(orderId) {
    this.errorMessage = "";
    this.webshop.deleteOrder(orderId)
      .subscribe(() => {
        this.webshop.clearOrder();
        this.router.navigate(["/"]);
        alert("Order deleted");
      }, (error: any) => {
        console.log(error);
        this.errorMessage = `Unable to delete order ${error}`;
      })
  }

  onCancelOrder(orderId) {
    this.errorMessage = "";
    this.webshop.cancelOrder(orderId)
      .subscribe(() => {
        this.webshop.clearOrder();
        this.router.navigate(["/"]);
        alert("Order canceled");
      }, (error: any) => {
        console.log(error);
        this.errorMessage = `Unable to cancel order ${error}`;
      })
  }
}
