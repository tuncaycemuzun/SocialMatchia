import { Button } from '@components';
import React from 'react'
import { View, Text } from 'react-native'

import { useWizard } from 'react-use-wizard';


const Gender = () => {
  const { nextStep } = useWizard();
  return (
    <View>
      <Text>Gender</Text>
      <Button onPress={()=>nextStep()}><Text>Next</Text></Button>
    </View>
  )
}

Gender.displayName = "Gender"
export default Gender