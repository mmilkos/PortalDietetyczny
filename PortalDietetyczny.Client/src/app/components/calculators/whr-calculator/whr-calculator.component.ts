import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { filter } from 'rxjs';

@Component({
  selector: 'app-whr-calculator',
  templateUrl: './whr-calculator.component.html',
  styleUrl: './whr-calculator.component.css'
})
export class WhrCalculatorComponent
{
  calculatedWhr : string = "";
  ngOnInit(): void
  {
    this.whrForm.valueChanges
      .pipe(filter(() => this.whrForm.valid))
      .subscribe(value => this.calculateWhr());
  }

  whrForm = new FormGroup(
    {
      waist: new FormControl(0, Validators.required),
      hips: new FormControl(0, Validators.required),
    })



  calculateWhr(): number
  {
    let waist = this.whrForm.get("waist").value || 0

    let height = (this.whrForm.get("hips").value || 0) * 0.01

    var result = waist / height

    this.calculatedWhr = result.toFixed(1)

    return result
  }

}
