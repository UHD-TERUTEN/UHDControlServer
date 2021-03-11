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
            <td>{{ item.dateTime }}</td>
            <td>{{ item.programName }}</td>
            <td>{{ item.details }}</td>
            <td>
              <v-btn
                color="primary"
                elevation="1"
                @click="getInquiries(item.id)"
              >
                확인
              </v-btn>
            </td>
            <td>
              <v-btn
                v-if="item.isAllowed"
                color="primary"
                elevation="1"
                @click="toggleAllow(item)"
              >
                허용
              </v-btn>
              <v-btn
                v-else
                color="error"
                elevation="1"
                @click="toggleAllow(item)"
              >
                차단
              </v-btn>
              <v-snackbar
                v-model="snackbar"
                :timeout="timeout"
                right
              >
                {{ text }}
              </v-snackbar>
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
      snackbar: false,
      timeout: 2000,
      text: null
    }
  },
  created() {
    this.next()
  },
  methods: {
    next() {
      axios.get(`/file-access-reject-log?page=${this.page}`)
        .then(res => {
          console.log(res)
          this.logList = res.data
        })
        .catch(err => console.log(err))
    },
    getLog(id) {
      axios.get(`/file-access-reject-log/${id}`)
        .then(res => {
          console.log(res)
        })
        .catch(err => console.log(err))
    },
    getInquiries(id) {
      axios.get(`/file-access-reject-log/${id}/inquiries/1`) // TODO: get all inquiries
        .then(res => {
          console.log(res)
        })
        .catch(err => console.log(err))
    },
    toggleAllow(item) {
      item.isAllowed = !item.isAllowed
      this.updateLog(item)
    },
    updateLog(item) {
      axios.put('/file-access-reject-log', item)
        .then(res => {
          this.text = `${res.data.isAllowed ? '허용' : '차단' }되었습니다`
          this.snackbar = true
        })
        .catch(err => console.log(err));
    }
  }
}
</script>