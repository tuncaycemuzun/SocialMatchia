import React from 'react'
import { Image, StyleSheet, Text, View } from 'react-native'
import { Link, NavigationProp, ParamListBase, useNavigation } from '@react-navigation/native'

import { FontAwesomeIcon } from '@fortawesome/react-native-fontawesome'
import { faFacebook, faGoogle, faAppStore } from '@fortawesome/free-brands-svg-icons'

import { Colors, Dimensions } from '@utils'
import { Button } from '@components'

const SignUp = () => {
  const navigation = useNavigation<NavigationProp<ParamListBase>>()
  return (
    <View style={styles.container}>
      <View style={styles.signUpInfoContainer}>
        <Image style={styles.logo} source={require('@assets/images/logo.png')}></Image>
        <Text style={styles.boldText}>Sign up to continue</Text>
        <Button style={styles.button}  onPress={() => navigation.navigate("EmailSignUp")}>
          <Text style={styles.buttonText}>Continue with email</Text>
        </Button>
        <Link to="/SignIn">
          <Text style={styles.signInText}>Already have an account? <Text style={styles.signInLink}>Sign In</Text></Text>
        </Link>
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
          <View>
            <Text style={styles.bottomText}>Term of use</Text>
          </View>
          <View>
            <Text style={styles.bottomText}>Privacy Policy</Text>
          </View>
        </View>
      </View>
    </View>
  )
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: 'white',
    padding: Dimensions.xxLarge,
  },
  signUpInfoContainer: {
    flex: 2,
    justifyContent: 'flex-start',
    alignItems: 'center',
    paddingTop: Dimensions.xxxLarge,
    gap: Dimensions.xxLarge,
  },
  logo: {
    height: 150,
    resizeMode: 'contain',
  },
  boldText: {
    fontSize: Dimensions.medium,
    fontWeight: 'bold',
  },
  button: {
    width: '100%',
    backgroundColor: Colors.red.main,
    borderRadius: Dimensions.medium,
    padding: Dimensions.medium,
    justifyContent: 'center',
    alignItems: 'center',
  },
  buttonText: {
    color: Colors.white,
    fontWeight: 'bold',
    fontSize: Dimensions.medium,  
  },
  otherInfoContainer: {
    flex: 1,
    gap: Dimensions.xxLarge,
  },
  otherOptionContainer: {
    borderBottomColor: 'black',
    borderBottomWidth: StyleSheet.hairlineWidth,
    position: 'relative'
  },
  otherOptionText: {
    position: 'absolute',
    backgroundColor: 'white',
    paddingHorizontal: Dimensions.xSmall,
    top: -10,
    left: '50%',
    transform: [{ translateX: -50 }],
    color: 'black',
  },
  providers: {
    display: 'flex',
    flexDirection: 'row',
    justifyContent: 'space-between',
    gap: Dimensions.medium,
    paddingHorizontal: Dimensions.medium,
  },
  provider: {
    borderWidth: 1,
    borderColor: Colors.lightGray,
    borderRadius: Dimensions.xSmall,
    padding: Dimensions.small
  },
  bottomContainer: {
    display: 'flex',
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'flex-end',
    flex: 1,
    paddingHorizontal: Dimensions.medium
  },
  bottomText: {
    color: Colors.red.main,
    fontSize: Dimensions.normal,
  },
  signIn: {
    left: 0,
    right: 0,
    alignItems: 'center'
  },
  signInText: {
    fontSize: Dimensions.normal,
    color: Colors.black
  },
  signInLink: {
    color: Colors.red.main,
    fontWeight: 'bold'
  },
})

SignUp.displayName = 'SignUp'
export default SignUp