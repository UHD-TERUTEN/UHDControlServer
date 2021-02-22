<template>
  <div>

    <v-simple-table>
      <template v-slot:default>
        <thead>
          <tr>
            <th class="text-left">에이전트 번호</th>
            <th class="text-left">시간</th>
            <th class="text-left">프로그램</th>
            <th class="text-left">내용</th>
            <th class="text-left">관련 문의</th>
            <th class="text-left">허용하기</th>
          </tr>
        </thead>
        <tbody>
          <tr
            v-for="item in logList"
            :key="item.name"
          >
            <td>{{ item.agentId }}</td>
            <td>{{ item.datetime }}</td>
            <td>{{ item.programName }}</td>
            <td>{{ item.details }}</td>
            <td>
              <v-btn
                color="primary"
                elevation="1"
                tile
              >
                확인
              </v-btn>
            </td>
            <td>
              <v-btn
                color="primary"
                elevation="1"
                tile
              >
                허용
              </v-btn>
            </td>
          </tr>
        </tbody>
      </template>
    </v-simple-table>

    <div class="text-center">
      <v-container>
        <v-row justify="center">
          <v-col cols="8">
            <v-container class="max-width">
              <v-pagination
                v-model="page"
                class="my-4"
                :length="4"
                @input="next"
              ></v-pagination>
            </v-container>
          </v-col>
        </v-row>
      </v-container>
    </div>

  </div>
</template>

<style>

</style>

<script>
export default {
  data () {
    return {
      logList: [
        {
          agentId: 1,
          datetime: '2021-11-01 13:01:11',
          programName: 'Microsoft Excel',
          details: '새 이미지.png 접근 차단',
        },
        {
          agentId: 1,
          datetime: '2021-11-01 12:58:22',
          programName: 'Microsoft Excel',
          details: '새 이미지.png 접근 차단',
        },
        {
          agentId: 2,
          datetime: '2021-11-01 12:56:33',
          programName: '한글 2012',
          details: '새 이미지.svg 접근 차단',
        },
        {
          agentId: 3,
          datetime: '2021-11-01 12:55:44',
          programName: 'Microsoft Excel',
          details: '새 이미지.png 접근 차단',
        },
        {
          agentId: 3,
          datetime: '2021-11-01 12:53:55',
          programName: 'Microsoft Excel',
          details: '새 이미지.png 접근 차단',
        },
        {
          agentId: 1,
          datetime: '2021-11-01 10:00:32',
          programName: 'Microsoft Excel',
          details: '새 이미지.png 접근 차단',
        },
        {
          agentId: 1,
          datetime: '2021-11-01 09:23:01',
          programName: 'Microsoft Excel',
          details: '새 이미지.png 접근 차단',
        },
      ],
      page: 1,
    }
  },
  methods: {
    next() {
      this.logList = [
        {
          agentId: 10,
          datetime: '2021-11-01 13:01:11',
          programName: 'Microsoft Excel',
          details: '새 이미지.png 접근 차단',
        },
        {
          agentId: 10,
          datetime: '2021-11-01 12:58:22',
          programName: 'Microsoft Excel',
          details: '새 이미지.png 접근 차단',
        },
        {
          agentId: 20,
          datetime: '2021-11-01 12:56:33',
          programName: '한글 2012',
          details: '새 이미지.svg 접근 차단',
        },
        {
          agentId: 30,
          datetime: '2021-11-01 12:55:44',
          programName: 'Microsoft Excel',
          details: '새 이미지.png 접근 차단',
        },
        {
          agentId: 30,
          datetime: '2021-11-01 12:53:55',
          programName: 'Microsoft Excel',
          details: '새 이미지.png 접근 차단',
        },
        {
          agentId: 10,
          datetime: '2021-11-01 10:00:32',
          programName: 'Microsoft Excel',
          details: '새 이미지.png 접근 차단',
        },
        {
          agentId: 10,
          datetime: '2021-11-01 09:23:01',
          programName: 'Microsoft Excel',
          details: '새 이미지.png 접근 차단',
        },
      ]
    }
  }
}
</script>