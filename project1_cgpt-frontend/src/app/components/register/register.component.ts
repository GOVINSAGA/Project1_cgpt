import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {

  user = {
    name: '',
    userName: '',
    password: '',
    mobileNo: '',
    dob: '',
    email: '',
    address: ''
  };

  constructor(private authService: AuthService) { }

  register() {

    this.authService.register(this.user).subscribe({
      next: (res: any) => {
        alert("User registered successfully");
        console.log(res);
      },
      error: (err: any) => {
        alert("Registration failed");
        console.error(err);
      }
    });

  }

}
