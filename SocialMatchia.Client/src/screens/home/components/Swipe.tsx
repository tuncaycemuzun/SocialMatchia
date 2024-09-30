import React, { useRef, useState, useEffect } from 'react';
import { View, StyleSheet, Image } from 'react-native';

import Swiper from 'react-native-swiper';
import { FontAwesomeIcon } from '@fortawesome/react-native-fontawesome';
import { faHeart, faTimes, faStar } from '@fortawesome/free-solid-svg-icons';

import { Button, Text } from '@components';
import { Colors, Dimensions } from '@utils';


const users: any = [
  {
    id: 1,
    name: 'Jessica Parker',
    age: 23,
    images: [
      require('@assets/images/users/woman.png'),
      require('@assets/images/users/woman2.png'),
      require('@assets/images/users/man.png'),
    ],
    locationInfo: "1 km",
    job: "Professional Model"
  },
  {
    id: 2,
    name: 'Bred Jackson',
    age: 25,
    images: [
      require('@assets/images/users/man.png')
    ],
    job: 'Photographer',
  },
];

const Swipe = () => {
  const [currentUser, setCurrentUser] = useState(0);
  const [currentPhotoStep, setCurrentPhotoStep] = useState(0);
  const swiperRef = useRef<Swiper>(null);
  const [user, setUser] = useState(users[currentUser]);
  const [userImages, setUserImages] = useState(user.images);
  const [showUserInfo, setShowUserInfo] = useState(user ? true : false);


  useEffect(() => {
    setUser(users[currentUser]);
    setUserImages(users[currentUser].images);
    setShowUserInfo(true);
  }, [currentUser]);

  const onPhotoIndexChanged = (index: number) => {
    setCurrentPhotoStep(index);
    console.log('index', index);
  };

  return (
    <View style={styles.container}>
      <View style={styles.imageContainer}>
        <Swiper
          horizontal={false}
          ref={swiperRef}
          loop={false}
          index={currentPhotoStep}
          onIndexChanged={onPhotoIndexChanged}
          onTouchStart={() => setShowUserInfo(false)}
          onMomentumScrollEnd={() => setShowUserInfo(true)}
          showsPagination={true}
          activeDot={<View style={styles.activeDot} />}
          dot={<View style={styles.dot} />}
        >
          {userImages.map((image: any, index: number) => (
            <View key={`${user.id}-image-${index}`} style={styles.slide}>
              <Image source={image} style={styles.image} />
            </View>
          ))}
        </Swiper>
        {
          showUserInfo && (
            <View>
              <View style={{
                position: 'absolute',
                bottom: 20,
                left: 0,
                backgroundColor: 'rgba(0,0,0,0.5)',
                width: '100%',
                padding: Dimensions.large,
                borderWidth: 1,
                borderColor: Colors.lightGray,
                borderBottomRightRadius: Dimensions.normal,
                borderBottomLeftRadius: Dimensions.normal,
              }}>
                <Text color={Colors.white} fontWeight='bold' fontSize={Dimensions.large}>
                  {user.name} - {user.age}
                </Text>
                <Text color={Colors.white} fontWeight='normal' fontSize={Dimensions.medium}>{user.job}</Text>
              </View>
            </View>
          )
        }
      </View>
      <View style={styles.actionContainer}>
        <Button style={styles.actionButton}>
          <FontAwesomeIcon icon={faTimes} size={Dimensions.xLarge} color={Colors.orange.main} />
        </Button>

        <Button style={[styles.actionButton, { backgroundColor: Colors.red.main }]}>
          <FontAwesomeIcon icon={faStar} size={Dimensions.xxxLarge} color={Colors.white} />
        </Button>

        <Button style={styles.actionButton}>
          <FontAwesomeIcon icon={faHeart} size={Dimensions.xLarge} color={Colors.purple.main} />
        </Button>
      </View>
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
  },
  imageContainer: {
    flex: 6,
    borderRadius: Dimensions.large,
  },
  actionContainer: {
    flex: 1,
    flexDirection: 'row',
    justifyContent: 'space-around',
    alignItems: 'center',
    borderRadius: Dimensions.large,
  },
  activeDot: {
    backgroundColor: Colors.red.main,
    width: 8,
    height: 8,
    borderRadius: 4,
    margin: 3,
  },
  dot: {
    backgroundColor: Colors.lightGray,
    width: 8,
    height: 8,
    borderRadius: 4,
    margin: 3,
  },
  image: {
    flex: 1,
    width: '100%',
    height: '100%',
    resizeMode: 'cover',
    borderRadius: Dimensions.normal,
    marginBottom: 20
  },
  slide: {
    flex: 1
  },
  actionButton: {
    backgroundColor: Colors.white,
    padding: Dimensions.medium,
    borderRadius: Dimensions.xxLarge,
    elevation: 2
  }
});

Swipe.displayName = 'Swipe';
export default Swipe;
