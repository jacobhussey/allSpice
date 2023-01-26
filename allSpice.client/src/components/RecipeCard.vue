<template>
    <!-- <div>
        <button v-if="account.id" @click="createFavorite(recipe.id)" class="btn btn-light">
            <i class="mdi mdi-heart text-danger"></i>
        </button>

        <button v-if="recipe.creatorId == account.id" @click="removeRecipe(recipe.id)" class="btn btn-danger">
            <i class="mdi mdi-delete"></i>
        </button>
    </div>
    <div class="bg-dark text-center p-3" data-bs-toggle="modal" data-bs-target="#recipe-info">
        <div @click="getRecipeById(recipe.id)">
            <img :src="recipe.img" alt="" class="img-fluid recipe-img">
            <div>
                <h4>
                    {{ recipe.category }}
                </h4>
            </div>
            <div>
                <h5>{{ recipe.title }}</h5>
            </div>
        </div>
    </div> -->


    <div>
        <div @click="getRecipeById(recipe.id) && getIngredients(recipe.id)" class="card-style selectable"
            data-bs-toggle="modal" data-bs-target="#recipe-info">
            <div class="card-header">
                <img :src="recipe.img" />
            </div>
            <span class="rounded-pill border-style bg-grey p-1 fst-italic">{{ recipe.category }}</span>
            <div class="p-2 borders">
                <div class="mt-3">
                    <h6>
                        {{ recipe.title }}
                    </h6>
                </div>
            </div>
        </div>
        <div class="move-up move-over d-flex justify-content-end me-4">
            <button v-if="account.id" @click="createFavorite(recipe.id)" class="btn bg-grey rounded-circle">
                <i class="mdi mdi-heart text-danger"></i>
            </button>
            <button v-if="recipe.creatorId == account.id" @click="removeRecipe(recipe.id)" class="btn btn-danger ms-2">
                <i class="mdi mdi-delete"></i>
            </button>
        </div>
    </div>


</template>

<script>
import { recipesService } from '../services/RecipesService.js';
import { favoritesService } from '../services/FavoritesService.js';
import { logger } from '../utils/Logger';
import Pop from '../utils/Pop';
import { AppState } from '../AppState';
import { computed } from '@vue/reactivity';
import { ingredientsService } from '../services/IngredientsService';


export default {

    props: {
        recipe: {
            type: Object,
            required: true
        }
    },

    setup() {

        return {
            account: computed(() => AppState.account),
            async getRecipeById(recipeId) {
                try {
                    await recipesService.getRecipeById(recipeId)
                } catch (error) {
                    logger.error(error)
                    Pop.error(error)
                }
            },

            async getIngredients(recipeId) {
                try {
                    await ingredientsService.getIngredients(recipeId)
                } catch (error) {
                    console.error(error)
                    Pop.error(('[ERROR]'), error.message)
                }
            },

            async createFavorite(recipeId) {
                try {
                    await favoritesService.createFavorite(recipeId)
                    Pop.toast('Liked Recipe!', 'success')
                } catch (error) {
                    console.error(error)
                    Pop.error(('[ERROR]'), error.message)
                }
            },
            async removeRecipe(recipeId) {
                try {
                    await Pop.confirm('Are you sure you want to delete this recipe')
                    await recipesService.removeRecipe(recipeId)
                    // Modal.getOrCreateInstance('#recipe-info').hide()
                } catch (error) {
                    logger.error(error)
                    Pop.error(('[ERROR]'), error.message)
                }
            }
        };

    }
}
</script>

<style scoped lang="scss">
// .recipe-img {
//     height: 25vh;
//     width: 40vh;
// }

.move-up {
    transform: translateY(-10vh);
    // transform: translatex(1vh);
}

.border-style {
    border: 2px solid black;
}

.borders {
    // border-bottom: 1.5px solid black;
    border-top: 1.5px solid black;
}


// .contain-style {
//     display: flex;
//     width: 1040px;
//     justify-content: space-evenly;
//     flex-wrap: wrap;
// }

.card-style {
    // margin: 10px;
    background-color: #fff;
    border-radius: 10px;
    box-shadow: 0 2px 20px rgba(0, 0, 0, 0.2);
    overflow: hidden;
    width: 300px;
    height: 55vh;
}

.card-header img {
    width: 100%;
    height: 200px;
    object-fit: cover;
    // object-position: center;
}

// .user img {
//     border-radius: 50%;
//     width: 40px;
//     height: 40px;
//     // margin-right: 10px;
// }
</style>