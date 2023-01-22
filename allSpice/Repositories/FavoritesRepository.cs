namespace allSpice.Repositories;

public class FavoritesRepository
{
    private readonly IDbConnection _db;

    public FavoritesRepository(IDbConnection db)
    {
        _db = db;
    }


    internal int Create(RecipeMember recipeMemberData)
    {
        string sql = @"
        INSERT INTO recipeMembers
        (recipeId, accountId)
        VALUES
        (@recipeId, @accountId);
        SELECT LAST_INSERT_ID();
        ";
        int id = _db.ExecuteScalar<int>(sql, recipeMemberData);
        return id;
    }

    internal List<Favorite> GetFavorites(int recipeId)
    {
        string sql = @"
        SELECT
        ac.*,
        rm.id AS recipeMemberId
        FROM recipeMembers rm
        JOIN accounts ac ON ac.id = rm.accountId
        WHERE rm.recipeId = @recipeId;
        ";
        return _db.Query<Favorite>(sql, new { recipeId }).ToList();
    }
}
