import { Component} from "@angular/core";
import { Router } from "@angular/router";
import Webshop from "../services/webshop.service";

/*import { FormsModule } from "@angular/forms";*/

@Component({
  selector: "login-page",
  templateUrl: "loginPage.component.html",
  styleUrls: ["loginPage.component.css"]
})

export default class LoginPage {
  constructor(public webshop: Webshop, public router: Router) { }

  //public n = FormsModule

  public loginUser = {
    email: "",
    password: ""
  };

  onLogin() {
    alert("Logging in");
  }
}
