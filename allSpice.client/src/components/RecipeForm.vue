<template>
    <div class="modal-header">
        <h1 class="modal-title fs-5" id="create-recipe-modal">Create Recipe</h1>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
    </div>
    <form @submit.prevent="createRecipe()">
        <div class="modal-body">
            <div class="form-floating mb-3 elevation-5">
                <input v-model="editable.title" type="text" required class="form-control" id="title"
                    placeholder="Title">
                <label for="name">Recipe Title</label>
            </div>
            <div class="form-floating mb-3 elevation-5">
                <input v-model="editable.instructions" type="text" required class="form-control" id="instructions"
                    placeholder="Subtitle">
                <label for="name">Instructions</label>
            </div>
            <!-- <div class="form-floating mb-3 elevation-5 dropdown">
                <input v-model="editable.category" type="dropdown" required class="form-control dropdown-toggle"
                    id="instructions" placeholder="Category" data-bs-toggle="dropwdown" aria-expanded="false">
                <label for="name">Category</label>
                <ul class="dropdown-menu">
                    <li><a class="dropdown-item" href="#">Action</a></li>
                    <li><a class="dropdown-item" href="#">Another action</a></li>
                    <li><a class="dropdown-item" href="#">Something else here</a></li>
                </ul>
            </div> -->
            <div class="form-floating mb-3 elevation-5">
                <input v-model="editable.img" type="url" required class="form-control" id="img"
                    placeholder="Recipe Image">
                <label for="img">Cover Image</label>
            </div>

            <label for="category">Category:</label>
            <select v-model="editable.category" id="category" name="category"
                class="form-floating elevation-5 w-100 py-2 mt-2 rounded">
                <option value="misc">Miscellaneous</option>
                <option value="italian">Italian</option>
                <option value="mexican">Mexican</option>
                <option value="asian">Asian</option>
                <option value="greek">Greek</option>
            </select>
        </div>
        <div class="modal-footer">
            <button type="submit" class="btn btn-danger rounded-pill">Submit</button>
        </div>
    </form>
</template>

<script>
import { ref } from 'vue';
import { recipesService } from '../services/RecipesService';
import Pop from '../utils/Pop';
import { Modal } from 'bootstrap';



export default {



    setup() {
        const editable = ref({})
        return {
            editable,
            async createRecipe() {
                try {
                    await recipesService.createRecipe(editable.value)
                    Modal.getOrCreateInstance('#create-recipe-modal').hide()
                    // editable = {}
                } catch (error) {
                    console.error(error)
                    Pop.error(('[ERROR]'), error.message)
                }
            }
        };

    }
}
</script>

<style>

</style>