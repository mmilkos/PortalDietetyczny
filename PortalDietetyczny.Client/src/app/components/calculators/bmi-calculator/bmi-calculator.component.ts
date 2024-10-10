import { Component, OnInit } from '@angular/core';
import {FormControl, FormGroup, Validators } from '@angular/forms';
import { filter } from 'rxjs';

@Component({
  selector: 'app-bmi-calculator',
  templateUrl: './bmi-calculator.component.html',
  styleUrl: './bmi-calculator.component.css'
})
export class BmiCalculatorComponent implements OnInit
{
  calculatedBmi : string = "";
  ngOnInit(): void
  {
    this.bmiForm.valueChanges
      .pipe(filter(() => this.bmiForm.valid))
      .subscribe(value => this.calculateBmi());
  }

  bmiForm = new FormGroup(
    {
      weight: new FormControl(0, Validators.required),
      height: new FormControl(0, Validators.required),
    })



  calculateBmi(): void
  {
    let weight = this.bmiForm.get("weight").value || 0

    let height = (this.bmiForm.get("height").value || 0) * 0.01

    var result = (weight) / (height * height)

    let fixed = result.toFixed(1)

    switch (true)
    {
      case (result < 18.5):
         this.calculatedBmi = fixed + " " + "Niedowaga"
        break;

      case (result < 25):
        this.calculatedBmi = fixed + " " + "Waga prawidłowa"
        break;

      case (result <= 29.9):
        this.calculatedBmi = fixed + " " + "Nadwaga"
        break;

      case (result > 29.9):
        this.calculatedBmi = fixed + " " + "Nadwaga"
        break;
    }
  }
}
