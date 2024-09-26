import React from 'react';
import {
  View,
  Text,
  StyleSheet,
  TouchableOpacity,
  ScrollView,
} from 'react-native';
import {useWizard} from 'react-use-wizard';
import {FontAwesomeIcon} from '@fortawesome/react-native-fontawesome';
import {faChevronLeft} from '@fortawesome/free-solid-svg-icons';
import {Formik} from 'formik';
import * as Yup from 'yup';

import {TextInput, Button} from '@components';
import {Colors, Fonts} from '@utils';
import { useNavigation } from '@react-navigation/native';

const EmailAndPassword = () => {
  const {nextStep,previousStep} = useWizard();
  const navigation = useNavigation();

  const validationSchema = Yup.object().shape({
    email: Yup.string().email('Geçerli bir e-posta adresi girin').required('E-posta adresi gereklidir'),
    password: Yup.string().min(6, 'Şifre en az 6 karakter olmalıdır').required('Şifre gereklidir'),
    rePassword: Yup.string()
      .oneOf([Yup.ref('password')], 'Şifreler eşleşmiyor')
      .required('Şifre tekrarı gereklidir'),
  });

  return (
    <View style={styles.container}>
      <TouchableOpacity onPress={previousStep} style={styles.backButton}>
        <FontAwesomeIcon
          icon={faChevronLeft}
          size={20}
          color={Colors.red.main}
        />
      </TouchableOpacity>
      <ScrollView contentContainerStyle={styles.scrollContent}>
        <Text style={styles.title}>E-posta ve Şifre</Text>
        <Formik
          initialValues={{
            email: '',
            password: '',
            rePassword: '',
          }}
          validationSchema={validationSchema}
          onSubmit={(values, {setSubmitting}) => {
            navigation.navigate("Home" as never)
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
                  label="E-posta"
                  onChangeText={handleChange('email')}
                  onBlur={handleBlur('email')}
                  value={values.email}
                  error={touched.email && errors.email}
                />
                <TextInput
                  label="Şifre"
                  onChangeText={handleChange('password')}
                  onBlur={handleBlur('password')}
                  value={values.password}
                  error={touched.password && errors.password}
                  secureTextEntry
                />
                <TextInput
                  label="Şifre Tekrarı"
                  onChangeText={handleChange('rePassword')}
                  onBlur={handleBlur('rePassword')}
                  value={values.rePassword}
                  error={touched.rePassword && errors.rePassword}
                  secureTextEntry
                />
              </View>
              <View style={styles.buttonContainer}>
                <Button
                  onPress={() => handleSubmit()}
                  style={styles.registerButton}>
                  <Text style={styles.buttonText}>Kayıt Ol</Text>
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
    fontFamily: Fonts.bold,
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
  registerButton: {
    backgroundColor: Colors.red.main,
    borderRadius: 8,
    padding: 15,
    alignItems: 'center',
  },
  buttonText: {
    color: Colors.white,
    fontSize: 16,
    fontWeight: 'bold',
  },
});

EmailAndPassword.displayName = 'EmailAndPassword';
export default EmailAndPassword;