import { Component, OnInit } from '@angular/core';
import { ShopService } from '../../../services/shop.service';
import { CartProduct, CartSummaryRequest, CartSummaryResponse } from '../../../DTOs/CartSummaryResponse';
import {FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { RoutesEnum } from '../../../enums/RoutesEnum';
import { OrderDto } from '../../../DTOs/OrderDto';

@Component({
  selector: 'app-shop-grid',
  templateUrl: './shop-grid.component.html',
  styleUrl: './shop-grid.component.css'
})
export class ShopGridComponent implements OnInit
{
  public routes  = RoutesEnum;
  products: CartProduct[]
  total: number
  summary: boolean;
  invoice: boolean;
  orderForm: FormGroup;
  isValid: boolean = false

  constructor(private shopService : ShopService,
              private router: Router,
              private fb: FormBuilder)
  {
    this.summary = false;
    this.invoice = false;

    this.orderForm = this.fb.group({
      email: ['', Validators.required],
      consent: [false, Validators.requiredTrue],
      invoice: [false],
      name: [''],
      lastName: [''],
      street: [''],
      city: ['']
    });
  }

  ngOnInit(): void
  {
    this.getCartSummary()
  }

  getCartSummary()
  {
    const ids = this.shopService.getIdsList()
    const dto: CartSummaryRequest =
      {
        productsIds: ids
      }

    this.shopService.getCartSummary(dto).subscribe(
      (response : CartSummaryResponse)=>
      {
        this.products = response.products;
        this.total = response.total / 100;
      },
      (error)=> console.log(error.error)
    )
  }

  toggleSummary()
  {
    this.summary = !this.summary
  }

  toggleInvoice()
  {
    this.invoice = !this.invoice
  }

  navigateTo(url : string)
  {
    this.router.navigate([url])
  }

  submit(): number
  {

    if (this.invoice)
    {
      let valid: boolean = this.orderForm.get("name").value != ""
        && this.orderForm.get("lastName").value  != ""
        && this.orderForm.get("street").value  != ""
        && this.orderForm.get("city").value  != ""

      if (!valid)
      {
        console.log("Proszę wypełnić wszystkie pola")
        return 0
      }
    }

    var orderDto: OrderDto =
      {
        customerEmail: this.orderForm.get("email").value,
        productsIds: this.products.map(p => p.id),
        invoiceDto: this.invoice ?
          {
            name: this.orderForm.get("name").value,
            lastName: this.orderForm.get("lastName").value,
            street: this.orderForm.get("street").value,
            city: this.orderForm.get("city").value
          } : null,
      }

    console.log(orderDto)
    this.shopService.sendOrder(orderDto).subscribe(
      respose=> console.log('działa'),
        (error) => console.log(error))

    return 1
  }
}
