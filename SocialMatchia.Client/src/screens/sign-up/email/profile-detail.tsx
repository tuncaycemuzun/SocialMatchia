import React from 'react';
import {
  View,
  Text,
  StyleSheet,
  TouchableOpacity,
  Modal,
  ScrollView,
  Image,
} from 'react-native';
import {useWizard} from 'react-use-wizard';
import {FontAwesomeIcon} from '@fortawesome/react-native-fontawesome';
import {faChevronLeft, faPlus, faTimes} from '@fortawesome/free-solid-svg-icons';
import {Formik} from 'formik';
import * as Yup from 'yup';
import DateTimePicker from '@react-native-community/datetimepicker';

import {ChoosePhotoModal, TextInput, Button} from '@components';
import {Colors, Fonts} from '@utils';

const ProfileDetail = () => {
  const {nextStep, previousStep} = useWizard();
  const [isPhotoModalVisible, setPhotoModalVisible] = React.useState(false);
  const [selectedPhotos, setSelectedPhotos] = React.useState<any[]>([]);
  const [isDatePickerVisible, setDatePickerVisible] = React.useState(false);

  const toggleModal = () => {
    setPhotoModalVisible(prevState => !prevState);
  };

  const toggleDatePicker = () => {
    setDatePickerVisible(prevState => !prevState);
  };

  const validationSchema = Yup.object().shape({
    firstName: Yup.string().required('Ad gereklidir'),
    lastName: Yup.string().required('Soyad gereklidir'),
    birthday: Yup.date().required('Doğum tarihi gereklidir'),
  });

  const removePhoto = (index:number) => {
    setSelectedPhotos(prevPhotos => prevPhotos.filter((_, i) => i !== index));
  };

  return (
    <View style={styles.container}>
      <TouchableOpacity onPress={previousStep} style={styles.backButton}>
        <FontAwesomeIcon icon={faChevronLeft} size={20} color={Colors.black} />
      </TouchableOpacity>
      <ScrollView contentContainerStyle={styles.scrollContent}>
        <Text style={styles.title}>Profil detayları</Text>
        <View style={styles.photosContainer}>
          {selectedPhotos.map((photo, index) => (
            <View key={index} style={styles.photoWrapper}>
              <Image source={{uri: photo.uri}} style={styles.photo} />
              <TouchableOpacity
                style={styles.removePhotoButton}
                onPress={() => removePhoto(index)}>
                <FontAwesomeIcon icon={faTimes} size={15} color={Colors.white} />
              </TouchableOpacity>
            </View>
          ))}
          {selectedPhotos.length < 6 && (
            <TouchableOpacity 
              style={[
                styles.addPhotoButton,
                selectedPhotos.length === 0 && styles.photoContainerError
              ]} 
              onPress={toggleModal}
            >
              <FontAwesomeIcon icon={faPlus} size={30} color={Colors.lightGray} />
            </TouchableOpacity>
          )}
        </View>
        <Formik
          initialValues={{firstName: '', lastName: '', birthday: ''}}
          validationSchema={validationSchema}
          onSubmit={(values, {setSubmitting}) => {
            if (selectedPhotos.length === 0) {
              setSubmitting(false);
              return;
            }
            console.log(values, selectedPhotos);
            nextStep();
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
                  label="Ad"
                  onChangeText={handleChange('firstName')}
                  onBlur={handleBlur('firstName')}
                  value={values.firstName}
                  error={touched.firstName && errors.firstName}
                />
                <TextInput
                  label="Soyad"
                  onChangeText={handleChange('lastName')}
                  onBlur={handleBlur('lastName')}
                  value={values.lastName}
                  error={touched.lastName && errors.lastName}
                />
                <TouchableOpacity onPress={toggleDatePicker}>
                  <TextInput
                    label="Doğum Tarihi"
                    value={values.birthday}
                    editable={false}
                  />
                </TouchableOpacity>
                {isDatePickerVisible && (
                  <DateTimePicker
                    value={new Date(values.birthday)}
                    mode="date"
                    display="default"

                    onChange={(event, selectedDate) => {
                      setDatePickerVisible(false);
                      console.log(selectedDate)
                      if (selectedDate) {
                        setFieldValue('birthday', selectedDate);
                      }
                    }}
                  />
                )}
              </View>
              <View style={styles.buttonContainer}>
                <Button
                  onPress={() => handleSubmit()}
                  style={styles.confirmButton}>
                  <Text style={styles.buttonText}>Onayla</Text>
                </Button>
              </View>
            </View>
          )}
        </Formik>
      </ScrollView>

      <ChoosePhotoModal
        isModalVisible={isPhotoModalVisible}
        setModalVisible={setPhotoModalVisible}
        onPhotoSelect={(photos) => {
          setSelectedPhotos((prevPhotos : any) => {
            const newPhotos = [...prevPhotos, ...photos];
            return newPhotos.slice(0, 6);
          });
        }}
        maxPhotos={6 - selectedPhotos.length}
      />
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
    alignItems: 'center',
    padding: 20,
  },
  backButton: {
    position: 'absolute',
  },
  title: {
    fontSize: 24,
    fontWeight: 'bold',
    marginBottom: 20,
    fontFamily: Fonts.bold,
  },
  photosContainer: {
    flexDirection: 'row',
    flexWrap: 'wrap',
    justifyContent: 'center',
    marginBottom: 20,
    gap:10
  },
  photoWrapper: {
    gap:5,
  },
  photo: {
    width: 100,
    height: 100,
    borderRadius: 10,
  },
  removePhotoButton: {
    position: 'absolute',
    top: -5,
    right: -5,
    backgroundColor: Colors.red.main,
    borderRadius: 15,
    width: 25,
    height: 25,
    justifyContent: 'center',
    alignItems: 'center',
  },
  addPhotoButton: {
    width: 100,
    height: 100,
    borderWidth: 2,
    borderColor: Colors.lightGray,
    borderRadius: 10,
    alignItems: 'center',
    justifyContent: 'center',
    margin: 5,
  },
  formContainer: {
    width: '100%',
    flex:1,
    justifyContent: 'space-between',
  },
  inputContainer: {
    width: '100%',
    gap: 10,
  },
  errorText: {
    color: Colors.red.main,
    fontSize: 12,
    marginTop: 5,
    fontFamily: Fonts.regular,
  },
  buttonContainer: {
    backgroundColor: Colors.white,
  },
  confirmButton: {
    backgroundColor: Colors.red.main,
    borderRadius: 8,
    padding: 15,
    alignItems: 'center',
  },
  buttonText: {
    color: Colors.white,
    fontSize: 16,
    fontWeight: 'bold',
    fontFamily: Fonts.bold,
  },
  photoContainerError: {
    borderColor: Colors.red.main,
  }
});

ProfileDetail.displayName = 'ProfileDetail';
export default ProfileDetail;
