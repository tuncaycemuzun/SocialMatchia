import React from 'react'
import { StyleSheet,View } from 'react-native'
import { globalStyles } from '../styles'
import { TextInput } from '../components'

const Register = () => {
  return (
    <View style={globalStyles.container}>
        <TextInput placeholder="Test"></TextInput>
    </View>
  )
}

const styles = StyleSheet.create({
  
})

export default Register