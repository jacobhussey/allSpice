<template>
  <header>
    <Navbar />
  </header>
  <main>
    <router-view />
  </main>
  <footer class="bg-dark text-light">

  </footer>
  <CreateRecipe>
    <RecipeForm />
  </CreateRecipe>

  <RecipeInfo />
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
import Navbar from './components/Navbar.vue'
import RecipeInfo from './components/RecipeInfo.vue'
import CreateRecipe from './components/CreateRecipe.vue'
import RecipeForm from './components/RecipeForm.vue'


export default {
  setup() {
    return {
      appState: computed(() => AppState)
    }
  },
  components: { Navbar, RecipeInfo, CreateRecipe, RecipeForm }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";

:root {
  --main-height: calc(100vh - 32px - 64px);
}


footer {
  display: grid;
  place-content: center;
  height: 32px;
}
</style>
