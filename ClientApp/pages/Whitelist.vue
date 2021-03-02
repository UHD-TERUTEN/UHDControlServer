<template>
  <v-alert text color="info">
    <v-row>
      <v-col><h3 class="headline">Whitelist</h3></v-col>
    </v-row>
    <v-row>
      <v-col cols="3">현재 화이트리스트 버전: </v-col>
      <v-col>{{ latestWhitelist.version }}</v-col>
    </v-row>
    <v-row>
      <v-col cols="3">현재 버전 업데이트 시각: </v-col>
      <v-col>{{ latestWhitelist.lastUpdated }}</v-col>
    </v-row>
    <v-row>
      <v-col cols="3">마지막 배포 시각: </v-col>
      <v-col>{{ latestWhitelist.lastDistributed }}</v-col>
    </v-row>
    <v-divider class="my-4 info" style="opacity: 0.22" />
    <v-row align="center" no-gutters>
      <v-spacer />
      <v-col class="shrink">
        <v-btn color="primary" elevation="1"> 배포하기 </v-btn>
      </v-col>
    </v-row>
  </v-alert>
</template>

<script>
import axios from '../axios'

export default {
  data() {
    return {
      latestWhitelist: {
        version: "1.2.3",
        lastUpdated: "2021.02.24 21:25",
        lastDistributed: "2021.02.25 00:24",
      }
    };
  },
  created() {
    axios.get('/whitelist/latest')
      .then(res => {
        this.latestWhitelist = res.data
      })
      .catch(err => console.log(err))
  },
};
</script>
