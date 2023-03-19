import { createApp } from 'vue'
import App from './App.vue'
import router from './router'

import './assets/main.css'
import store from './store'

import registerEvents from './chatEvents'

registerEvents(store)

const app = createApp(App)

app.use(router)
app.use(store)

app.mount('#app')
