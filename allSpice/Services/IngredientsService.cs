namespace allSpice.Services;

public class IngredientsService
{
    private readonly RecipesService _recipesService;
    private readonly IngredientsRepository _repo;

    public IngredientsService(IngredientsRepository repo, RecipesService recipesService)
    {
        _repo = repo;
        _recipesService = recipesService;
    }

    internal List<Ingredient> GetIngredientsByRecipe(int recipeId, string userId)
    {
        Recipe recipe = _recipesService.GetOne(recipeId, userId);

        List<Ingredient> ingredients = _repo.GetIngredientsByRecipe(recipeId);
        return ingredients;
    }

    internal Ingredient Create(Ingredient ingredientData)
    {
        Ingredient ingredient = _repo.Create(ingredientData);
        return ingredient;
    }

    internal string Remove(int id, string userId)
    {
        Ingredient original = _repo.GetOne(id);
        if (original == null)
        {
            throw new Exception("No ingredient at that id");
        }
        _repo.Remove(id);
        return $"Ingredient at {id} was removed";

    }
}
