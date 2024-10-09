import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { ShopService } from '../../../services/shop.service';
import { OrdersSummaryRequestDto } from '../../../DTOs/OrdersSummaryRequestDto';
import { PagedResult } from '../../../models/PagedResult';
import { OrderSummaryDto } from '../../../DTOs/OrderSummaryDto';
import { HttpResponse } from '@angular/common/http';

@Component({
  selector: 'app-orders-list',
  templateUrl: './orders-list.component.html',
  styleUrl: './orders-list.component.css'
})
export class OrdersListComponent implements OnInit
{
  selectedInvoiceId: number | null = null;

  currentPage: number = 1;
  pageSize = 5;

  invoiceForm: FormGroup;
  range: FormGroup
  initStartDate = new Date(new Date().setDate(1)).toISOString()
  initEndDate = new Date().toISOString()

  orders: OrderSummaryDto[]
  invoicesList: number[] = [];
  constructor(private shopService: ShopService, private fb: FormBuilder) {
  }

  ngOnInit(): void
  {
    this.invoiceForm = this.fb.group(
      {
        invoice: [false]
      })

    this.range = this.fb.group(
      {
        start: new FormControl(),
        end: new FormControl()
      }
    )

    let initDto : OrdersSummaryRequestDto =
      {
        pageNumber: this.currentPage,
        pageSize: this.pageSize,
        dateFrom: this.initStartDate,
        dateTo: this.initEndDate
      }
      this.getOrders(initDto)
  }

  getInvoice()
  {
    this.shopService.getInvoice(this.selectedInvoiceId ).subscribe(
      (response: HttpResponse<Blob>) => {
        if (response.body) {
          let blob = new Blob([response.body], { type: 'application/pdf' });
          let url = window.URL.createObjectURL(blob);
          let contentDisposition = response.headers.get('Content-Disposition') || '';
          let fileName = contentDisposition.split(';')[1].split("=")[1];
          fileName = fileName.replace(/\./g, "-")
          let link = document.createElement('a');
          link.href = url;
          link.download = fileName;
          link.click();
        } else console.log("No response body");
      },
      (error) => console.log(error.error)
    );
  }


  getOrders(dto: OrdersSummaryRequestDto)
  {
    this.shopService.getOrdersPaged(dto).subscribe(
      (response: PagedResult<OrderSummaryDto>) => this.orders = response.data,
      (error)=> console.log(error.error));
  }

  nextPage()
  {
    this.currentPage++;

    var request: OrdersSummaryRequestDto =
      {
        pageNumber: this.currentPage,
        pageSize: this.pageSize,
        dateFrom: this.range.get("start").value || this.initStartDate,
        dateTo: this.range.get("end").value || this.initEndDate,
      }

    this.getOrders(request);
  }

  previousPage()
  {
    if (this.currentPage > 1) this.currentPage--;

    var request: OrdersSummaryRequestDto =
      {
        pageNumber: this.currentPage,
        pageSize: this.pageSize,
        dateFrom: this.range.get("start").value || this.initStartDate,
        dateTo: this.range.get("end").value || this.initEndDate,
      }

    this.getOrders(request);
  }

  submit()
  {
    var request: OrdersSummaryRequestDto =
      {
        pageNumber: this.currentPage,
        pageSize: this.pageSize,
        dateFrom: this.range.get("start").value,
        dateTo: this.range.get("end").value,
      }
    this.getOrders(request)
  }

  onInvoiceChange(invoiceId: number)
  {
    this.selectedInvoiceId = invoiceId;
  }

  clear()
  {
    this.range.get("start").setValue(null)
    this.range.get("end").setValue(null)
  }
}
