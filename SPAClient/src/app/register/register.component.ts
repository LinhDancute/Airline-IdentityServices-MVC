import {Component, inject} from '@angular/core';
import {FormsModule} from "@angular/forms";
import {Router, RouterLink} from "@angular/router";
import {HttpClient, HttpHeaders} from "@angular/common/http";

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [
    FormsModule,
    RouterLink
  ],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  registerObj: any = {
    username: "",
    email: "",
    password: "",
    password_confirmation: ""
  };

  http = inject(HttpClient);
  constructor(private router: Router) {}

  onRegister() {
    this.http.post("https://localhost:7002/api/Auth/register/member", this.registerObj).subscribe((response: any) => {
      if (response.token) {
        alert("Đăng kí thành công");
        this.router.navigateByUrl('/login');
      } else {
        alert("Đăng kí thất bại");
      }
    });
  }
}
