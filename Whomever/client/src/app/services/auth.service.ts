import { Injectable } from "@angular/core";
import { CanActivate, Router } from "@angular/router";
import Webshop from "../shared/Webshop";

@Injectable()
export default class Auth implements CanActivate {
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
