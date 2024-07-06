import React from 'react';
import { StyleSheet, Text, View } from 'react-native';

import { faCamera, faPlus, faClose } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-native-fontawesome';
import { launchCamera, launchImageLibrary } from 'react-native-image-picker';
import Modal from "react-native-modal";
import { Wizard, useWizard } from 'react-use-wizard';

import { Button } from '@components';
import { Colors } from '@utils';

const EmailSignUp = () => {
  return (
    <View style={styles.container}>
      <Wizard>
        <ProfileDetail />
        <Gender />
        <EmailAndPassword />
      </Wizard>
    </View>
  )
}

const ProfileDetail = () => {
  const { nextStep } = useWizard();
  const [isModalVisible, setModalVisible] = React.useState(false);

  const toggleModal = () => {
    setModalVisible(prevState => !prevState);
  };

  const handleTakePhoto = () => {
    launchCamera({ mediaType: 'photo', saveToPhotos: true, }, response => {
      if (response.assets) {
        console.log('Photo URI: ', response.assets[0].uri);
      }
    });
  };

  const handleChooseFromLibrary = () => {
    launchImageLibrary({ mediaType: 'photo' }, response => {
      if (response.assets) {
        console.log('Photo URI: ', response.assets[0].uri);
      }
    });
  };

  return (
    <View style={styles.itemContainer}>
      <Text style={styles.title}>Profile details</Text>
      <View style={styles.photoContainer}>
        <Button style={styles.photo} onPress={toggleModal}>
          <FontAwesomeIcon icon={faPlus} size={30} color={Colors.lightGray} />
          <View style={styles.camera}>
            <FontAwesomeIcon icon={faCamera} size={15} color={Colors.white} />
          </View>
        </Button>
      </View>
      <Button onPress={nextStep}><Text>Next</Text></Button>
      <Modal
        animationIn={"slideInUp"}
        animationOut="slideOutDown"
        coverScreen={true}
        style={{ flex: 1 }}
        backdropOpacity={0.6}
        onSwipeComplete={() => setModalVisible(false)}
        swipeDirection="down"
        isVisible={isModalVisible}
      >
        <View style={styles.modal}>
          <View style={styles.modalHeader}>
            <Text style={styles.subTitle}>Choose an option</Text>
            <Button style={styles.modalClose} onPress={toggleModal}>
              <FontAwesomeIcon icon={faClose}></FontAwesomeIcon>
            </Button>
          </View>
          {/* <View style={styles.cameraActions}>
            <Button style={styles.cameraActionItem} onPress={handleTakePhoto}><Text style={{ color: Colors.red.main, fontWeight: 'bold' }}>Take photo</Text></Button>
            <Button style={[styles.cameraActionItem, { borderStyle: 'dashed', borderWidth: 2, borderColor: Colors.red.main }]} onPress={handleChooseFromLibrary}><Text style={{ color: Colors.red.main, fontWeight: 'bold' }}>Choose from library</Text></Button>
          </View> */}


          <View style={styles.cameraActions}>
            <Button style={styles.cameraActionItem} onPress={handleTakePhoto}><Text style={{ color: Colors.red.main, fontWeight: 'bold' }}>Take photo</Text></Button>
            <Button style={[styles.cameraActionItem, { borderStyle: 'dashed', borderWidth: 2, borderColor: Colors.red.main }]} onPress={handleChooseFromLibrary}><Text style={{ color: Colors.red.main, fontWeight: 'bold' }}>Choose from library</Text></Button>
          </View>
        </View>
      </Modal>
    </View>
  )
}

const Gender = () => {
  const { nextStep } = useWizard();
  return (
    <View>
      <Text>Gender</Text>
      <Button onPress={nextStep}><Text>Next</Text></Button>
    </View>
  )
}

const EmailAndPassword = () => {
  return (
    <View>
      <Text>EmailAndPassword</Text>
    </View>
  )
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    padding: 40,
    backgroundColor: Colors.white
  },
  itemContainer: {
    flex: 1
  },
  subTitle: {
    fontSize: 20,
    fontWeight: 'bold',
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
  modal: {
    height: 320,
    backgroundColor: 'white',
    padding: 20,
    gap: 20,
    borderRadius: 10
  },
  cameraActions: {
    flexDirection: 'column',
    justifyContent: 'center',
    alignItems: 'center',
    gap: 20
  },
  cameraActionItem: {
    justifyContent: 'center',
    backgroundColor: '#fdecee',
    padding: 10,
    borderRadius: 5,
    height: 100,
    width: '100%',
    alignItems: 'center'
  },
  modalHeader: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between'
  },
  modalClose: {
    borderWidth: 1,
    borderColor: Colors.lightGray,
    padding: 5,
    borderRadius: 10
  }
})

EmailSignUp.displayName = 'EmailSignUp';
export default EmailSignUp;
