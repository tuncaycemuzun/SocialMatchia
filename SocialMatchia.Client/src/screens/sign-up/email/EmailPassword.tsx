import React from 'react';
import {
  View,
  StyleSheet,
  ScrollView,
} from 'react-native';
import { useWizard } from 'react-use-wizard';
import { Formik } from 'formik';
import * as Yup from 'yup';

import { TextInput, Button, BackButton, Text } from '@components';
import { Colors, Dimensions } from '@utils';
import { NavigationProp, ParamListBase, useNavigation } from '@react-navigation/native';
import { Title } from './components';
import Toast from 'react-native-toast-message';

const EmailAndPassword = () => {
  const { previousStep } = useWizard();
  const navigation = useNavigation<NavigationProp<ParamListBase>>();

  const validationSchema = Yup.object().shape({
    email: Yup.string().email('Geçerli bir e-posta adresi girin').required('E-posta adresi gereklidir'),
    password: Yup.string().min(6, 'Şifre en az 6 karakter olmalıdır').required('Şifre gereklidir'),
    rePassword: Yup.string()
      .oneOf([Yup.ref('password')], 'Şifreler eşleşmiyor')
      .required('Şifre tekrarı gereklidir'),
  });

  return (
    <View style={styles.container}>
      <BackButton onPress={previousStep} />
      <ScrollView contentContainerStyle={styles.scrollContent}>
        <Title title="E-mail and password" />
        <Formik
          initialValues={{
            email: '',
            password: '',
            rePassword: '',
          }}
          validationSchema={validationSchema}
          onSubmit={(values, { setSubmitting }) => {
            Toast.show({
              type: 'success',
              text1: 'Success',
              text2: 'You have successfully registered. You can log in now! 💁'
            });
            navigation.navigate("SignIn");
          }}>
          {({
            handleChange,
            handleBlur,
            handleSubmit,
            values,
            errors,
            touched,
          }) => (
            <View style={styles.formContainer}>
              <View style={styles.inputContainer}>
                <TextInput
                  label="E-mail"
                  onChangeText={handleChange('email')}
                  onBlur={handleBlur('email')}
                  value={values.email}
                  touched={touched.email}
                  errorMessage={errors.email}
                />
                <TextInput
                  label="Password"
                  onChangeText={handleChange('password')}
                  onBlur={handleBlur('password')}
                  value={values.password}
                  touched={touched.password}
                  errorMessage={errors.password}
                  secureTextEntry
                />
                <TextInput
                  label="Re-Password"
                  onChangeText={handleChange('rePassword')}
                  onBlur={handleBlur('rePassword')}
                  value={values.rePassword}
                  touched={touched.rePassword}
                  errorMessage={errors.rePassword}
                  secureTextEntry
                />
              </View>
              <View style={styles.buttonContainer}>
                <Button
                  onPress={() => handleSubmit()}>
                  <Text color={Colors.white} fontWeight='bold' fontSize={Dimensions.medium}>Register</Text>
                </Button>
              </View>
            </View>
          )}
        </Formik>
      </ScrollView>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: Colors.white,
  },
  scrollContent: {
    flexGrow: 1,
  },
  backButton: {
    position: 'absolute',
    borderStyle: 'solid',
    borderWidth: 1,
    padding: 10,
    borderRadius: 10,
    borderColor: Colors.lightGray,
    zIndex: 100,
  },
  title: {
    marginTop: 70,
    fontSize: 24,
    fontWeight: 'bold',
    marginBottom: 10,
    color: Colors.black,
  },
  formContainer: {
    width: '100%',
    flex: 1,
    justifyContent: 'space-between',
  },
  inputContainer: {
    width: '100%',
    gap: 10,
  },
  buttonContainer: {
    width: '100%',
    marginBottom: 20,
  },
  buttonText: {
    color: Colors.white,
    fontSize: 16,
    fontWeight: 'bold',
  },
});

EmailAndPassword.displayName = 'EmailAndPassword';
export default EmailAndPassword;