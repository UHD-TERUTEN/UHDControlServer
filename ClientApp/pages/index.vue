<template>
  <v-container>
    <v-row justify="space-around">
      <v-col justify="space-around">
        <v-card>
          <v-parallax height="400" src="https://cdn.vuetifyjs.com/images/parallax/material.jpg" />

          <v-card-text>
            <div class="font-weight-bold ml-8 mb-2">
              Features
            </div>

            <v-timeline
              align-top
              dense
            >
              <v-timeline-item
                v-for="message in messages"
                :key="message.time"
                :color="message.color"
                small
              >
                <div>
                  <div class="font-weight-normal">
                    <strong>{{ message.from }}</strong>
                  </div>
                  <div>{{ message.message }}</div>
                </div>
              </v-timeline-item>
            </v-timeline>
          </v-card-text>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
  export default {
    data: () => ({
      messages: [
        {
          from: '파일 접근 차단 로그',
          message: '에이전트 PC 환경에서 발생한 파일 접근 차단 로그 관리',
          color: 'cyan lighten-3',
        },
        {
          from: '화이트리스트',
          message: '화이트리스트 버전 관리 및 에이전트 PC 환경에 배포',
          color: 'orange darken-1',
        },
        {
          from: '에이전트 시스템 로그',
          message: '에이전트 PC 환경에서 발생한 시스템 로그 확인',
          color: 'lime lighten-1',
        },
      ],
    }),
  }
</script>