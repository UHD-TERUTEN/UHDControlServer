<template>
  <div>

    <v-simple-table>
      <template v-slot:default>
        <thead>
          <tr>
            <th class="text-left">에이전트 번호</th>
            <th class="text-left">날짜</th>
            <th class="text-left">크기</th>
            <th class="text-left">다운로드</th>
          </tr>
        </thead>
        <tbody>
          <tr
            v-for="item in logList"
            :key="item.name"
          >
            <td>{{ item.agentId }}</td>
            <td>{{ item.date }}</td>
            <td>{{ item.size }}</td>
            <td>
              <v-btn
                color="primary"
                elevation="1"
              >
                다운로드
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
          date: '2021-11-01',
          size: "10.2 MB",
        },
        {
          agentId: 1,
          date: '2021-11-01',
          size: "10.3 MB",
        },
        {
          agentId: 2,
          date: '2021-11-01',
          size: "10.4 MB",
        },
        {
          agentId: 3,
          date: '2021-10-31',
          size: "10.5 MB",
        },
        {
          agentId: 3,
          date: '2021-10-31',
          size: "10.6 MB",
        },
        {
          agentId: 1,
          date: '2021-10-31',
          size: "10.7 MB",
        },
        {
          agentId: 1,
          date: '2021-10-30',
          size: "10.8 MB",
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
          date: '2021-11-01',
          size: "10.2 MB",
        },
        {
          agentId: 10,
          date: '2021-11-01',
          size: "10.3 MB",
        },
        {
          agentId: 20,
          date: '2021-11-01',
          size: "10.4 MB",
        },
        {
          agentId: 30,
          date: '2021-10-31',
          size: "10.5 MB",
        },
        {
          agentId: 30,
          date: '2021-10-31',
          size: "10.6 MB",
        },
        {
          agentId: 10,
          date: '2021-10-31',
          size: "10.7 MB",
        },
        {
          agentId: 10,
          date: '2021-10-30',
          size: "10.8 MB",
        },
      ]
    }
  }
}
</script>