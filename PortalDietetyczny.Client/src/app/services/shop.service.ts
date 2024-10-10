import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import {CartSummaryRequest, CartSummaryResponse} from '../DTOs/CartSummaryResponse';
import { OrderDto } from '../DTOs/OrderDto';
import { OrderSummaryDto } from '../DTOs/OrderSummaryDto';
import { PagedResult } from '../models/PagedResult';
import { OrdersSummaryRequestDto } from '../DTOs/OrdersSummaryRequestDto';
import { config } from '../config';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  apiUrl: string = config.API_URL + "shop/"
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

  sendOrder(order: OrderDto) : Observable<any>
  {
    return this.http.post(this.apiUrl + "order", order)
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

  getOrdersPaged(request: OrdersSummaryRequestDto) : Observable<PagedResult<OrderSummaryDto>>
  {
    return this.http.post<PagedResult<OrderSummaryDto>>(this.apiUrl + "orders/paged", request, {withCredentials: true});
  }

  getInvoice(id: number) : Observable<HttpResponse<Blob>> {

    return this.http.post(`${this.apiUrl}order/invoice/${id}`, null,
      {
        observe: 'response',
        responseType: 'blob',
        withCredentials: true
      });
  }
}
