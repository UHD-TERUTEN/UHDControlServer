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
                @click="updateLog(item)"
              >
                허용
              </v-btn>
              <v-btn
                v-else
                color="error"
                elevation="1"
                @click="updateLog(item)"
              >
                차단
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
      axios.get(`/file-access-reject-logs?page=${this.page}`)
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
    getInquiries(id) {
      axios.get(`/file-access-reject-logs/${id}/inquiries/1`) // TODO: get all inquiries
        .then(res => {
          console.log(res)
        })
        .catch(err => console.log(err))
    },
    updateLog(item) {
      item.isAllowed = !item.isAllowed;
      axios.put(`/file-access-reject-logs/${item.id}`, item)  // 405 Method Not Allowed
        .then(res => {
          console.log(res)
        })
        .catch(err => console.log(err));
    }
  }
}
</script>