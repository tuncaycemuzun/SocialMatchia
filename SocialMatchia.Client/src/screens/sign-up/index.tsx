import React from 'react'
import { Image, StyleSheet, View } from 'react-native'
import { Link, NavigationProp, ParamListBase, useNavigation } from '@react-navigation/native'

import { FontAwesomeIcon } from '@fortawesome/react-native-fontawesome'
import { faFacebook, faGoogle, faAppStore } from '@fortawesome/free-brands-svg-icons'

import { Colors, Dimensions } from '@utils'
import { Button, Text } from '@components'

const SignUp = () => {
  const navigation = useNavigation<NavigationProp<ParamListBase>>()
  return (
    <View style={styles.container}>
      <View style={styles.signUpInfoContainer}>
        <Image style={styles.logo} source={require('@assets/images/logo.png')}></Image>
        <Text fontWeight='bold' fontSize={Dimensions.medium}>Sign up to continue</Text>
        <Button style={styles.button} onPress={() => navigation.navigate("EmailSignUp")}>
          <Text fontWeight='bold' color={Colors.white} fontSize={Dimensions.medium}>Continue with email</Text>
        </Button>
        <Link to="/SignIn">
          <Text>Already have an account? <Text color={Colors.red.main} fontWeight='bold'>Sign In</Text></Text>
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
            <Text color={Colors.red.main}>Term of use</Text>
          </View>
          <View>
            <Text color={Colors.red.main}>Privacy Policy</Text>
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
  button: {
    width: '100%',
    backgroundColor: Colors.red.main,
    borderRadius: Dimensions.medium,
    padding: Dimensions.medium,
    justifyContent: 'center',
    alignItems: 'center',
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
  signIn: {
    left: 0,
    right: 0,
    alignItems: 'center'
  }
})

SignUp.displayName = 'SignUp'
export default SignUp