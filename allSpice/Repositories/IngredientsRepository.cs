
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
        i.*
        FROM ingredients i 
        WHERE recipeId = @recipeId;
        ";
        List<Ingredient> ingredients = _db.Query<Ingredient>(sql).ToList();
        return ingredients;
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
}
