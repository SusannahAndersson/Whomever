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
  //orderId: Order;
  //orders: any[];
  constructor(public webshop: Webshop, private router: Router) {
  }
  async ngOnInit() {
    this.router.navigate(["/order"]);
    this.webshop.loadOrder()
      //this.webshop.loadOrderItem()
      .subscribe();
  }

  //public orderId: Order = {
  //    orderId: "",
  //    orderDate: new Date(),
  //    orderNumber: "",
  //    items: [],
  //    total: 0
  //};

  public errorMessage = "";

  onDeleteOrder() {
    this.errorMessage = "";
    this.webshop.deleteOrder(this.orderId)
      .subscribe(() => {
        this.webshop.clearOrder();
        this.router.navigate(["/"]);
        alert("Order deleted");
      }, (error: any) => {
        console.log(error);
        this.errorMessage = `Unable to delete order ${error}`;
      })
  }
}
