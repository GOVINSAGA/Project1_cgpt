import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './login.component.html'
})
export class LoginComponent {

  loginData = {
    userNameOrEmail: '',
    password: ''
  };

  constructor(private authService: AuthService, private router: Router) { }

  onSubmit() {

    this.authService.login(this.loginData).subscribe({

      next: (response: any) => {

        const token = response.data.token;

        this.authService.saveToken(token);

        alert("Login Successful");

        this.router.navigate(['/profile']);
      },

      error: (error: any) => {
        alert("Invalid credentials");
        console.error(error);
      }

    });

  }

}
