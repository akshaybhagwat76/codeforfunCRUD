import { Component, Inject } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { HttpClient, HttpHeaders } from '@angular/common/http';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  public declarativeFormCaptchaValue: string;
  public reactiveForm;
  url: string = '';
  
  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.reactiveForm = new FormGroup({
      recaptchaReactive: new FormControl(null, Validators.required)
    });
    this.url = baseUrl;
  }
  @Inject('BASE_URL') baseUrl: string;
  submit(captchaResponse: string): void {
    this.http.get(this.url + 'api/GoogleReCaptcha/ValidGoogleReCaptcha?recaptchaResponse=' + captchaResponse).subscribe(result => {
      alert(result);
    }, error => console.log(error));
  }
}
