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

  constructor(private authService: AuthService, private accountService: AccountService) { }

  ngOnInit(): void {
    debugger
    this.checkAuth();

  }

  collapse() {
    debugger
    this.isExpanded = false;
  }

  checkLogin() {
    debugger
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
    debugger
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
    debugger
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
