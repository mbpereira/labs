import { reactive } from "vue";
import Vuex from "vuex";

const state = reactive({
    messages: []
})

const store = new Vuex.Store({
    state,
    mutations: {
        addMessage(state, payload) {
            state.messages = [...state.messages, payload]
        }
    }
})

export default store;