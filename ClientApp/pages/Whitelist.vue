<template>
    <div id="app">
        <v-app id="inspire">
            <div>
                <v-alert text color="info">
                    <h3 class="headline">
                        White List
                    </h3>
                    <td v-for='info in object' 
                    :key='info.name'></td>
                    <div>
                        <p> 현재 화이트 리스트 버전 : {{object.CurVer}}</p>
                        <p> 현재 버전 업데이트 시각 : {{object.VerUT}}</p>
                        <p> 마지막 배포 시각 : {{object.LastSpreadT}}</p>
                    </div>
                    <v-divider class="my-4 info" style="opacity: 0.22"></v-divider>
                    <v-row align="center" no-gutters>
                    <v-spacer></v-spacer>
                    <v-col class="shrink">
                    <v-btn color="info" outlined>
                        배포하기
                    </v-btn>
                    </v-col>
                    </v-row>
                </v-alert>
            </div>
        </v-app>
    </div>
</template>

<style>
</style>

<script>
export default {
  data () {
    return {
      object: {
            CurVer : "1.2.3",
            VerUT : "2021.02.24 21:25",
            LastSpreadT : "2021.02.25 00:24"
        }
      
    }
  }
}
</script>
