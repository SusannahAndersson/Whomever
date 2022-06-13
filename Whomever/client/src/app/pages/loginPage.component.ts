import { Component } from "@angular/core";
import { Router } from "@angular/router";
import { LoginCreds } from "../shared/User";
import Webshop from "../services/webshop.service";
///*import { FormsModule } from "@angular/forms";*/

@Component({
  selector: "login-page",
  templateUrl: "loginPage.component.html",
  styleUrls: ["loginPage.component.css"]
})

export default class LoginPage {
  constructor(private webshop: Webshop, private router: Router) {
  }
  //public fm = FormsModule
  //errormessage when login fails
  public errorMessage = "";

  //empty property string for loginpage
  public creds: LoginCreds = {
    username: "",
    password: ""
  };

  //subscribe to user successfully login via accountcontroller httppost
  onLogin() {
    this.webshop.login(this.creds)
      .subscribe(() => {
        //if successfull --> route auth user to checkoutpage if cartitems>0
        if (this.webshop.order.items.length > 0) {
          this.router.navigate(["/checkout"]);
        }
        //if failed then navigates back
        else {
          this.router.navigate(["/"]);
        }
        ////log error
      }, (error: any) => {
        console.log(error);
        this.errorMessage = `Wrong email or password, try again ${error}`;
      });
  }
}
