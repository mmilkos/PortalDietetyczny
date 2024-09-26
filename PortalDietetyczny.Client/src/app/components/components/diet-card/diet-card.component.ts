import { Component, Input, OnInit } from '@angular/core';
import { DietPreviewDto } from '../../../DTOs/DietPreviewDto';
import { IdAndName } from '../../../DTOs/IdAndName';
import { DietsService } from '../../../services/diets.service';
import { DietsPreviewPageRequest } from '../../../DTOs/DietsPreviewPageRequest';
import { PagedResult } from '../../../models/PagedResult';
import { ShopService } from '../../../services/shop.service';

@Component({
  selector: 'app-diet-card',
  templateUrl: './diet-card.component.html',
  styleUrl: './diet-card.component.css'
})
export class DietCardComponent
{
  @Input() info: DietPreviewDto;

  constructor(private shopService: ShopService) {}

  addToCart()
  {
    console.log(this.info.id)
    this.shopService.addToIdsList(this.info.id);
  }
}
