import { Component, Input } from '@angular/core';
import { CartProduct } from '../../../DTOs/CartSummaryResponse';
import { ShopService } from '../../../services/shop.service';

@Component({
  selector: 'app-cart-card',
  templateUrl: './cart-card.component.html',
  styleUrl: './cart-card.component.css'
})
export class CartCardComponent
{
  @Input() info: CartProduct
  @Input() isSummary: boolean

  constructor(private shopService: ShopService) {
  }

  delete()
  {
    this.shopService.removeFromIdsList(this.info.id)
  }

}
