

namespace allSpice.Services;

public class FavoritesService
{
    private readonly FavoritesRepository _repo;
    private readonly RecipesService _recipesService;

    public FavoritesService(FavoritesRepository repo, RecipesService recipesService)
    {
        _repo = repo;
        _recipesService = recipesService;
    }

    internal Favorite Create(Favorite favoriteData)
    {
        Recipe recipe = _recipesService.GetOne(favoriteData.RecipeId, favoriteData.AccountId);

        Favorite favorite = _repo.Create(favoriteData);

        return favorite;
    }

    internal List<MyRecipe> GetMyFavorites(string accountId)
    {
        List<MyRecipe> myRecipes = _repo.GetMyFavorites(accountId);
        return myRecipes;
    }



    internal string Remove(int id, string userId)
    {
        Favorite original = _repo.GetOne(id);
        if (original == null)
        {
            throw new Exception("no favorite at this id");
        }
        if (original.AccountId != userId)
        {
            throw new Exception("you didnt favorite this");
        }
        _repo.Remove(id);
        return $"removed favorite at id {id}";
    }

}
