import { shallowMount } from '@vue/test-utils'
import Main from '@/pages'

describe('main(index) component tests', () => {
  it('should be a Vue instance', () => {
    const wrapper = shallowMount(Main)
    expect(wrapper).toMatchSnapshot()
  })
})
