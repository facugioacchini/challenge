import { Component, OnInit } from '@angular/core';
import {FormControl, FormGroup, NgForm, Validators} from '@angular/forms';
import {
  MomentDateAdapter,
  MAT_MOMENT_DATE_ADAPTER_OPTIONS,
} from '@angular/material-moment-adapter';
import {DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE} from '@angular/material/core';
import { MY_DATE_FORMATS } from '../../consts/dateFormat';
import { UserService } from 'src/app/services/user.service';
import { User } from 'src/app/models/user';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
  providers: [
    {
      provide: DateAdapter,
      useClass: MomentDateAdapter,
      deps: [MAT_DATE_LOCALE, MAT_MOMENT_DATE_ADAPTER_OPTIONS],
    },
    {provide: MAT_DATE_FORMATS, useValue: MY_DATE_FORMATS},
  ],
})

export class HomeComponent implements OnInit {

  userForm: FormGroup = new FormGroup({
  nameFormControl: new FormControl('', [Validators.required, Validators.maxLength(50), Validators.pattern("^[a-zA-Z ]*$")]),
  lastNameFormControl: new FormControl('', [Validators.required, Validators.maxLength(50), Validators.pattern("^[a-zA-Z ]*$")]),
  emailFormControl: new FormControl('', [Validators.required, Validators.email]),
  dateFormControl: new FormControl('', [Validators.required]),
  phoneFormControl: new FormControl('', [Validators.minLength(8), Validators.pattern("^[0-9]*$")]),
  countryFormControl: new FormControl('', [Validators.required]),
  infoFormControl: new FormControl(false)});

  dateFilter = (d: Date | null): boolean => {
    const today = new Date();
    if (!d) {
      return false;
    }
    return (d<=today);
  };

  constructor(private UserService: UserService) { }

  ngOnInit(): void {
  }

  saveUser() {
    let usuario = new User(
      this.userForm.controls['nameFormControl'].value, 
      this.userForm.controls['lastNameFormControl'].value, 
      this.userForm.controls['emailFormControl'].value, 
      this.userForm.controls['dateFormControl'].value, 
      this.userForm.controls['phoneFormControl'].value, 
      this.userForm.controls['countryFormControl'].value,
      this.userForm.controls['infoFormControl'].value,
    )
    this.UserService.createUser(usuario).subscribe((result: any) => {
      console.log(result.body.statusCode)
    });
    
  }

}
