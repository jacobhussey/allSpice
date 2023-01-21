
namespace allSpice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IngredientsController : ControllerBase
{
    private readonly IngredientsService _ingredientsService;
    private readonly Auth0Provider _auth0Provider;

    public IngredientsController(IngredientsService ingredientsService, Auth0Provider auth0Provider)
    {
        _ingredientsService = ingredientsService;
        _auth0Provider = auth0Provider;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Ingredient>> Create([FromBody] Ingredient ingredientData)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            Ingredient ingredient = _ingredientsService.Create(ingredientData);
            return Ok(ingredient);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
