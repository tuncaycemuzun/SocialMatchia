import React from 'react';
import { View, Text, StyleSheet } from 'react-native';

import { launchCamera, launchImageLibrary } from 'react-native-image-picker';
import Modal from 'react-native-modal';
import { FontAwesomeIcon } from '@fortawesome/react-native-fontawesome';
import { faClose } from '@fortawesome/free-solid-svg-icons';

import { Colors, Dimensions } from '@utils';
import { Button } from '@components';

interface ChoosePhotoModalProps {
  isModalVisible: boolean;
  setModalVisible: (value: boolean) => void;
  onPhotoSelect: (photos: any) => void;
  maxPhotos: number;
}

const ChoosePhotoModal = ({
  isModalVisible,
  setModalVisible,
  onPhotoSelect,
  maxPhotos,
}: ChoosePhotoModalProps) => {
  const toggleModal = () => {
    setModalVisible(false);
  };

  const handleTakePhoto = () => {
    launchCamera({
      mediaType: 'photo',
      saveToPhotos: true,
      cameraType: 'front',
      includeBase64: true,
    }, response => {
      if (response.assets) {
        console.log('Photo URI: ', response.assets[0].uri);
        onPhotoSelect(response.assets);
      }
    });
  };

  const handleChooseFromLibrary = () => {
    launchImageLibrary({
      mediaType: 'photo',
      selectionLimit: 6,
      includeBase64: true,
    }, response => {
      if (response.assets) {
        onPhotoSelect(response.assets)
      }
    });
  };

  return (
    <Modal
      animationIn={'slideInUp'}
      animationOut="slideOutDown"
      coverScreen={true}
      style={{ flex: 1 }}
      backdropOpacity={0.6}
      onSwipeComplete={() => setModalVisible(false)}
      swipeDirection="down"
      isVisible={isModalVisible}>
      <View style={styles.modal}>
        <View style={styles.modalHeader}>
          <View>
            <Text style={styles.subTitle}>Choose an option</Text>
            <Text style={styles.maxPhotosInfo}>
              Maksimum {maxPhotos} fotoğraf seçebilirsiniz
            </Text>
          </View>
          <Button style={styles.modalClose} onPress={() => toggleModal()}>
            <FontAwesomeIcon
              icon={faClose}
              size={20}
              color={Colors.lightGray}
            />
          </Button>
        </View>
        <View style={styles.cameraActions}>
          <Button
            style={styles.cameraActionItem}
            onPress={() => handleTakePhoto()}>
            <Text style={{ color: Colors.red.main, fontWeight: 'bold' }}>
              Take photo
            </Text>
          </Button>
          <Button
            style={[
              styles.cameraActionItem,
              {
                borderStyle: 'dashed',
                borderWidth: 2,
                borderColor: Colors.red.main,
              },
            ]}
            onPress={() => handleChooseFromLibrary()}>
            <Text style={{ color: Colors.red.main, fontWeight: 'bold' }}>
              Choose from library
            </Text>
          </Button>
        </View>
      </View>
    </Modal>
  );
};

const styles = StyleSheet.create({
  modal: {
    height: 'auto',
    backgroundColor: 'white',
    padding: Dimensions.large,
    gap: Dimensions.xLarge,
    borderRadius: Dimensions.small,
  },
  subTitle: {
    fontSize: Dimensions.large,
    fontWeight: 'bold',
  },
  maxPhotosInfo: {
    fontSize: Dimensions.medium,
    color: Colors.lightGray,
    marginTop: 5,
  },
  modalHeader: {
    width: '100%',
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
  },
  modalClose: {
    borderWidth: 1,
    borderColor: Colors.lightGray,
    backgroundColor: Colors.white,
    borderRadius: Dimensions.small,
    width: 30,
    height: 30,
    justifyContent: 'center',
    alignItems: 'center',
  },
  cameraActions: {
    flexDirection: 'column',
    justifyContent: 'center',
    alignItems: 'center',
    gap: Dimensions.large,
  },
  cameraActionItem: {
    justifyContent: 'center',
    backgroundColor: '#fdecee',
    padding: Dimensions.medium,
    borderRadius: Dimensions.small,
    height: 100,
    width: '100%',
    alignItems: 'center',
  },
});

ChoosePhotoModal.displayName = 'ChoosePhotoModal';
export default ChoosePhotoModal;
