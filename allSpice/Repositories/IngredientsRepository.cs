
namespace allSpice.Repositories;

public class IngredientsRepository
{
    private readonly IDbConnection _db;

    public IngredientsRepository(IDbConnection db)
    {
        _db = db;
    }

    internal List<Ingredient> GetIngredientsByRecipe(int recipeId)
    {
        string sql = @"
        SELECT
        *
        FROM ingredients 
        WHERE recipeId = @recipeId;
        ";
        List<Ingredient> ingredients = _db.Query<Ingredient>(sql, new { recipeId }).ToList();
        return ingredients;
    }

    internal Ingredient GetOne(int id)
    {
        string sql = @"
        SELECT
        *
        FROM ingredients
        WHERE id = @id;
        ";
        return _db.Query<Ingredient>(sql, new { id }).FirstOrDefault();
    }

    internal Ingredient Create(Ingredient ingredientData)
    {
        string sql = @"
        INSERT INTO ingredients
        (name, quantity, recipeId)
        VALUES
        (@name, @quantity, @recipeId);
        SELECT LAST_INSERT_ID();
        ";
        int id = _db.ExecuteScalar<int>(sql, ingredientData);
        ingredientData.Id = id;
        return ingredientData;
    }

    internal void Remove(int id)
    {
        string sql = @"
        DELETE FROM ingredients
        WHERE id = @id;
        ";
        _db.Execute(sql, new { id });
    }
}
