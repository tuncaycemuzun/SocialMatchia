import React from 'react'
import { Image, KeyboardAvoidingView, Platform, StyleSheet, Text, TextInput, View } from 'react-native'
import { NavigationProp, ParamListBase, useNavigation } from '@react-navigation/native'
import { FontAwesomeIcon } from '@fortawesome/react-native-fontawesome';
import { faEye, faEyeSlash } from '@fortawesome/free-solid-svg-icons';

import { Colors } from '@utils';
import { Button } from '@components';
import Toast from 'react-native-toast-message';

const SignIn = () => {
  const [isPasswordVisible, setIsPasswordVisible] = React.useState(true)
  const navigation = useNavigation<NavigationProp<ParamListBase>>()
  
  const handleClick = () => {
    console.log("test")
    Toast.show({
      type: 'success',
      text1: 'Success',
      text2: 'You have successfully signed in!'
    });
    navigation.navigate('Home')
  }

  return (
    <KeyboardAvoidingView behavior={Platform.OS === 'ios' ? 'padding' : 'height'} style={{ flex: 1 }}>
      <View style={styles.container}>
        <Image style={styles.logo} source={require('@assets/images/logo.png')}></Image>
        <Text style={styles.boldText}>Sign in to continue</Text>
        <View style={styles.inputContainer}>
          <TextInput placeholder="Email" style={styles.input}></TextInput>
          <View style={{ position: 'relative' }}>
            <TextInput secureTextEntry={isPasswordVisible} placeholder="Password" style={styles.input}></TextInput>
            <Button style={{
              position: 'absolute',
              right: 10,
              top: 15,
              zIndex: 10
            }} onPress={() => setIsPasswordVisible(!isPasswordVisible)}>
              <FontAwesomeIcon icon={isPasswordVisible ? faEye : faEyeSlash} size={20} color={Colors.lightGray} />
            </Button>
            <Text style={{ color: 'black' }}>
              {isPasswordVisible}
            </Text>
          </View>
        </View>
        <Button style={styles.button} onPress={() => handleClick()}>
          <Text style={styles.buttonText}>Sign In</Text>
        </Button>
        <Button style={styles.signUp} onPress={() => navigation.navigate("SignUp")}>
          <Text style={styles.signUpText}> Don't have an account? <Text style={styles.signUpLink}>Sign Up</Text></Text>
        </Button>
      </View>
    </KeyboardAvoidingView>
  )
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: Colors.white,
    paddingHorizontal: 40,
    gap: 40,
    alignItems: 'center',
    justifyContent: 'center',
  },
  logo: {
    height: 150,
    resizeMode: 'contain',
  },
  boldText: {
    fontSize: 16,
    fontWeight: 'bold',
  },
  inputContainer: {
    width: '100%',
    gap: 20
  },
  input: {
    height: 50,
    width: '100%',
    backgroundColor: Colors.white,
    borderWidth: 1,
    borderColor: Colors.lightGray,
    borderRadius: 10,
    padding: 10,
  },
  button: {
    height: 50,
    width: '100%',
    backgroundColor: Colors.red.main,
    borderRadius: 15,
    alignItems: 'center',
    justifyContent: 'center',
  },
  buttonText: {
    color: Colors.white,
    fontWeight: 'bold',
  },
  signUp: {
    left: 0,
    right: 0,
    alignItems: 'center'
  },
  signUpText: {
    fontSize: 14
  },
  signUpLink: {
    color: Colors.red.main,
    fontWeight: 'bold'
  },
})

SignIn.displayName = 'SignIn'
export default SignIn