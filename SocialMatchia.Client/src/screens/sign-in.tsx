import React from 'react';
import { Image, KeyboardAvoidingView, Platform, StyleSheet, Text, View, TouchableOpacity } from 'react-native';
import { NavigationProp, ParamListBase, useNavigation } from '@react-navigation/native';
import { FontAwesomeIcon } from '@fortawesome/react-native-fontawesome';
import { faEye, faEyeSlash } from '@fortawesome/free-solid-svg-icons';
import Toast from 'react-native-toast-message';
import { TextInput } from 'react-native-paper';
import { Formik } from 'formik';
import * as Yup from 'yup';

import { Colors } from '@utils';
import { Button } from '@components';

const SignInSchema = Yup.object().shape({
  email: Yup.string().email('Invalid email').required('Required'),
  password: Yup.string().min(6, 'Too Short!').required('Required'),
});

const SignIn = () => {
  const [isPasswordVisible, setIsPasswordVisible] = React.useState(true);
  const navigation = useNavigation<NavigationProp<ParamListBase>>();

  const handleClick = () => {
    Toast.show({
      type: 'success',
      text1: 'Success',
      text2: 'You have successfully signed in!'
    });
    navigation.navigate('Home');
  };

  return (
    <KeyboardAvoidingView behavior={Platform.OS === 'ios' ? 'padding' : 'height'} style={{ flex: 1 }}>
      <View style={styles.container}>
        <Image style={styles.logo} source={require('@assets/images/logo.png')} />
        <Text style={styles.boldText}>Sign in to continue</Text>
        <Formik
          initialValues={{ email: '', password: '' }}
          validationSchema={SignInSchema}
          onSubmit={values => {
            handleClick();
          }}
        >
          {({ handleChange, handleBlur, handleSubmit, values, errors, touched }) => (
            <View style={styles.inputContainer}>
              <TextInput
                mode="outlined"
                label="Email"
                onChangeText={handleChange('email')}
                onBlur={handleBlur('email')}
                value={values.email}
                error={touched.email && !!errors.email}
              />
              {touched.email && errors.email && <Text style={styles.errorText}>{errors.email}</Text>}
              <TextInput
                mode="outlined"
                label="Password"
                secureTextEntry={isPasswordVisible}
                onChangeText={handleChange('password')}
                onBlur={handleBlur('password')}
                value={values.password}
                error={touched.password && !!errors.password}
                right={
                  <TextInput.Icon
                    icon={isPasswordVisible ? "eye-off" : "eye"}
                    onPress={() => setIsPasswordVisible(!isPasswordVisible)}
                  />
                }
              />
              {touched.password && errors.password && <Text style={styles.errorText}>{errors.password}</Text>}
              <Button style={styles.button} onPress={()=>handleSubmit()}>
                <Text style={styles.buttonText}>Sign In</Text>
              </Button>
            </View>
          )}
        </Formik>
        <TouchableOpacity style={styles.signUp} onPress={() => navigation.navigate("SignUp")}>
          <Text style={styles.signUpText}> Don't have an account? <Text style={styles.signUpLink}>Sign Up</Text></Text>
        </TouchableOpacity>
      </View>
    </KeyboardAvoidingView>
  );
};

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
    gap: 20,
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
    minWidth: '100%',
    backgroundColor: Colors.red.main,
    borderRadius:10,
    borderWidth: 0,
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
    alignItems: 'center',
  },
  signUpText: {
    fontSize: 14,
  },
  signUpLink: {
    color: Colors.red.main,
    fontWeight: 'bold',
  },
  errorText: {
    color: 'red',
    fontSize: 12,
    marginTop: -15,
    marginBottom: 10,
    textAlign: 'left',
    alignSelf: 'stretch',
  },
});

SignIn.displayName = 'SignIn';
export default SignIn;
