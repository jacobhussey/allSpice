import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class RecipesService {
    async getRecipes() {
        const res = await api.get('api/recipes')
        logger.log('getting the recipes', res.data)
        AppState.recipes = res.data
    }

    async createRecipe(recipeData) {
        const res = await api.post('api/recipes', recipeData)
        logger.log('created recipe', res.data)
        AppState.recipes.push(res.data)
    }


    async searchRecipes(query) {
        const res = await api.get('api/recipes')
        AppState.recipes = res.data
        logger.log('searching recipes', res.data)
        AppState.recipes = AppState.recipes.filter((r) => r.category.toString().toUpperCase().includes(query.toUpperCase()))
    }
}

export const recipesService = new RecipesService()