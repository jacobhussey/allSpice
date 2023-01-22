

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

    internal int Create(RecipeMember recipeMemberData)
    {
        Recipe recipe = _recipesService.GetOne(recipeMemberData.recipeId, recipeMemberData.AccountId);

        int id = _repo.Create(recipeMemberData);

        return id;
    }

    internal List<Favorite> GetFavorites(int recipeId, string userId)
    {
        Recipe recipe = _recipesService.GetOne(recipeId, userId);

        List<Favorite> favorites = _repo.GetFavorites(recipeId);
        return favorites;
    }

    // internal string Remove(int id, string userId)
    // {

    // }
}
