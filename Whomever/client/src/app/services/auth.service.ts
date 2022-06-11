import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from "@angular/router";
import { Observable } from "rxjs";
import Webshop from "./webshop.service";
import { FormsModule } from '@angular/forms';

@Injectable()
export default class Auth implements CanActivate {
  constructor(public webshop: Webshop, public router: Router, public ng = FormsModule) {
  }
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {
    if (this.webshop.loginUser) {
      this.router.navigate(["login"]);
      return false;
    }
    else {
      return true;
    }
  }
}
