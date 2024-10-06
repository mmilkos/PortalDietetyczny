import { Component, OnInit } from '@angular/core';
import { ShopService } from '../../../services/shop.service';
import { CartProduct, CartSummaryRequest, CartSummaryResponse } from '../../../DTOs/CartSummaryResponse';
import {FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-shop-grid',
  templateUrl: './shop-grid.component.html',
  styleUrl: './shop-grid.component.css'
})
export class ShopGridComponent implements OnInit
{
  products: CartProduct[]
  total: number
  summary: boolean;
  invoice: boolean;
  orderForm: FormGroup;

  constructor(private shopService : ShopService)
  {
    this.summary = false;
    this.invoice = false;

    this.orderForm = new FormGroup(
      {
        email: new FormControl,
        invoice: new FormControl(false),
      })
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
}
