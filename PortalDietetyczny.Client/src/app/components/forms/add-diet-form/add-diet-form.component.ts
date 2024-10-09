import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { IdAndName } from '../../../DTOs/IdAndName';
import { TagContext } from '../../../DTOs/AddTagDto';
import { AddDietDto } from '../../../DTOs/AddDietDto';
import { DietsService } from '../../../services/diets.service';

@Component({
  selector: 'app-add-diet-form',
  templateUrl: './add-diet-form.component.html',
  styleUrl: './add-diet-form.component.css'
})
export class AddDietFormComponent implements OnInit{
  tagContextDiet: TagContext = TagContext.Diet;
  dietForm: FormGroup;
  tagsForm: FormGroup;
  photoName: string = "";
  photo: string;
  tagsNames: IdAndName[] = [];
  dietsNames: IdAndName[] = [];
  selectedTagToDelete: number;
  selectedDietToDelete: number;
  selectedTags: string[] = []
  selectedTagsIds: number[] = []
  fileName: string = "";

  file: string;

  filesNames: IdAndName[] = [];
  selectedFileToDelete: number;
  dietNames : IdAndName[] = []

  constructor(private fb: FormBuilder, private dietService: DietsService)
  {

  }

  ngOnInit(): void
  {
    this.getTagsNames()
    this.getDietsNames()

    this.dietForm = this.fb.group(
      {
        dietName: new FormControl('', Validators.required),
        kcal: new FormControl('', Validators.required),
        price: new FormControl(0, [Validators.required, Validators.pattern(/^\d+\.?\d{0,2}$/)])
      })

    this.tagsForm = this.fb.group(
      {
        tagName: ['', Validators.required]
      })


    }

  onPhotoSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    const photo = input.files[0];

    const reader = new FileReader();

    reader.onload = (e: any) =>
    {
      const arrayBuffer = e.target.result;
      const bytes = new Uint8Array(arrayBuffer);
      let binary = '';
      bytes.forEach((byte) => binary += String.fromCharCode(byte));
      this.photo = btoa(binary);
    }

    reader.readAsArrayBuffer(photo);
    this.photoName = photo.name
  }

  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    const file = input.files[0];

    const reader = new FileReader();

    reader.onload = (e: any) =>
    {
      const arrayBuffer = e.target.result;
      const bytes = new Uint8Array(arrayBuffer);
      let binary = '';
      bytes.forEach((byte) => binary += String.fromCharCode(byte));
      this.file = btoa(binary);
    }

    reader.readAsArrayBuffer(file);
    this.fileName = file.name
  }

  removeTag(tag: string) {
    this.selectedTags = this.selectedTags.filter(t => t !== tag);
  }

  addTag() {
    const tag = this.tagsForm.value;
    let tagName = tag.tagName.name;
    let tagId = tag.tagName.id;

    this.selectedTags.push(tagName);
    this.selectedTagsIds.push(tagId)
    this.tagsForm.reset();
  }

  getTagsNames()
  {
    this.dietService.getTags().subscribe(
      dto  =>
      {
        this.tagsNames = dto.names;
      },
      error => console.log(error)
    )
  }

  getDietsNames()
  {
    this.dietService.getDiets().subscribe(
      dto  =>
      {
        this.dietsNames = dto.names;
      },
      error => console.log(error)
    )
  }

  deleteTag()
  {
    this.dietService.deleteTag(this.selectedTagToDelete).subscribe(
      (response)=>{},
      (error) => console.log(error.error))
  }
  submitDiet() {
    const recipeFormVal = this.dietForm.value;

    const addDietDto: AddDietDto = {
      tagsIds: this.selectedTagsIds,
      name: recipeFormVal.dietName,
      kcal: recipeFormVal.kcal,
      fileBytes: this.file,
      fileName: this.fileName,
      photoFileBytes: this.photo || null,
      photoFileName: this.photoName,
      price: recipeFormVal.price * 100

    };

    this.dietService.addDiet(addDietDto).subscribe(
      response => {},
      error => console.log(error.error))
  }

  deleteDiet()
  {
    this.dietService.deleteDiet(this.selectedDietToDelete).subscribe(

      response=> {},
      error =>  console.log(error.error))
  }
}
