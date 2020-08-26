import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-test-error',
  templateUrl: './test-error.component.html',
  styleUrls: ['./test-error.component.scss']
})
export class TestErrorComponent implements OnInit {
  // add setting in environment, export development url (localhost:5001)
  baseUrl = environment.apiUrl;
  validationErrors: any;
  
  constructor(private http: HttpClient) { }
  ngOnInit(): void {
  }
  // tslint:disable-next-line:typedef
  get404Error(){
    this.http.get(this.baseUrl + 'buggy/notfound').subscribe(response => {
      console.log(response);
    }, error => {
      console.log(error);
    });
  }
  // tslint:disable-next-line:typedef
  get500Error(){
    this.http.get(this.baseUrl + 'buggy/servererror').subscribe(response => {
      console.log(response);
    }, error => {
      console.log(error);
    });
  }
  // tslint:disable-next-line:typedef
  get400Error(){
    this.http.get(this.baseUrl + 'buggy/badrequest').subscribe(response => {
      console.log(response);
    }, error => {
      console.log(error);
    });
  }
  // tslint:disable-next-line:typedef
  get400validationError(){
    this.http.get(this.baseUrl + 'products/fortytwo').subscribe(response => {
      console.log(response);
    }, error => {
      console.log(error);
      this.validationErrors = error.errors;
    });
}
}
