
namespace allSpice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecipesController : ControllerBase
{
    private readonly RecipesService _recipesService;
    private readonly Auth0Provider _auth0provider;
    private readonly IngredientsService _ingredientsService;

    public RecipesController(RecipesService recipesService, Auth0Provider auth0provider, IngredientsService ingredientsService)
    {
        _recipesService = recipesService;
        _auth0provider = auth0provider;
        _ingredientsService = ingredientsService;
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
    public async Task<ActionResult<Recipe>> Getone(int id)
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
}
