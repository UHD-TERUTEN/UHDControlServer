import Vuetify from 'vuetify'
import { mount, createLocalVue } from '@vue/test-utils'
import FileAccessRejectLog from '@/pages/FileAccessRejectLog'
import axios from '../axios'

const generateData = (number) => 
  [...Array(10).keys()].map(x => {
    const idx = (number - 1) * 10 + x + 1
    return ({
      agentId: idx,
      dateTime: `2021-03-23 23:${idx}`,
      programName: `programName ${idx}`,
      details: `details ${idx}`,
    })
  })

jest.mock('axios')

describe('FileAccessRejectLog component tests', () => {
  const localVue = createLocalVue()
  let wrapper

  beforeEach(() => {
    const vuetify = new Vuetify()
    wrapper = mount(FileAccessRejectLog, {
      localVue,
      vuetify,
    })
  })

  it('should be a Vue instance', () => {
    expect(wrapper.vm).toBeTruthy()
  })

  it('should return logList when the component after mounted', () => {
    expect(wrapper.vm.logList.length).toBeGreaterThan(0)
  })

  it('should return true and print message if allow button is clicked', () => {
    wrapper.find('.v-btn.allow').trigger('click')
    expect(wrapper.vm.text).toBe('허용되었습니다')
    expect(wrapper.vm.snackbar).toBeTruthy()
  })

  it('should return false and print message if reject button is clicked', () => {
    wrapper.find('.v-btn.allow').trigger('click')
    wrapper.find('.v-btn.reject').trigger('click')
    expect(wrapper.vm.text).toBe('차단되었습니다')
    expect(wrapper.vm.snackbar).toBeFalsy()
  })

  it('should return page 4 if next button is clicked 3 times', () => {
    const pagination = wrapper.find('.v-pagination')
    pagination.trigger('input')
    pagination.trigger('input')
    pagination.trigger('input')
    expect(wrapper.vm.page).toBe(4)
  })
})
