import { Component } from '@angular/core';
import Webshop from './services/webshop.service';

@Component({
  selector: 'webshop',
  templateUrl: "app.component.html",
  styleUrls: []
})
export default class AppComponent {
  title = 'Webshop';

  constructor(public webshop: Webshop) { }
}
