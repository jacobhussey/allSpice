
namespace allSpice.Repositories;

public class RecipesRepository
{
    private readonly IDbConnection _db;

    public RecipesRepository(IDbConnection db)
    {
        _db = db;
    }
    internal List<Recipe> Get()
    {
        string sql = @"
       SELECT
       re.*,
       ac.*
       FROM recipes re
       JOIN accounts ac ON ac.id = re.creatorId;
       ";

        List<Recipe> recipes = _db.Query<Recipe, Account, Recipe>(sql, (recipe, account) =>
        {
            recipe.Creator = account;
            return recipe;
        }).ToList();

        return recipes;
    }

    internal Recipe GetOne(int id)
    {
        string sql = @"
        SELECT
        re.*,
        ac.*
        FROM recipes re
        JOIN accounts ac ON ac.id = re.creatorId
        WHERE re.id = @id;
        ";
        return _db.Query<Recipe, Account, Recipe>(sql, (recipe, account) =>
        {
            recipe.Creator = account;
            return recipe;
        }, new { id }).FirstOrDefault();
    }

    internal Recipe Create(Recipe recipeData)
    {
        string sql = @"
        INSERT INTO recipes 
        (title, instructions, img, category, creatorId)
        VALUES
        (@title, @instructions, @img, @category, @creatorId);
        SELECT LAST_INSERT_ID();
        ";
        int id = _db.ExecuteScalar<int>(sql, recipeData);
        recipeData.Id = id;
        return recipeData;
    }

    internal void Remove(int id)
    {
        string sql = @"
        DELETE FROM recipes
        WHERE id = @id;
        ";
        _db.Execute(sql, new { id });
    }

}
