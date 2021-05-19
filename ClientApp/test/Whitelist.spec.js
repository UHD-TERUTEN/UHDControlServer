import Vuetify from 'vuetify'
import { createLocalVue, mount } from '@vue/test-utils'
import Whitelist from '@/pages/Whitelist'
import axios from '@/axios'
import moxios from 'moxios'

describe('Whitelist component tests', () => {
  const localVue = createLocalVue()
  let wrapper

  beforeEach(() => {
    const vuetify = new Vuetify()
    wrapper = mount(Whitelist, {
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
    moxios.wait(() => {
      const request = moxios.requests.mostRecent()
      request.respondWith({
        status: 200,
        response: generateData(1)
      })
      expect(wrapper.vm.latestWhitelist.version).toBeTruthy()
    })
  })

  it('should return true if distribute button is clicked', () => {
    wrapper.findComponent({ name: 'v-btn' }).trigger('click')
    moxios.wait(() => {
      const request = moxios.requests.mostRecent()
      request.respondWith({
        status: 200,
        response: generateData(1)
      })
      expect(wrapper.vm.snackbar).toBeTruthy()
    })
  })
})