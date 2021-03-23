import Vuetify from 'vuetify'
import { createLocalVue, mount } from '@vue/test-utils'
import Whitelist from '@/pages/Whitelist'

describe('Whitelist component tests', () => {
  const localVue = createLocalVue()
  let wrapper

  beforeEach(() => {
    const vuetify = new Vuetify()
    wrapper = mount(Whitelist, {
      localVue,
      vuetify,
    })
  })

  it('should be a Vue instance', () => {
    expect(wrapper.vm).toBeTruthy()
  })

  // it('should return logList when the component after mounted', () => {
  //   expect(wrapper.vm.latestWhitelist.version).toBeTruthy()
  // })

  // it('should return true if distribute button is clicked', () => {
  //   wrapper.findComponent({ name: 'v-btn' }).trigger('click')
  //   expect(wrapper.vm.snackbar).toBeTruthy()
  // })
})