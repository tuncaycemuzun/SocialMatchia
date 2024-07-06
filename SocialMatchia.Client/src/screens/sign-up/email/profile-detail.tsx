import React from 'react';
import { View, Text, StyleSheet, TouchableOpacity } from 'react-native';
import { useWizard } from 'react-use-wizard';
import { FontAwesomeIcon } from '@fortawesome/react-native-fontawesome';
import { faCalendar, faPlus } from '@fortawesome/free-solid-svg-icons';
import { Button } from 'react-native-paper';
import { Formik } from 'formik';
import * as Yup from 'yup';

import { ChoosePhotoModal, TextInput } from '@components';
import { Colors } from '@utils';

const ProfileDetail = () => {
  const { nextStep } = useWizard();
  const [isModalVisible, setModalVisible] = React.useState(false);

  const toggleModal = () => {
    setModalVisible(prevState => !prevState);
  };

  const validationSchema = Yup.object().shape({
    firstName: Yup.string().required('First name is required'),
    lastName: Yup.string().required('Last name is required'),
    birthday: Yup.string().required('Birthday date is required'),
  });

  return (
    <View style={styles.container}>
      <Text style={styles.title}>Profile details</Text>
      <TouchableOpacity style={styles.photoContainer} onPress={toggleModal}>
        <FontAwesomeIcon icon={faPlus} size={30} color={Colors.lightGray} />
      </TouchableOpacity>
      <Formik
        initialValues={{ firstName: '', lastName: '', birthday: '' }}
        validationSchema={validationSchema}
        onSubmit={(values) => {
          console.log(values);
          nextStep();
        }}
      >
        {({ handleChange, handleBlur, handleSubmit, values, errors, touched }) => (
          <View style={{ flex: 3, gap: 20, width: '100%' }}>
            <TextInput
              label="First name"
              onChangeText={handleChange('firstName')}
              onBlur={handleBlur('firstName')}
              value={values.firstName}
            />
            {touched.firstName && errors.firstName && <Text style={styles.error}>{errors.firstName}</Text>}
            <TextInput
              label="Last name"
              onChangeText={handleChange('lastName')}
              onBlur={handleBlur('lastName')}
              value={values.lastName}
            />
            {touched.lastName && errors.lastName && <Text style={styles.error}>{errors.lastName}</Text>}
            <TouchableOpacity onPress={() => { /* Date picker logic */ }}>
              <View style={styles.dateButton}>
                <FontAwesomeIcon icon={faCalendar} size={20} color={Colors.red.main} />
                <Text style={styles.dateButtonText}>Choose birthday date</Text>
              </View>
            </TouchableOpacity>
            {touched.birthday && errors.birthday && <Text style={styles.error}>{errors.birthday}</Text>}
            <Button mode="contained" style={styles.confirmButton} onPress={() => handleSubmit()}>
              <Text style={styles.buttonText}>Confirm</Text>
            </Button>
          </View>
        )}
      </Formik>
      <ChoosePhotoModal isModalVisible={isModalVisible} setModalVisible={setModalVisible} />
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: Colors.white,
    alignItems: 'center',
    gap: 20
  },
  title: {
    fontSize: 24,
    fontWeight: 'bold',
  },
  photoContainer: {
    flex: 1,
    borderWidth: 2,
    borderColor: Colors.lightGray,
    borderRadius: 10,
    width: 150,
    alignItems: 'center',
    justifyContent: 'center',
    position: 'relative',
  },
  input: {
    height: 50,
    width: '100%',
    borderColor: Colors.lightGray,
    borderWidth: 1,
    borderRadius: 10,
  },
  dateButton: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: Colors.white,
    borderColor: Colors.red.main,
    borderWidth: 1,
    borderRadius: 10,
    paddingVertical: 10,
    paddingHorizontal: 10,
  },
  dateButtonText: {
    color: Colors.red.main,
    marginLeft: 10,
  },
  confirmButton: {
    backgroundColor: Colors.red.main,
    borderRadius: 10,
    paddingVertical: 10,
  },
  buttonText: {
    color: Colors.white,
    fontWeight: 'bold',
  },
  error: {
    color: 'red',
    fontSize: 12,
  },
});

ProfileDetail.displayName = "ProfileDetail";
export default ProfileDetail;
