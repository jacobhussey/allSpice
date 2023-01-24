<template>
    <div>
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

</template>

<script>

export default {

    props: {
        recipe: {
            type: Object,
            required: true
        }
    },

    setup() {

        return {};

    }
}
</script>

<style>
.recipe-img {
    height: 25vh;
    width: 40vh;
}
</style>