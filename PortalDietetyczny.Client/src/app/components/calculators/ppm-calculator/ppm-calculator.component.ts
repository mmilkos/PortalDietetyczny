import { Component } from '@angular/core';
import {FormControl, FormGroup, Validators } from '@angular/forms';
import { filter } from 'rxjs';

@Component({
  selector: 'app-ppm-calculator',
  templateUrl: './ppm-calculator.component.html',
  styleUrl: './ppm-calculator.component.css'
})
export class PpmCalculatorComponent
{
  calculatedPpm : string = "";
  ngOnInit(): void
  {
    this.ppmForm.valueChanges
      .pipe(filter(() => this.ppmForm.valid))
      .subscribe(value => this.calculatePpm());
  }

  ppmForm = new FormGroup(
    {
      weight: new FormControl(0, Validators.required),
      height: new FormControl(0, Validators.required),
      age: new FormControl(0, Validators.required),
      gender: new FormControl(0, Validators.required),
    })



  calculatePpm(): number
  {
    let weight = this.ppmForm.get("weight").value || 0
    let age = this.ppmForm.get("age").value || 0
    let gender = this.ppmForm.get("gender").value || 0
    let height = (this.ppmForm.get("height").value || 0) * 0.01

    let result = 0;

    console.log(gender)


    if (gender == 0)
    {
      result =  655.1 + (9.563 * weight) + (1.85 * height) - (4.674 * age)
      this.calculatedPpm = result.toFixed(1)
      return result;
    }
    else
    {
      result = 66.5 + (13.75 * weight) + (5.003 * height) - (6.775 * age);
      this.calculatedPpm = result.toFixed(1)
      return result
    }
  }

}
