import { Component } from '@angular/core';
import {FormControl, FormGroup, Validators } from '@angular/forms';
import { filter } from 'rxjs';

@Component({
  selector: 'app-nmc-calculator',
  templateUrl: './nmc-calculator.component.html',
  styleUrl: './nmc-calculator.component.css'
})
export class NmcCalculatorComponent
{
  calculatedNmc : string = "";
  ngOnInit(): void
  {
    this.nmcForm.valueChanges
      .pipe(filter(() => this.nmcForm.valid))
      .subscribe(value => this.calculateNmc());
  }

  nmcForm = new FormGroup(
    {
      height: new FormControl(0, Validators.required),
      gender: new FormControl(0, Validators.required),
    })



  calculateNmc(): number
  {
    let gender = this.nmcForm.get("gender").value || 0
    let height = (this.nmcForm.get("height").value || 0)

    let result = 0;

    console.log(gender)


    if (gender == 0)
    {
      result = height - 100 - ((height- 150) / 2)
      this.calculatedNmc = result.toFixed()
      return result

    }
    else
    {
      result = height - 100 -((height - 150)/ 4)
      this.calculatedNmc = result.toFixed()
      return result
    }
  }

}
