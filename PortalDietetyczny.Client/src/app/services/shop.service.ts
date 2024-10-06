import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import {CartSummaryRequest, CartSummaryResponse} from '../DTOs/CartSummaryResponse';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  apiUrl: string = "https://localhost:44317/api/shop/"
  constructor(private http : HttpClient)
  {
    let list : number[] = []
    if (sessionStorage.getItem('productIds') === null)
      sessionStorage.setItem('productIds', JSON.stringify(list));

  }

  getCartSummary(ids: CartSummaryRequest): Observable<CartSummaryResponse>
  {
    return this.http.post<CartSummaryResponse>(this.apiUrl, ids)
  }

  addToIdsList(id: number)
  {
    let list : number[] = this.getIdsList()

    list.push(id)

    list = [...new Set(list)]

    sessionStorage.setItem("productIds", JSON.stringify(list));
  }

  removeFromIdsList(id: number)
  {
    let list : number[] = this.getIdsList()
    list = list.filter(item => item !== id);

    sessionStorage.setItem("productIds", JSON.stringify(list));

    location.reload()
  }

  getIdsList(): number[]
  {
    let list : number[] = JSON.parse(sessionStorage.getItem("productIds"))
    return list
  }
}
