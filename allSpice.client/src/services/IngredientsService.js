import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class IngredientsService {
    async getIngredients(recipeId) {
        const res = await api.get(`api/recipes/${recipeId}/ingredients`)
        logger.log('getting ingredients', res.data)
        AppState.ingredients = res.data
        logger.log(AppState.ingredients)
    }

    async createIngredient() {
        const res = await api.post('api/ingredients')
        logger.log('ingredient created', res.data)
        AppState.ingredients.push(res.data)
    }


}

export const ingredientsService = new IngredientsService()