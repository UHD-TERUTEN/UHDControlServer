import Vuetify from 'vuetify'
import { createLocalVue, mount } from '@vue/test-utils'
import FileAccessRejectLog from '@/pages/FileAccessRejectLog'
import axios from '@/axios'
import moxios from 'moxios'

const generateData = (number) => 
  [...Array(10).keys()].map(x => {
    const idx = (number - 1) * 10 + x + 1
    return ({
      agentId: idx,
      date: `2021-03-23 23:${idx}`,
      programName: `programName ${idx}`,
      details: `details ${idx}`,
    })
  })

describe('FileAccessRejectLog component tests', () => {
  const localVue = createLocalVue()
  let wrapper

  beforeEach(() => {
    const vuetify = new Vuetify()
    wrapper = mount(FileAccessRejectLog, {
      localVue,
      vuetify,
    })
    moxios.install(axios)
  })

  afterEach(() => {
    moxios.uninstall(axios)
  })

  it('should be a Vue instance', () => {
    expect(wrapper.vm).toBeTruthy()
  })

  it('should return logList when the component after mounted', () => {
    wrapper.vm.next()
    moxios.wait(() => {
      const request = moxios.requests.mostRecent()
      request.respondWith({
        status: 200,
        response: generateData(1)
      })
      expect(wrapper.vm.logList.length).toBeGreaterThan(0)
    })
  })

  // it('should return true and print message if allow button is clicked', () => {
  //   wrapper.findAll('[data-test="allow"]').trigger('click')
  //   moxios.wait(() => {
  //     const request = moxios.requests.mostRecent()
  //     request.respondWith({
  //       status: 200,
  //       response: generateData(1)
  //     })
  //     expect(wrapper.vm.text).toBe('허용되었습니다')
  //     expect(wrapper.vm.snackbar).toBeTruthy()
  //   })
  // })

  // it('should return false and print message if reject button is clicked', () => {
  //   wrapper.findAll('[data-test="allow"]').trigger('click')
  //   wrapper.findAll('[data-test="reject"]').trigger('click')
  //   moxios.wait(() => {
  //     const request = moxios.requests.mostRecent()
  //     request.respondWith({
  //       status: 200,
  //       response: generateData(1)
  //     })
  //     expect(wrapper.vm.text).toBe('차단되었습니다')
  //     expect(wrapper.vm.snackbar).toBeFalsy()
  //   })
  // })

  // it('should return page 4 if next button is clicked 3 times', () => {
  //   const pagination = wrapper.findAll('[data-test="pagination"]').
  //   pination.trigger('input')
  //   pagination.trigger('input')
  //   pagination.trigger('input')
  //   moxios.wait(() => {
  //     const request = moxios.requests.mostRecent()
  //     request.respondWith({
  //       status: 200,
  //       response: generateData(4)
  //     })
  //     expect(wrapper.vm.page).toBe(4)
  //   })
  // })
})
