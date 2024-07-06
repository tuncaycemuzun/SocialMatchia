import React from 'react'
import { View, Text, StyleSheet } from 'react-native'
import { useWizard } from 'react-use-wizard';
import { FontAwesomeIcon } from '@fortawesome/react-native-fontawesome';
import { faCamera, faPlus } from '@fortawesome/free-solid-svg-icons';

import { Button, ChoosePhotoModal } from '@components';
import { Colors } from '@utils';


const ProfileDetail = () => {
  const { nextStep } = useWizard();
  const [isModalVisible, setModalVisible] = React.useState(false);

  const toggleModal = () => {
    setModalVisible(prevState => !prevState);
  };
  return (
    <View style={styles.container}>
      <Text style={styles.title}>Profile details</Text>
      <View style={styles.photoContainer}>
        <Button style={styles.photo} onPress={toggleModal}>
          <FontAwesomeIcon icon={faPlus} size={30} color={Colors.lightGray} />
          <View style={styles.camera}>
            <FontAwesomeIcon icon={faCamera} size={15} color={Colors.white} />
          </View>
        </Button>
      </View>
      <View style={styles.personnelInfo}>
        
      </View>
      <Button onPress={nextStep}><Text>Next</Text></Button>
      <ChoosePhotoModal isModalVisible={isModalVisible} setModalVisible={setModalVisible} />
    </View>
  )
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    gap:40,
  },
  title: {
    fontSize: 24,
    fontWeight: 'bold',
  },
  photoContainer: {
    width: '100%',
    height: 100,
    justifyContent: 'center',
    alignItems: 'center',
    borderRadius: 10,
    position: 'relative'
  },
  photo: {
    width: 100,
    height: 100,
    borderRadius: 30,
    backgroundColor: Colors.white,
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
    borderWidth: 2,
    borderColor: Colors.lightGray,
  },
  camera: {
    position: 'absolute',
    bottom: -5,
    right: -5,
    backgroundColor: Colors.red.main,
    padding: 8,
    borderRadius: 20,
    borderWidth: 2,
    borderColor: Colors.white
  },
  personnelInfo:{
    flex:1,
    backgroundColor:'red'
  }
})

ProfileDetail.displayName = "ProfileDetail"
export default ProfileDetail