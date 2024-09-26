import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import {CartSummaryRequest, CartSummaryResponse} from '../DTOs/CartSummaryResponse';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  apiUrl: string = "https://localhost:44317/api/shop/"
  productsIds : number[] = []
  constructor(private http : HttpClient)
  {
    sessionStorage.setItem('productIds', JSON.stringify(this.productsIds));
  }

  getCartSummary(ids: CartSummaryRequest): Observable<CartSummaryResponse>
  {
    return this.http.post<CartSummaryResponse>(this.apiUrl, ids)
  }

  addToIdsList(id: number)
  {
    let list : number[] = this.getIdsList()

    list.push(id)

    let newList = [...new Set(list)]

    sessionStorage.setItem("productIds", JSON.stringify(newList));
  }

  getIdsList(): number[]
  {
    let list : number[] = JSON.parse(sessionStorage.getItem("productIds"))
    return list
  }
}
