

namespace allSpice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FavoritesController : ControllerBase
{
    private readonly FavoritesService _favoritesService;
    private readonly Auth0Provider _auth0Provider;

    public FavoritesController(FavoritesService favoritesService, Auth0Provider auth0Provider)
    {
        _favoritesService = favoritesService;
        _auth0Provider = auth0Provider;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Favorite>> Create([FromBody] RecipeMember recipeMemberData)
    {
        try
        {
            Favorite userInfo = await _auth0Provider.GetUserInfoAsync<Favorite>(HttpContext);
            recipeMemberData.AccountId = userInfo.Id;
            int id = _favoritesService.Create(recipeMemberData);
            userInfo.RecipeMemberId = id;
            return Ok(userInfo);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    // [HttpDelete("{id}")]
    // [Authorize]
    // public async Task<ActionResult<string>> Remove(int id)
    // {
    //     try
    //     {
    //         Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
    //         string Message = _favoritesService.Remove(id, userInfo.Id);
    //         return Ok(Message);
    //     }
    //     catch (Exception e)
    //     {
    //         return BadRequest(e.Message);
    //     }
    // }
}
