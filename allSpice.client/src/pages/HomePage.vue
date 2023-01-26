<template>
  <section class="container-fluid">
    <!-- SECTION COVER IMAGE & SEARCH/ACCOUNT -->
    <section class="row mx-3 text-white fw-bold elevation-5 rounded mt-3">
      <div class="col-12 bg-img text-center py-5 rounded">

        <div class="d-flex justify-content-end move-search">
          <form @submit.prevent="searchRecipes" class="mt-2" action="">
            <div class="d-flex">
              <input v-model="editable" class="rounded" type="text" placeholder="search" id="search">
              <!-- <label for="search">Search by category</label> -->
              <button type="submit" class="btn btn-primary px-2 move-over rounded-pill">
                <i class="mdi mdi-magnify"></i>
              </button>
            </div>
          </form>
          <div class="me-3">
            <Login />
          </div>
        </div>

        <h1>All-Spice</h1>
        <h4>Cherish your family</h4>
        <h4>And their cooking</h4>
      </div>
    </section>

    <!-- SECTION FILTER BY CATEGORY -->
    <section class="row justify-content-center mt-4">
      <div class="col-6 bg-white d-flex justify-content-between move-up elevation-5 rounded py-3 px-4">
        <button @click="getRecipes()" class="btn btn-white selectable text-black">
          <h5>Home</h5>
        </button>
        <button @click="getMyRecipes()" class="btn btn-white selectable text-black">
          <h5>My Recipes</h5>
        </button>
        <button @click="" class="btn btn-white selectable text-black">
          <h5>Favorites</h5>
        </button>
      </div>
    </section>

    <!-- SECTION RECIPE CARD -->
    <section class="row mt-5 justify-content-center">
      <div v-for="r in recipes" class="col-3 mx-3">
        <RecipeCard :recipe="r" />
      </div>
    </section>

    <!-- SECTION CREATE RECIPE BUTTON -->
    <section class="row">
      <button v-if="account.id" href="#" class="float btn" data-bs-toggle="modal" data-bs-target="#create-recipe-modal"
        title="Create Recipe">
        <i class="mdi mdi-plus my-float fs-1"></i>
      </button>

    </section>

  </section>

</template>

<script>
import Login from '../components/Login.vue';
import { computed, ref, watchEffect } from 'vue'
import { AppState } from '../AppState';
import Pop from '../utils/Pop';
import { logger } from '../utils/Logger';
import { recipesService } from '../services/RecipesService.js'
import RecipeCard from '../components/RecipeCard.vue';
import { useRoute } from 'vue-router';



export default {
  setup() {

    const editable = ref('')
    async function getRecipes() {
      try {
        await recipesService.getRecipes()
      } catch (error) {
        logger.error(error)
        Pop.error(('[ERROR]'), error.message)
      }
    }



    watchEffect(() => {
      getRecipes();
      AppState.activeRecipe
      // AppState.recipes
    })
    return {
      recipes: computed(() => AppState.recipes),
      account: computed(() => AppState.account),
      myRecipes: computed(() => AppState.myRecipes),
      getRecipes,
      editable,

      async getMyRecipes() {
        try {
          await recipesService.getMyRecipes(AppState.account.id)
        } catch (error) {
          console.error(error)
          Pop.error(('[ERROR]'), error.message)
        }
      },

      async searchRecipes() {
        try {
          await recipesService.searchRecipes(editable.value)
          this.editable = ''
        } catch (error) {
          logger.error(error)
          Pop.error(('[ERROR]'), error.message)
        }
      }
    };
  },

  components: { Login, RecipeCard }
}
</script>

<style scoped lang="scss">
.bg-img {
  background-image: url("https://images.unsplash.com/photo-1493124325580-27c4b1f7e4c5?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1171&q=80");
  background-position: center;
  background-size: cover;
}

.move-over {
  transform: translateX(-2vw);
}

// .move-up {
//   transform: translateY(-2vh);
// }

.move-search {
  transform: translateY(-5vh);
}

* {
  padding: 0;
  margin: 0;
}

body {
  font-family: Verdana, Geneva, sans-serif;
  font-size: 18px;
  background-color: #CCC;
}

.float {
  position: fixed;
  width: 60px;
  height: 60px;
  bottom: 40px;
  right: 40px;
  background-color: #0C9;
  color: #FFF;
  border-radius: 50px;
  text-align: center;
  box-shadow: 2px 2px 3px #999;
}

.my-float {
  margin-top: 22px;
}
</style>
