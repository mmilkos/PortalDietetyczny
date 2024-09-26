import { Component, OnInit } from '@angular/core';
import { DietsPreviewPageRequest } from '../../../DTOs/DietsPreviewPageRequest';
import { DietsService } from '../../../services/diets.service';
import { IdAndName } from '../../../DTOs/IdAndName';
import { DietPreviewDto } from '../../../DTOs/DietPreviewDto';
import { PagedResult } from '../../../models/PagedResult';

@Component({
  selector: 'app-diets-grid',
  templateUrl: './diets-grid.component.html',
  styleUrl: './diets-grid.component.css'
})
export class DietsGridComponent implements OnInit
{
  dummyDiet: DietPreviewDto =
    {
      id: null,
      name: null,
      kcal: null,
      photoUrl:"https://res.cloudinary.com/dzohpx1mq/image/upload/v1726062320/Stock/hyui3cahzqmujvt1jlx5.gif",
      price: null
    };

  diets: DietPreviewDto[] =
    [
      this.dummyDiet,
      this.dummyDiet,
      this.dummyDiet,
      this.dummyDiet,
      this.dummyDiet,
      this.dummyDiet,
    ];

  tagsNames: IdAndName[] = [];
  selectedTagsIds : number[] = []
  tagSelections = new Map<number, boolean>();
  currentPage: number = 1;
  totalPages: number

  constructor(private dietsService: DietsService) {}

  ngOnInit(): void
  {
    let initDto : DietsPreviewPageRequest =
      {
        pageNumber: this.currentPage,
        pageSize: 6,
        tagsIds: []
      }

      this.getTagsNames()
      this.getDiets(initDto);
  }

  getDiets(dto: DietsPreviewPageRequest)
  {
    this.dietsService.getDietsPaged(dto).subscribe(
      (response: PagedResult<DietPreviewDto>) =>
      {
        this.diets = response.data;
        this.totalPages = Math.ceil(response.totalCount / 6)
      });
  }

  getTagsNames()
  {
    this.dietsService.getTags().subscribe(
      dto =>
      {
        this.tagsNames = dto.names;
        console.log(this.tagsNames)
      },
      error => console.log(error)
    )
  }

  toggleTagSelection(tagId: number, event: Event) {
    const inputElement = event.target as HTMLInputElement
    if (inputElement.checked) {
      this.tagSelections.set(tagId, true);
    } else {
      this.tagSelections.delete(tagId);
    }
  }

  search()
  {
    var initDto: DietsPreviewPageRequest =
      {
        pageNumber: 1,
        pageSize: 6,
        tagsIds: Array.from(this.tagSelections.keys()),
      };

    this.getDiets(initDto);
  }

  nextPage()
  {
    if (this.currentPage + 1 <= this.totalPages) {
      this.currentPage++;
    }

    this.getDiets({
      pageNumber: this.currentPage,
      pageSize: 6,
      tagsIds: Array.from(this.tagSelections.keys()),
    });

    window.scrollTo(0, 0);
  }

  previousPage()
  {
    this.currentPage--;
    if (this.currentPage < 1) this.currentPage = 1

    this.getDiets({
      pageNumber: this.currentPage,
      pageSize: 6,
      tagsIds: Array.from(this.tagSelections.keys()),
    });

    window.scrollTo(0, 0);
  }
}
