import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  // @Input() valuesFromHome: any; // this is how you communicate from parent component to child
  @Output() cancelRegister = new EventEmitter(); // this is how you communicate from child component to parent (as an EventEmitter)
  model: any = {};

  constructor(private auithService: AuthService) { }

  ngOnInit() {
  }

  register() {
    this.auithService.register(this.model).subscribe(() => {
      console.log('registered!');
    },
    error => {
      console.log(error);
    });
  }

  cancel() {
    this.cancelRegister.emit(false);
    console.log('canceled');
  }
}
