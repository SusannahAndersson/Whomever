import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { Order, OrderItem } from "../shared/Order";
import { Product } from "../shared/Product";

@Injectable()

export class Webshop {
  constructor(public http: HttpClient) {
  }
  //(exported)one productitem array for products
  public products: Product[] = [];
  //(exported)create new order
  public order: Order = new Order();
  //httpget the seeded productlist from db
  loadProducts(): Observable<void> {
    return this.http.get<[]>("/api/products")
      .pipe(map(data => {
        this.products = data;
        return;
      }))
  }
  //add product to new order
  addToOrder(product: Product) {
    //exported Order.ts orderitem []
    let item: OrderItem;
    item = this.order.items.find(i => i.productId === product.id);
    if (item) {
      item.quantity++;
    }
    else {
      const item = new OrderItem(); {
        item.productId = product.id;
        item.quantity = 1;
        item.unitPrice = product.price;
        item.productProductId = product.productId;
        item.productCategory = product.category;
        item.productTitle = product.title;
        item.productSize = product.size;
        item.productBrand = product.brand;
        this.order.items.push(item);
      }
    }
  }
}
