import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class FavoritesService {

    async createFavorite(recipeId) {
        const res = await api.post('api/favorites', { recipeId })
        logger.log('created favorite', res.data)
        AppState.favorites = res.data
    }

}

export const favoritesService = new FavoritesService()