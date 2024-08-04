import { Component } from '@angular/core';
import {FormControl, FormGroup, Validators } from '@angular/forms';
import { filter } from 'rxjs';

@Component({
  selector: 'app-cpm-calculator',
  templateUrl: './cpm-calculator.component.html',
  styleUrl: './cpm-calculator.component.css'
})
export class CpmCalculatorComponent
{
  calculatedCpm : string = "";
  ngOnInit(): void
  {
    this.cpmForm.valueChanges
      .pipe(filter(() => this.cpmForm.valid))
      .subscribe(value => this.calculateCpm());
  }

  cpmForm = new FormGroup(
    {
      weight: new FormControl(0, Validators.required),
      height: new FormControl(0, Validators.required),
      age: new FormControl(0, Validators.required),
      gender: new FormControl(0, Validators.required),
      activity: new FormControl(0, Validators.required),
    })



  calculateCpm(): number
  {
    let weight = this.cpmForm.get("weight").value || 0
    let age = this.cpmForm.get("age").value || 0
    let gender = this.cpmForm.get("gender").value || 0
    let height = (this.cpmForm.get("height").value || 0) * 0.01
    let activity = this.cpmForm.get("activity").value || 0

    let result = 0;


    if (gender == 0)
    {
      result =  (655.1 + (9.563 * weight) + (1.85 * height) - (4.674 * age)) * activity
      this.calculatedCpm = result.toFixed(1)
      return result;
    }
    else
    {
      result = 66.5 + (13.75 * weight) + (5.003 * height) - (6.775 * age) * activity;
      this.calculatedCpm = result.toFixed(1)
      return result
    }
  }
}
