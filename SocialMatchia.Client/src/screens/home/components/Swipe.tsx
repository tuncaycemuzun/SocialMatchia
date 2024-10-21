import React, { useState, useEffect } from 'react';
import { View, StyleSheet, Image } from 'react-native';

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
    locationInfo: '1 km',
    job: 'Professional Model',
  },
  {
    id: 2,
    name: 'Bred Jackson',
    age: 25,
    images: [require('@assets/images/users/man.png')],
    job: 'Photographer',
  },
];

const Swipe = () => {
  const [currentUserIndex, setCurrentUserIndex] = useState(0);
  const [currentPhotoStep, setCurrentPhotoStep] = useState(0);
  const [user, setUser] = useState(users[currentUserIndex]);

  useEffect(() => {
    setUser(users[currentUserIndex]);
    setCurrentPhotoStep(0);
  }, [currentUserIndex]);

  const changePhotoIndex = (decrement: boolean) => {
    if (decrement && currentPhotoStep > 0) {
      setCurrentPhotoStep(currentPhotoStep - 1);
    } else if (!decrement && currentPhotoStep < user.images.length - 1) {
      setCurrentPhotoStep(currentPhotoStep + 1);
    }
  };

  const like = () => {
    const userId = user.id;
    console.log(`Liked user with ID: ${userId}`);
    setCurrentUserIndex((prevIndex) => (prevIndex + 1) % users.length);
  };

  const superlike = () => {
    const userId = user.id;
    console.log(`Superliked user with ID: ${userId}`);
    setCurrentUserIndex((prevIndex) => (prevIndex + 1) % users.length);
  };

  const dislike = () => {
    const userId = user.id;
    console.log(`Disliked user with ID: ${userId}`);
    setCurrentUserIndex((prevIndex) => (prevIndex + 1) % users.length);
  };

  return (
    <View style={styles.container}>
      <View style={styles.imageContainer}>
        <View style={{ flex: 1, position: 'relative' }}>
          <View style={styles.slide}>
            <Image source={user.images[currentPhotoStep]} style={styles.image} />
          </View>
          <Button
            onPress={() => changePhotoIndex(true)}
            style={[styles.swipeButton, { left: 0 }]} />
          <Button
            onPress={() => changePhotoIndex(false)}
            style={[styles.swipeButton, { right: 0 }]} />
        </View>

        {
        
            <View>
              <View style={{
                position: 'absolute',
                bottom: 20,
                left: 0,
                backgroundColor: 'rgba(0,0,0,0.2)',
                width: '100%',
                padding: Dimensions.large,
                borderBottomRightRadius: Dimensions.normal,
                borderBottomLeftRadius: Dimensions.normal,
              }}>
                <Text color={Colors.white} fontWeight='bold' fontSize={Dimensions.large}>
                  {user.name} - {user.age}
                </Text>
                <Text color={Colors.white} fontWeight='normal' fontSize={Dimensions.medium}>{user.job}</Text>
              </View>
            </View>
        }
      </View>
      <View style={{ justifyContent: 'center', alignItems: 'center' }}>
        <View style={{ flexDirection: 'row' }}>
          {users[currentUserIndex].images.map((_: any, index: number) => (
            <View key={index} style={index === currentPhotoStep ? styles.activeDot : styles.dot} />
          ))}
        </View>
      </View>
      <View style={styles.actionContainer}>
        <Button onPress={dislike} style={styles.actionButton}>
          <FontAwesomeIcon icon={faTimes} size={Dimensions.xLarge} color={Colors.orange.main} />
        </Button>

        <Button onPress={superlike} style={[styles.actionButton, { backgroundColor: Colors.red.main }]}>
          <FontAwesomeIcon icon={faStar} size={Dimensions.xxxLarge} color={Colors.white} />
        </Button>

        <Button onPress={like} style={styles.actionButton}>
          <FontAwesomeIcon icon={faHeart} size={Dimensions.xLarge} color={Colors.purple.main} />
        </Button>
      </View>
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    gap: 5
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
    flex: 1,
    height: '100%',
  },
  actionButton: {
    backgroundColor: Colors.white,
    padding: Dimensions.medium,
    borderRadius: Dimensions.xxLarge,
    elevation: 2
  },
  swipeButton: {
    position: 'absolute',
    height: '100%',
    width: '50%'
  }
});

Swipe.displayName = 'Swipe';
export default Swipe;
