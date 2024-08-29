export interface AddRecipeDto
{
  tagsIds: number[];
  name: string;
  ingredients: RecipeIngredientDto[];
  nutritionInfo: NutritionInfo;
  instruction: string;
  fileBytes: string
  fileName: string
}
export interface RecipeIngredientDto {
  id: number;
  name: string;
  unit: string;
  unitValue: number;
  homeUnit: string;
  homeUnitValue: number;
}

export interface NutritionInfo {
  fiber: number;
  fat: number;
  carb: number;
  protein: number;
  kcal: number;
}
