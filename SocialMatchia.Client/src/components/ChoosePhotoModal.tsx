import React from 'react'
import { View, Text, StyleSheet } from 'react-native'

import { launchCamera, launchImageLibrary } from 'react-native-image-picker';
import Modal from 'react-native-modal';
import { FontAwesomeIcon } from '@fortawesome/react-native-fontawesome';
import { faClose } from '@fortawesome/free-solid-svg-icons';

import { Button } from '@components';
import { Colors } from '@utils';

interface ChoosePhotoModalProps {
  isModalVisible: boolean;
  setModalVisible: (value: boolean) => void;
}

const ChoosePhotoModal = ({ isModalVisible, setModalVisible }: ChoosePhotoModalProps) => {
  const toggleModal = () => {
    setModalVisible(false);
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
        <View style={styles.cameraActions}>
          <Button style={styles.cameraActionItem} onPress={handleTakePhoto}><Text style={{ color: Colors.red.main, fontWeight: 'bold' }}>Take photo</Text></Button>
          <Button style={[styles.cameraActionItem, { borderStyle: 'dashed', borderWidth: 2, borderColor: Colors.red.main }]} onPress={handleChooseFromLibrary}><Text style={{ color: Colors.red.main, fontWeight: 'bold' }}>Choose from library</Text></Button>
        </View>
      </View>
    </Modal>
  )
}

const styles = StyleSheet.create({
  modal: {
    height: 320,
    backgroundColor: 'white',
    padding: 20,
    gap: 20,
    borderRadius: 10
  },
  subTitle: {
    fontSize: 20,
    fontWeight: 'bold',
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
});

ChoosePhotoModal.displayName = 'ChoosePhotoModal'
export default ChoosePhotoModal