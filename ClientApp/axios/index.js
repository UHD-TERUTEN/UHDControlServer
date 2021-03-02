import axios from 'axios'

export default axios.create({
  baseURL: 'http://localhost:50598/api/'
})

// intercepter로 error handling 가능?