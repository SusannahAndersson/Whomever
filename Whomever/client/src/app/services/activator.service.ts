import { Injectable } from "@angular/core";
import { CanActivate, Router } from "@angular/router";
import Webshop from "./webshop.service";

@Injectable()
export default class Activator implements CanActivate {
  constructor(private webshop: Webshop, private router: Router) {
  }

  //authservice guard
  canActivate(route, state): boolean {
    if (this.webshop.loginRequired) {
      this.router.navigate(["/login"]);
      return false;
    }
    else {
      return true;
    }
  }
}
