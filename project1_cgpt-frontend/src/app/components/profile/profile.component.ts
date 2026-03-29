import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent implements OnInit {

  user: any;

  constructor(private authService: AuthService) { }

  ngOnInit(): void {

    this.authService.getProfile().subscribe({
      next: (res: any) => {
        console.log("PROFILE RESPONSE:", res);
        this.user = res.data;
      },
      error: (err) => {
        console.error(err);
      }
    });

  }

}
