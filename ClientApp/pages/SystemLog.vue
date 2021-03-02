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
            <td>{{ item.dateTime }}</td>
            <td>{{ item.size }}</td>
            <td>
              <v-btn
                color="primary"
                elevation="1"
                @click="downloadLog(id)"
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

<script>
import axios from '../axios'

export default {
  data () {
    return {
      logList: [],
      page: 1,
    }
  },
  created() {
    this.next()
  },
  methods: {
    next() {
      axios.get(`/system-logs?page=${this.page}`)
        .then(res => {
          console.log(res)
          this.logList = res.data
        })
        .catch(err => console.log(err))
    },
    getLog(id) {
      axios.get(`/file-access-reject-logs/${id}`)
        .then(res => {
          console.log(res)
        })
        .catch(err => console.log(err))
    },
    downloadLog(id) {
      // TODO: implement here
    },
  }
}
</script>