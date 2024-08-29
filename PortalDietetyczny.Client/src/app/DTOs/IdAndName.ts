export interface IdAndName
{
  id: number;
  name: string;
}
export interface IngredientListDto
{
  ingredients: IdAndName[];
}

export interface TagListDto
{
  tags: IdAndName[];
}
