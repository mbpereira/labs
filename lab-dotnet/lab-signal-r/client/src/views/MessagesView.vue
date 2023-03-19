<script>
export default {
  name: "MessagesView",
  data() {
    return {
      message: '',
      name: ''
    }
  },
  methods: {
    sendMessage() {
      if (this.name.length === 0) {
        alert('Digite seu nome');
        return;
      }
      this.$store.commit('addMessage', { from: this.name, content: this.message });
      this.$refs.messageInput.focus();
      this.message = '';
    }
  },
  updated() {
    this.$refs.chatBody.scroll(0, this.$refs.chatBody.scrollHeight)
  }
}
</script>

<template>
  <div class="chat">
    <div class="chat--user-identification">
      <input type="text" v-model="name" placeholder="Digite seu nome">
    </div>
    <div class="chat--body" ref="chatBody">
      <div class="chat--message" :key="index" v-for="(message, index) in $store.state.messages">
        <h3 class="chat--message-from">{{ message.from }} disse:</h3>
        <span class="chat--message-content">{{ message.content }}</span>
      </div>
    </div>
    <div class="chat--message-actions">
      <input type="text" ref="messageInput" v-model="message" placeholder="Digite alguma coisa...">
      <button type="button" @click="sendMessage">Enviar</button>
    </div>
  </div>
</template>
<style scoped>
.chat {
  height: 400px;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
}

.chat--body {
  overflow-y: scroll;
}

.chat--message {
  display: flex;
  flex-direction: column;
}
</style>