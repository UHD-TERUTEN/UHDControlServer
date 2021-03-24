import Vuetify from 'vuetify'
import { createLocalVue, mount } from '@vue/test-utils'
import SystemLog from '@/pages/SystemLog'

describe('SystemLog component tests', () => {
  const localVue = createLocalVue()
  let wrapper

  beforeEach(() => {
    const vuetify = new Vuetify()
    wrapper = mount(SystemLog, {
      localVue,
      vuetify,
    })
  })

  it('should be a Vue instance', () => {
    expect(wrapper.vm).toBeTruthy()
  })

  it('should return logList when the component after mounted', () => {
    moxios.wait(() => {
      const request = moxios.requests.mostRecent()
      request.respondWith({
        status: 200,
        response: generateData(1)
      })
      expect(wrapper.vm.logList.length).toBeGreaterThan(0)
    })
  })

  it('should throw error if download button is clicked', () => {
    const downloadButton = wrapper.findComponent({ name: 'v-btn' })
    downloadButton.trigger('click')
    moxios.wait(() => {
      const request = moxios.requests.mostRecent()
      request.respondWith({
        status: 200,
        response: generateData(1)
      })
      expect(downloadButton.text()).toBe('다운로드')
    })
  })

  it('should return page 4 if next button is clicked 3 times', () => {
    const pagination = wrapper.findComponent({ name: 'v-pagination' })
    pagination.trigger('input')
    pagination.trigger('input')
    pagination.trigger('input')
    moxios.wait(() => {
      const request = moxios.requests.mostRecent()
      request.respondWith({
        status: 200,
        response: generateData(1)
      })
      expect(wrapper.vm.page).toBe(4)
    })
  })
})
