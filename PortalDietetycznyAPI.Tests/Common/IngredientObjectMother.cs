using PortalDietetycznyAPI.Domain.Entities;
using PortalDietetycznyAPI.Infrastructure.Context;

namespace PortalDietetycznyAPI.Tests.Common;

public class IngredientObjectMother
{
    public static async Task<Ingredient> CreateAsync(Db dbContext, string name)
    {
        var ingredient = new Ingredient()
        {
            Name = name
        };

        await dbContext.Ingredients.AddAsync(ingredient);
        await dbContext.SaveChangesAsync();
        return ingredient;
    }
}