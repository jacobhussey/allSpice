<template>
    <div class="modal modal-xl fade" id="recipe-info" tabindex="-1" aria-labelledby="recipeInfoModal"
        aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content bg-light">
                <div class="modal-header text-center bg-dark">
                    <h3 class="modal-title" id="recipe-info">{{ activeRecipe.title }}</h3>
                    <div v-if="activeRecipe.creatorId == account.id" class="d-flex justify-content-end">
                        <!-- <button @click="removeRecipe(activeRecipe.id)" class="btn btn-danger">
                            <i class="mdi mdi-delete"></i>
                        </button> -->
                        <button data-bs-toggle="modal" data-bs-target="#edit-recipe" class="btn btn-warning">
                            <i class="mdi mdi-pencil"></i>
                        </button>
                    </div>
                    <button type="button" class="btn-close bg-danger" data-bs-dismiss="modal"
                        aria-label="Close"></button>
                </div>

                <div class="modal-body bg-dark">
                    <section class="row">
                        <div class="col-5 me-5">
                            <img :src="activeRecipe.img" alt="" class="img-height ms-1">
                        </div>

                        <div class="col-6 bg-dark box-border border-rounding text-center">
                            <div class="row">

                                <div class="col-6">
                                    <div class="bottom-border border-rounding">
                                        <h4 class="mt-2">Ingredients:</h4>
                                    </div>
                                    <div class="mt-2 mb-5">
                                        {{ ingredients.name }}
                                    </div>
                                    <div class="mt-5">
                                        <form @submit.prevent="createIngredient()" class="move-down">
                                            <div>
                                                <textarea v-model="edit.body" name="" id="" cols="25" rows="3"
                                                    class="mt-3 rounded" placeholder="Add ingredients"></textarea>
                                                <button class="btn btn-light move-up">
                                                    <i class="mdi mdi-plus"></i>
                                                </button>
                                            </div>
                                        </form>
                                    </div>
                                </div>

                                <div class="col-6 right-border">
                                    <div class="bottom-border border-rounding">
                                        <h4 class="mt-2">Instructions:</h4>
                                    </div>
                                    <div class="mt-2">
                                        <div>
                                            {{ activeRecipe.instructions }}
                                        </div>
                                    </div>
                                </div>
                                <!-- TODO add ingredients here -->

                            </div>
                        </div>
                    </section>

                </div>
                <div class="modal-footer bg-dark">
                </div>
            </div>
        </div>
    </div>




    <!-- SECTION EDIT RECIPE FORM -->
    <div class="modal fade" id="edit-recipe" tabindex="-1" aria-labelledby="editRecipe" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content bg-light">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="edit-recipe">Edit Recipe </h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <form @submit.prevent="editRecipe(activeRecipe.id)">

                    <div class="modal-body">
                        <div class="form-floating mb-3 elevation-5 user-box">
                            <input v-model="editable.title" type="text" required class="form-control" id="title"
                                placeholder="Recipe Title">
                            <label for="name">Recipe Title</label>
                        </div>
                        <div class="form-floating mb-3 elevation-5 user-box">
                            <input v-model="editable.instructions" type="text" required class="form-control"
                                id="instructions" placeholder="Recipe Instructions">
                            <label for="name">Recipe Instructions</label>
                        </div>
                        <div class="form-floating mb-3 elevation-5 user-box">
                            <input v-model="editable.category" type="text" required class="form-control" id="category"
                                placeholder="Recipe Category">
                            <label for="name">Recipe Category</label>
                        </div>

                    </div>
                    <div class="modal-footer">
                        <button type="submit" class="btn btn-primary">Submit</button>
                    </div>
                </form>
            </div>
        </div>
    </div>
</template>

<script>
import { AppState } from '../AppState';
import { computed, ref } from '@vue/reactivity';
import { logger } from '../utils/Logger';
import Pop from '../utils/Pop';
import { recipesService } from '../services/RecipesService';
import { ingredientsService } from '../services/IngredientsService';
import { Modal } from 'bootstrap';
import { onMounted, watchEffect } from 'vue';



export default {

    setup() {
        const editable = ref({})
        const edit = ref('')
        async function editRecipe(recipeId) {
            try {
                await recipesService.editRecipe(recipeId, editable.value)
                Pop.toast('Recipe Edited!', 'success')
                Modal.getOrCreateInstance('#edit-recipe').hide()
            } catch (error) {
                console.error(error)
                Pop.error(('[ERROR]'), error.message)
            }
        }

        // async function getIngredients() {
        //     try {
        //         await ingredientsService.getIngredients(activeRecipe.id)
        //     } catch (error) {
        //         console.error(error)
        //         Pop.error(('[ERROR]'), error.message)
        //     }
        // }

        // onMounted(() => {
        //     getIngredients

        // })
        return {
            activeRecipe: computed(() => AppState.activeRecipe),
            account: computed(() => AppState.account),
            ingredients: computed(() => AppState.ingredients),
            editable,
            editRecipe,
            edit,
            // getIngredients,

            async createIngredient() {
                try {
                    await ingredientsService.createIngredient(edit.value)
                    edit = ''
                } catch (error) {
                    console.error(error)
                    Pop.error(('[ERROR]'), error.message)
                }
            }


            // async removeRecipe(recipeId) {
            //     try {
            //         await recipesService.removeRecipe(recipeId)
            //         Modal.getOrCreateInstance('#recipe-info').hide()
            //     } catch (error) {
            //         logger.error(error)
            //         Pop.error(('[ERROR]'), error.message)
            //     }
            // }
        };

    }
}
</script>

<style scoped lang="scss">
.img-height {
    max-height: 70vh;
    width: 81vh;
    border-radius: 15px;
}

.border-rounding {
    border-radius: 15px;
}

.bottom-border {
    border-bottom: 3px solid white;
}

.box-border {
    border: 1px solid white;
}

.move-down {
    transform: translateY(28vh);
}

.move-up {
    transform: translateY(-3vh);
}

// @media (min-width: 768px) {
//     .modal-xl {
//         width: 90%;
//         max-width: 1200px;
//     }
// }
</style>