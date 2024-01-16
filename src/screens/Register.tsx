import React from 'react'
import { Image, KeyboardAvoidingView, Platform, StyleSheet, View } from 'react-native'
import { globalStyles } from '../styles'
import { Text, TextInput } from '../components'
import Button from '../components/Button'
import { colors } from '../utils'
import { faEye, faEyeSlash } from '@fortawesome/free-solid-svg-icons'
import { faGoogle } from '@fortawesome/free-brands-svg-icons'

import { useNavigation } from '@react-navigation/native'

const Register = () => {

  const [isPasswordVisible, setIsPasswordVisible] = React.useState(true)
  const [isRePasswordVisible, setIsRePasswordVisible] = React.useState(true)

  const navigation = useNavigation()
  return (

    <KeyboardAvoidingView
      behavior={Platform.OS === 'ios' ? 'padding' : 'height'} style={[globalStyles.container, {

      }]}>
      <View style={styles.form}>

        <Text size='large'>Register</Text>
        <Image style={styles.image} source={require('../assets/images/register.png')} />
        <TextInput placeholder="Email"></TextInput>
        <TextInput placeholder="Password"
          secureTextEntry={isPasswordVisible}
          icon={isPasswordVisible ? faEyeSlash : faEye}
          iconPress={() => {
            setIsPasswordVisible(!isPasswordVisible)
          }}></TextInput>
        <TextInput placeholder="Re-Password"
          secureTextEntry={isRePasswordVisible}
          icon={isRePasswordVisible ? faEyeSlash : faEye}
          iconPress={() => {
            setIsRePasswordVisible(!isRePasswordVisible)
          }}></TextInput>
        <Button text="Register" size='medium' wFull></Button>
        <Button text="Register with Google" icon={faGoogle} iconPosition='left' textColor={colors.black} style={{ backgroundColor: colors.frenchGray }} size='medium' wFull></Button>
        <Text size='small'
          onPress={() => navigation.navigate('Login' as never)}
          style={{
            textDecorationLine: 'underline'
          }}>Do you have an account?</Text>
      </View>

    </KeyboardAvoidingView>
  )
}

const styles = StyleSheet.create({
  image: {
    width: '100%',
    height: 200
  },
  form: {
    flex: 1,
    alignItems: 'center',
    justifyContent: 'center',
    gap: 20,
    width: '100%',
    height: '100%',
  },
  leftIcon: {
    left: 0,
    padding: 10,
    position: 'absolute',
    zIndex: 1,
  },
  rightIcon: {
    right: 0,
    padding: 10,
    position: 'absolute',
    zIndex: 1,
    backgroundColor: 'red'
  }
})


export default Register