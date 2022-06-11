import { Component } from "@angular/core";
import { Router } from "@angular/router";
import Webshop from "../shared/Webshop";
/*import { FormsModule } from "@angular/forms";*/

@Component({
  selector: "login-page",
  templateUrl: "loginPage.component.html",
  styleUrls: ["loginPage.component.css"]
})

export default class LoginPage {
  constructor(private webshop: Webshop, private router: Router) { }

  //public n = FormsModule

  //empty property string for loginpage
  public props = {
    username: "",
    password: ""
  };

  //errormessage when login fails
  public errorMessage = "";

  //subscribe to user successfully login via accountcontroller httppost
  onLogin() {
    this.webshop.login(this.props).subscribe(() => {
        //if successfull --> route auth user to checkoutpage
      if (this.webshop.order.items.length > 0) {
        this.router.navigate(["/checkout"]);
      }
      //if failed then navigates back
      else {
        this.router.navigate(["/"]);
      }
        ////log error to console
}, error => {
  console.log(error);
      this.errorMessage = "Wrong email or password, try again";
    });
  }
}
