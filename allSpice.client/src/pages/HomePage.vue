<template>
  <section class="container-fluid">
    <!-- SECTION COVER IMAGE & SEARCH/ACCOUNT -->
    <section class="row mx-3 text-white fw-bold elevation-5 rounded mt-3">
      <div class="col-12 bg-img text-center py-5 rounded">

        <div class="d-flex justify-content-end move-search">
          <form @submit.prevent="searchRecipes" class="mt-3 me-3" action="">
            <button type="submit" class="btn btn-primary me-2">
              <i class="mdi mdi-magnify move-over"></i>
            </button>
            <input v-model="editable" class="rounded" type="text" placeholder="search">
          </form>
          <Login />
        </div>

        <h1>All-Spice</h1>
        <h4>Cherish your family</h4>
        <h4>And their cooking</h4>
      </div>
    </section>

    <!-- SECTION FILTER BY CATEGORY -->
    <section class="row justify-content-center">
      <div class="col-3 bg-white d-flex justify-content-between move-up elevation-5 rounded py-2">
        <button @click="getRecipes()" class="btn btn-white selectable text-success">
          <h5>Home</h5>
        </button>
        <button @click="" class="btn btn-white selectable text-success">
          <h5>My Recipes</h5>
        </button>
        <button @click="" class="btn btn-white selectable text-success">
          <h5>Favorites</h5>
        </button>
      </div>
    </section>

    <!-- SECTION RECIPE CARD -->
    <section class="row mx-3">
      <div v-for="r in recipes" class="col-3 mx-5 my-3">
        <RecipeCard :recipe="r" />
      </div>
    </section>

    <!-- SECTION CREATE RECIPE BUTTON -->
    <section class="row">
      <button href="#" class="float btn" data-bs-toggle="modal" data-bs-target="#create-recipe-modal"
        title="Create Recipe">
        <i class="mdi mdi-plus my-float fs-1"></i>
      </button>

    </section>

  </section>

</template>

<script>
import Login from '../components/Login.vue';
import { computed, onMounted, ref } from 'vue'
import { AppState } from '../AppState';
import Pop from '../utils/Pop';
import { logger } from '../utils/Logger';
import { recipesService } from '../services/RecipesService.js'
import RecipeCard from '../components/RecipeCard.vue';



export default {
  setup() {
    const editable = ref({})
    async function getRecipes() {
      try {
        await recipesService.getRecipes()
      } catch (error) {
        logger.error(error)
        Pop.error(('[ERROR]'), error.message)
      }
    }
    onMounted(() => {
      getRecipes();
    })
    return {
      recipes: computed(() => AppState.recipes),
      getRecipes,
      editable,

      async searchRecipes() {
        try {
          await recipesService.searchRecipes(editable.value)
          this.editable = {}
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

// .move-over {
//   // transform: translateX(2vw);
// }
.move-up {
  transform: translateY(-2vh);
}

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
