<template>
  <v-app>

    <v-app-bar app flat color="#F5DF4D">
      <v-toolbar-title v-text="title" style="width: 300px" class="font-weight-black" />
      <v-tabs color="#939597" right>
        <v-tab :to="'/'">
          <h3 class="font-weight-bold">홈</h3>
        </v-tab>
        <v-tab :to="'/FileAccessRejectLog'">
          <h3 class="font-weight-bold">파일 접근 차단 로그</h3>
        </v-tab>
        <v-tab :to="'/Whitelist'">
          <h3 class="font-weight-bold">화이트리스트</h3>
        </v-tab>
        <v-tab :to="'/SystemLog'">
          <h3 class="font-weight-bold">에이전트 시스템 로그</h3>
        </v-tab>
      </v-tabs>
    </v-app-bar>

    <v-main>
      <v-container>
        <nuxt />
      </v-container>
    </v-main>

  </v-app>
</template>

<script>
export default {
  data () {
    return {
      title: 'UHD Control Server'
    }
  }
}
</script>
