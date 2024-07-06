import React from 'react'
import { Image, StyleSheet, Text, View } from 'react-native'
import { NavigationProp, ParamListBase, useNavigation } from '@react-navigation/native'

import { FontAwesomeIcon } from '@fortawesome/react-native-fontawesome'
import { faFacebook, faGoogle, faAppStore } from '@fortawesome/free-brands-svg-icons'

import { Colors } from '@utils'
import { Button } from 'react-native-paper'



const SignUp = () => {
  const navigation = useNavigation<NavigationProp<ParamListBase>>()
  return (
    <View style={styles.container}>
      <View style={styles.signUpInfoContainer}>
        <Image style={styles.logo} source={require('@assets/images/logo.png')}></Image>
        <Text style={styles.boldText}>Sign up to continue</Text>
        <Button style={styles.button} onPress={() => navigation.navigate("EmailSignUp")}>
          <Text style={styles.buttonText}>Continue with email</Text>
        </Button>
        <Button style={styles.signIn} onPress={() => navigation.navigate('SignIn')}>
          <Text style={styles.signInText}> Don't have an account? <Text style={styles.signInLink}>Sign In</Text></Text>
        </Button>
      </View>
      <View style={styles.otherInfoContainer}>
        <View style={styles.otherOptionContainer}>
          <Text style={styles.otherOptionText}>or sign up with</Text>
        </View>
        <View style={styles.providers}>
          <View style={styles.provider}>
            <FontAwesomeIcon icon={faFacebook} size={30} color={Colors.red.main} />
          </View>
          <View style={styles.provider}>
            <FontAwesomeIcon icon={faGoogle} size={30} color={Colors.red.main} />
          </View>
          <View style={styles.provider}>
            <FontAwesomeIcon icon={faAppStore} size={30} color={Colors.red.main} />
          </View>
        </View>
        <View style={styles.bottomContainer}>
          <Button>
            <Text style={styles.bottomText}>Term of use</Text>
          </Button>
          <Button>
            <Text style={styles.bottomText}>Privacy Policy</Text>
          </Button>
        </View>
      </View>
    </View>
  )
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: 'white',
    padding: 40,
  },
  signUpInfoContainer: {
    flex: 2,
    justifyContent: 'flex-start',
    alignItems: 'center',
    paddingTop: 50,
    gap: 40,
  },
  logo: {
    height: 150,
    resizeMode: 'contain',
  },
  boldText: {
    fontSize: 16,
    fontWeight: 'bold',
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
  otherInfoContainer: {
    flex: 1,
    gap: 40,
  },
  otherOptionContainer: {
    borderBottomColor: 'black',
    borderBottomWidth: StyleSheet.hairlineWidth,
    position: 'relative'
  },
  otherOptionText: {
    position: 'absolute',
    backgroundColor: 'white',
    paddingHorizontal: 10,
    top: -10,
    left: '50%',
    transform: [{ translateX: -50 }],
    color: 'black',
  },
  providers: {
    display: 'flex',
    flexDirection: 'row',
    justifyContent: 'space-between',
    gap: 20,
    paddingHorizontal: 20,
  },
  provider: {
    borderWidth: 1,
    borderColor: Colors.lightGray,
    borderRadius: 10,
    padding: 10
  },
  bottomContainer: {
    display: 'flex',
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'flex-end',
    flex: 1,
    paddingHorizontal: 20,
    gap: 10
  },
  bottomText: {
    color: Colors.red.main
  },
  signIn: {
    left: 0,
    right: 0,
    alignItems: 'center'
  },
  signInText: {
    fontSize: 14,
    color: Colors.black
  },
  signInLink: {
    color: Colors.red.main,
    fontWeight: 'bold'
  },
})

SignUp.displayName = 'SignUp'
export default SignUp