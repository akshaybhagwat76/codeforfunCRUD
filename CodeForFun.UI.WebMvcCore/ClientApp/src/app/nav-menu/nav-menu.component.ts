import { Component, OnInit } from '@angular/core';
import { AuthService } from 'angularx-social-login';
import { AccountService } from '../services/account.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
  isExpanded = false;
  user: any = {}
  isLogged = false;
  isLoggedBySocialMedia;
  loginForm = false;
  registerForm = false;
  token:any;
  constructor(private authService: AuthService, private accountService: AccountService) { 
    this.checkAuth();
  }

  ngOnInit(): void {
    this.checkAuth();
    this.checkLogin();
  }

  collapse() {
    this.isExpanded = false;
  }

  checkLogin() {
    console.log(2)
    this.authService.authState.subscribe(x => {
      if (x) {
        this.isLogged = true;
        this.isLoggedBySocialMedia = true;
        this.user.name = x.name
      }
      else {
        this.isLogged = false
      }
    })
  }
  checkAuth() {
    console.log(1)
    this.accountService.isLogged().subscribe(x => {
      if (x) {
        console.log(x)
        this.user.name = x.userName;
        this.isLogged = true;

      }
      else {
        this.checkLogin();
        this.isLogged = false;
      }
    })
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  logged($event) {

    this.user.name = $event.surname == undefined ? $event.email: $event.name + " " + $event.surname
    this.isLogged = true;
  }

  logout() {
    this.accountService.logOut().subscribe(x => {
      this.isLogged = false;
    })

    if (this.isLoggedBySocialMedia) {
      this.authService.signOut(true);
      this.isLoggedBySocialMedia = false;
    }

  }

}
