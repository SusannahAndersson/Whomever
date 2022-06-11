import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { LoginAuth } from "./LoginAuth";
import { Order, OrderItem } from "./Order";
import { Product } from "./Product";

@Injectable()
export default class Webshop {
  //(exported)one productitem array for products
  products: Observable<Product[]>;
    //(exported)create new order
  order = new Order();
  //enabling user auth
  token = "";
  tokenExpiration = new Date();
  //exp display error msg
  errorMessage = "";

  constructor(private http: HttpClient) { }
    //httpget the seeded productlist from db
  loadProducts() {
    if (this.products) return new Observable();

    return this.http.get<Observable<Product[]>>("/api/products")
      .pipe(map(dbdata =>
        this.products = dbdata));
  }

    //add product to (cart) new order
  addToOrder(product: Product) {
    let item: OrderItem = this.order.items.find(item =>
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

  //(login props) create and store client side token
  login(props) {
    return this.http.post<LoginAuth>("/account/createtoken", props)
      .pipe(map(propsToken => {
        this.token = propsToken.token;
        this.tokenExpiration = propsToken.expiration;
      }));
  }

  //set and use header for token auth since client not store cookieyumyum
  checkout() {
    const headers = new HttpHeaders().set("Authorization", "Bearer " + this.token);
    return this.http.post("/api/orders", this.order, {
      headers: headers
    });
  }

  clearOrder() {
    this.order = new Order();
  }
}
