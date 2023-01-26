import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { api } from "./AxiosService"

class RecipesService {
    async getRecipes() {
        const res = await api.get('api/recipes')
        logger.log('getting the recipes', res.data)
        AppState.recipes = res.data
    }

    async getRecipeById(recipeId) {
        const activeRecipe = AppState.recipes.find(r => r.id == recipeId)
        AppState.activeRecipe = activeRecipe
        logger.log('setting the active recipe', activeRecipe)
    }

    async getMyRecipes(accountId) {
        try {
            // this.getRecipes()
            const filteredRecipes = AppState.recipes.filter(f => f.creatorId == accountId)
            logger.log('filtered', filteredRecipes)
            AppState.myRecipes = filteredRecipes
            AppState.recipes = AppState.myRecipes
        } catch (error) {
            console.error(error)
            Pop.error(('[ERROR]'), error.message)
        }
    }

    async createRecipe(recipeData) {
        const res = await api.post('api/recipes', recipeData)
        logger.log('created recipe', res.data)
        AppState.recipes.push(res.data)
        return res.data
    }

    async removeRecipe(recipeId) {
        const res = await api.delete(`api/recipes/${recipeId}`)
        logger.log('recipe deleted', res.data)
        AppState.recipes = AppState.recipes.filter(r => r.id != recipeId)
        // TODO remove favorites
    }

    async editRecipe(recipeId, recipeData) {
        const res = await api.put(`api/recipes/${recipeId}`, recipeData)
        logger.log('edited recipe', res.data)
        AppState.recipes.push(res.data)
        AppState.activeRecipe = res.data
    }

    async searchRecipes(query) {
        const res = await api.get('api/recipes')
        AppState.recipes = res.data
        logger.log('searching recipes', res.data)
        AppState.recipes = AppState.recipes.filter((r) => r.category.toString().toUpperCase().includes(query.toUpperCase()))
    }
}

export const recipesService = new RecipesService()