<template>
    <div class="modal fade" id="create-recipe-modal" tabindex="-1" aria-labelledby="createRecipeModal"
        aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content bg-light">
                <slot></slot>
            </div>
        </div>
    </div>
</template>

<script>

export default {



    setup() {

        return {};

    }
}
</script>

<style>

</style>