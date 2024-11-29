import React, { useCallback } from 'react'
import { View, StyleSheet } from 'react-native'

import { NavigationProp, ParamListBase, useNavigation } from '@react-navigation/native'
import * as Yup from 'yup'

import { BackButton, Button, Dropdown, Text, TextInput } from '@components'
import { Colors, Dimensions } from '@utils'
import { Form, Formik } from 'formik'
import Toast from 'react-native-toast-message'

const Settings = () => {
  const navigation = useNavigation<NavigationProp<ParamListBase>>();

  const validationSchema = Yup.object().shape({
    beginAge: Yup.number().typeError("Lütfen sayı girin").required('Alt yaş aralığı gereklidir').min(18, 'Yaşınız 18 den büyük olmalıdır'),
    endAge: Yup.number().typeError("Lütfen sayı girin").required('Üst yaş aralığı gereklidir').max(70, 'Yaşınız 70 den küçük olmalıdır'),
    city: Yup.string().required('Şehir gereklidir'),
    gender: Yup.string().required('Cinsiyet gereklidir'),
  });

  const data = [
    { label: 'Item 1', value: '1' },
    { label: 'Item 2', value: '2' },
    { label: 'Item 3', value: '3' },
    { label: 'Item 4', value: '4' },
    { label: 'Item 5', value: '5' },
    { label: 'Item 6', value: '6' },
    { label: 'Item 7', value: '7' },
    { label: 'Item 8', value: '8' },
  ];

  return (
    <View style={styles.container}>
      <BackButton onPress={() => { navigation.navigate("HomeTabs") }} />
      <Formik
        initialValues={{
          beginAge: 18,
          endAge: 70,
          city: '',
          gender: '',
        }}
        validationSchema={validationSchema}
        onSubmit={(values, { setSubmitting }) => {
          Toast.show({
            type: 'success',
            text1: 'Success',
            text2: 'Your settings have been saved successfully! 💁'
          });

        }}>
        {({
          handleChange,
          handleBlur,
          handleSubmit,
          setFieldValue,
          values,
          errors,
          touched,
        }) => (
          <View style={styles.formContainer}>
            <View style={styles.inputContainer}>
              <TextInput
                label="Begin Age"
                onChangeText={handleChange('beginAge')}
                onBlur={handleBlur('beginAge')}
                value={values.beginAge.toString()}
                touched={touched.beginAge}
                errorMessage={errors.beginAge}
              />
              <TextInput
                label="End Age"
                onChangeText={handleChange('endAge')}
                onBlur={handleBlur('endAge')}
                value={values.endAge.toString()}
                touched={touched.endAge}
                errorMessage={errors.endAge}
              />

              <Dropdown
                onFocus={() => { }}
                label="City"
                search={true}
                value={values.city}
                onChange={(item) => setFieldValue("city", item.value)} 
                onBlur={() => handleBlur("city")} 
                errorMessage={errors.city}
              />

            </View>
            <View style={styles.buttonContainer}>
              <Button
                onPress={() => handleSubmit()}>
                <Text color={Colors.white} fontWeight='bold' fontSize={Dimensions.medium}>Save</Text>
              </Button>
            </View>
          </View>
        )}
      </Formik>
    </View>
  )
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: Colors.white,
    padding: Dimensions.large,
    gap: Dimensions.large,
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
})

Settings.displayName = 'Settings'
export default Settings