import { NutritionInfo, RecipeIngredientDto } from "./AddRecipeDto";

export interface RecipeDetailsDto
{
  uid: number;
  recipeTags: string[];
  name: string;
  nutrition: NutritionInfo;
  ingredients: RecipeIngredientDto[];
  instruction: string;
  photoUrl?: string;
}
