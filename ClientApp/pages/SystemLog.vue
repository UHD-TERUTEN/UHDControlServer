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
                @click="downloadLog(item)"
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
      axios.get(`/system-log?page=${this.page}`)
        .then(res => {
          console.log(res)
          this.logList = res.data
        })
        .catch(err => console.log(err))
    },
    getFileName(item) {
      const shortDate = item.date.slice(0, 10)
      return `${shortDate}.tar.gz`
    },
    downloadLog(item) {
      const fileName = this.getFileName(item)
      console.log(fileName)
      axios.get(`/system-log/${fileName}`, { responseType: 'blob' })
        .then(res => {
          const fileUrl = window.URL.createObjectURL(new Blob([res.data]))
          const fileLink = document.createElement('a')

          fileLink.href = fileUrl
          fileLink.setAttribute('download', fileName)
          document.body.appendChild(fileLink)
          fileLink.click()
        })
        .catch(err => console.log(err))
    },
  }
}
</script>
