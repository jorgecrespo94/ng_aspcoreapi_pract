import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};
  localUsername: any = '';

  constructor(private authService: AuthService) { }

  ngOnInit() {
    const username = localStorage.getItem('username');
    if (username !== null && username !== 'undefined') {
      this.localUsername = username;
    } else {
      this.localUsername = 'User';
    }
  }

  login() {
    this.authService.login(this.model).subscribe(next => {
      this.localUsername = this.model.username;
    }, error => {
      console.log('login error!');
    });

  }

  loggedIn() {
      const token = localStorage.getItem('token');
      return !!token;
  }

  logout() {
    localStorage.removeItem('token');
  }

}
