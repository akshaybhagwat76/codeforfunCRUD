import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { RecaptchaModule, RecaptchaFormsModule } from 'ng-recaptcha';
import { RECAPTCHA_V3_SITE_KEY, RecaptchaV3Module } from 'ng-recaptcha';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { ProductsComponent } from './products/products.component';
import { CategoryComponent } from './category/category.component';
import { CategoryService } from './services/category.service';
import { ProductService } from './services/product.service';
import { SocialLoginModule, AuthServiceConfig } from "angularx-social-login";
import { GoogleLoginProvider, FacebookLoginProvider } from "angularx-social-login";
import { LoginComponent } from './auth/login/login.component';
import { RegisterComponent } from './auth/register/register.component';
import { AccountService } from './services/account.service';
import { OrdersComponent } from './orders/orders.component';
import { OrderService } from './services/order.service';


let config = new AuthServiceConfig([
  {
    id: GoogleLoginProvider.PROVIDER_ID,
    provider: new GoogleLoginProvider("735335157823-lqpobanqtlfh05sb2fje2ltskp906nqs.apps.googleusercontent.com")
  },
  {
    id: FacebookLoginProvider.PROVIDER_ID,
    provider: new FacebookLoginProvider("3001727586546685")
  }
]);

export function provideConfig() {
  return config;
}

@NgModule({
   declarations: [
      AppComponent,
      NavMenuComponent,
      HomeComponent,
      CounterComponent,
      FetchDataComponent,
      ProductsComponent,
      CategoryComponent,
      OrdersComponent,
      LoginComponent,
      RegisterComponent,
      OrdersComponent
   ],
   imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RecaptchaModule,
    RecaptchaFormsModule,
    SocialLoginModule,
    RecaptchaV3Module,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'products', component: ProductsComponent}
    ])
  ],
  providers: [
    // { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true },
    // { provide: RECAPTCHA_V3_SITE_KEY, useValue: '6LdjaOAUAAAAADxWsshY0Oq5nJNv744g36l6fwnj' },
    {
      provide: AuthServiceConfig,
      useFactory: provideConfig
    },
    CategoryService,
    ProductService,
    AccountService,
    OrderService
    
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
