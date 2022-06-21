import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, pipe } from "rxjs";
import { map } from "rxjs/operators";
import { LoginAuth, LoginCreds } from "../shared/User";
import { Order, OrderItem } from "../shared/Order";
import { Product } from "../shared/Product";

@Injectable()
export default class Webshop {
  url: string;
  constructor(private http: HttpClient) {
    this.url = "http://localhost:5500/api/orders";
  }

  //(exported)one productitem array for products
  products: Observable<Product[]>;
  //(exported)create new order
  order = new Order();
  //enabling user auth
  token = "";
  tokenExpiration = new Date();
  //exp display error msg
  errorMessage = "";
  orders: Observable<Order[]>;
  orderId: number;

  //httpget the seeded productlist from db
  loadProducts() {
    if (this.products) return new Observable();

    return this.http.get<Observable<Product[]>>("/api/products")
      .pipe(map(dbdata =>
        this.products = dbdata));
  }

  //add product to (cart) new order
  addToOrder(product: Product) {
    let item: OrderItem = this.order.items.find((item) =>
      item.productId === product.id);

    if (item) {
      item.quantity++;
    }
    else {
      item = new OrderItem();
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

  //to auth guard
  get loginRequired(): boolean {
    return this.token?.length === 0 || this.tokenExpiration > new Date();
  }

  //(login signCreds) create and store client side token
  login(creds: LoginCreds) {
    return this.http.post<LoginAuth>("/account/createtoken", creds)
      .pipe(map(request => {
        this.token = request.token;
        this.tokenExpiration = request.expiration;
      }));
  }

  clearOrder() {
    this.order = new Order();
  }

  //set and use header for token auth since client not store cookieyumyum
  checkout() {
    const headers = new HttpHeaders().set("Authorization", "Bearer " + this.token);
    return this.http.post("/api/orders", this.order, {
      headers: headers
    })
      .pipe(map(() => {
        this.order = new Order();
      }));
  }

  loadOrder() {
    const headers = new HttpHeaders().set("Authorization", "Bearer " + this.token);
    if (this.orders) return new Observable();

    return this.http.get<Observable<Order[]>>(this.url, {
      headers: headers
    })
      .pipe(map(dbdata =>
        this.orders = dbdata
      ));
  }

  public deleteOrder(orderId: number) {
    //let orderId = this.order.orderId;
    const headers = new HttpHeaders().set("Authorization", "Bearer " + this.token);
    return this.http.delete(this.url + "/" + orderId, {
      headers: headers
    })
      .pipe(map(() => {
        console.log();
        this.orderId = this.order.orderId;
        this.order = new Order();
      }));
  }

  public cancelOrder(orderId: number) {
    //let orderId = this.order.orderId;
    const headers = new HttpHeaders().set("Authorization", "Bearer " + this.token);
    return this.http.put(this.url + "/" + orderId, {
      headers: headers
    })
      .pipe(map(() => {
        console.log();
        this.orderId = this.order.orderId;
        this.order = new Order();

    ////let orderId = this.order.orderId;
    //const headers = new HttpHeaders().set("Authorization", "Bearer " + this.token);
    //return this.http.put(this.url + "/" + orderId, {
    //  headers: headers
    //})
    //  .pipe(map(() => {
    //    console.log();
    //    this.orderId = this.order.orderId;
    //    this.order = new Order();
      }));
  }
}
