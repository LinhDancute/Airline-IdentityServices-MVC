import {Component, inject} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {FormsModule} from "@angular/forms";
import {Router, RouterLink} from "@angular/router";

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    FormsModule,
    RouterLink
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  loginObj:any = {
    "email": "",
    "password": ""
  };

  http = inject(HttpClient)
  constructor(private router: Router) {

  }
  onLogin() {
    debugger;
    this.http.post("https://localhost:7002/api/Auth/login", this.loginObj).subscribe((response: any) => {
      if (response.flag) {
        alert("Đăng nhập thành công");
        this.router.navigateByUrl("main");
      } else {
        alert("Sai email hoặc mật khẩu");
      }
    });
  }

}
