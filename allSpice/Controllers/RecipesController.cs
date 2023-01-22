
namespace allSpice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecipesController : ControllerBase
{
    private readonly RecipesService _recipesService;
    private readonly Auth0Provider _auth0provider;
    private readonly IngredientsService _ingredientsService;
    private readonly FavoritesService _favoritesService;

    public RecipesController(RecipesService recipesService, Auth0Provider auth0provider, IngredientsService ingredientsService, FavoritesService favoritesService)
    {
        _recipesService = recipesService;
        _auth0provider = auth0provider;
        _ingredientsService = ingredientsService;
        _favoritesService = favoritesService;
    }

    [HttpGet]
    public ActionResult<List<Recipe>> Get()
    {
        try
        {
            List<Recipe> recipes = _recipesService.Get();
            return Ok(recipes);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Recipe>> GetOne(int id)
    {
        try
        {
            Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
            Recipe recipe = _recipesService.GetOne(id, userInfo?.Id);
            return Ok(recipe);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{id}/ingredients")]
    public async Task<ActionResult<List<Ingredient>>> GetIngredients(int id)
    {
        try
        {
            Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
            List<Ingredient> ingredients = _ingredientsService.GetIngredientsByRecipe(id, userInfo?.Id);
            return Ok(ingredients);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{id}/favorites")]
    public async Task<ActionResult<List<Favorite>>> GetFavorites(int id)
    {
        try
        {
            Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
            List<Favorite> favorites = _favoritesService.GetFavorites(id, userInfo?.Id);
            return Ok(favorites);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Recipe>> Create([FromBody] Recipe recipeData)
    {
        try
        {
            Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
            recipeData.CreatorId = userInfo.Id;
            Recipe recipe = _recipesService.Create(recipeData);
            recipe.Creator = userInfo;
            return Ok(recipe);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<string>> Remove(int id)
    {
        try
        {
            Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
            string message = _recipesService.Remove(id, userInfo.Id);
            return Ok(message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Recipe>> Update([FromBody] Recipe recipeUpdate, int id)
    {
        try
        {
            Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
            Recipe recipe = _recipesService.Update(recipeUpdate, id, userInfo.Id);
            return Ok(recipe);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
