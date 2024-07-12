import {Component, inject} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {FormsModule} from "@angular/forms";
import {Router} from "@angular/router";

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    FormsModule
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  loginObj:any = {
    "Email": "",
    "Password": ""
  };

  http = inject(HttpClient)
  constructor(private router: Router) {

  }
  onLogin(){
    debugger;
    this.http.post("http://localhost:7002", this.loginObj).subscribe((response:any)=>{
      if (response.result){
        this.loginObj = response;
        alert("Đăng nhập thành công");
        this.router.navigateByUrl("pageContent");
      } else {
        alert("Sai email hoặc mật khẩu");
      }
    })
  }
}
