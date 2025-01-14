import React, { useState, useRef } from 'react';
import { View, StyleSheet, Image } from 'react-native';

import Swiper from 'react-native-swiper';
import { Link, NavigationProp, ParamListBase, useNavigation } from '@react-navigation/native';

import { Colors, Dimensions } from '@utils';
import { Button, Text } from '@components';

enum OnboardSteps {
  Algorithm = 'ALGORITHM',
  Matches = 'MATCHES',
  Premium = 'PREMIUM'
}

const images = [
  require('@assets/images/onboard/girl1.png'),
  require('@assets/images/onboard/girl2.png'),
  require('@assets/images/onboard/girl3.png')
];

const data = [
  {
    type: OnboardSteps.Algorithm,
    primaryImage: images[0],
    firstImage: images[2],
    secondImage: images[1],
    title: 'Algorithm',
    description: 'Users going through a vetting process to ensure you never match with bots.'
  },
  {
    type: OnboardSteps.Matches,
    primaryImage: images[1],
    firstImage: images[0],
    secondImage: images[2],
    title: 'Matches',
    description: 'We match you with people that have a large array of similar interests.'
  },
  {
    type: OnboardSteps.Premium,
    primaryImage: images[2],
    firstImage: images[1],
    secondImage: images[0],
    title: 'Premium',
    description: 'Sign up today and enjoy the first month of premium benefits on us.'
  }
];

const Onboard = () => {
  const [currentStep, setCurrentStep] = useState(0);
  const swiperRef = useRef<Swiper>(null);
  const navigation = useNavigation<NavigationProp<ParamListBase>>()

  const onIndexChanged = (index: number) => {
    setCurrentStep(index);
  };

  const handleNext = () => {
    if (swiperRef.current && currentStep < data.length - 1) {
      swiperRef.current.scrollBy(1);
    } else if (currentStep === data.length - 1) {
      navigation.navigate('SignUp');
    }
  };

  return (
    <View style={styles.container}>
      <Swiper
        ref={swiperRef}
        loop={false}
        index={currentStep}
        onIndexChanged={onIndexChanged}
        showsPagination={true}
        paginationStyle={styles.pagination}
        activeDot={<View style={styles.activeDot} />}
        dot={<View style={styles.dot} />}
      >
        {data.map((item, i) => (
          <View key={i} style={styles.slide}>
            <View style={styles.imageContainer}>
              <Image source={item.firstImage} style={styles.passiveImage} />
              <Image source={item.primaryImage} style={styles.image} />
              <Image source={item.secondImage} style={styles.passiveImage} />
            </View>
            <Text fontSize={Dimensions.large} fontWeight='bold'>{item.title}</Text>
            <Text fontSize={Dimensions.medium} textAlign='center'>{item.description}</Text>
            <Button style={styles.button} onPress={handleNext}>
              <Text fontWeight='bold' color={Colors.white} padding={Dimensions.xSmall}>{data.length == i + 1 ? 'Create an account' : 'Next'}</Text>
            </Button>
          </View>
        ))}
      </Swiper>
      <Link to="/SignIn" style={styles.signIn}>
        <Text>Already have an account? <Text fontWeight='bold' color={Colors.red.main}>Sign In</Text></Text>
      </Link>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: Colors.white
  },
  slide: {
    paddingTop: 50,
    flex: 1,
    justifyContent: 'flex-start',
    alignItems: 'center',
    padding: Dimensions.xLarge,
    gap: Dimensions.xLarge
  },
  imageContainer: {
    flexDirection: 'row',
    justifyContent: 'center',
    alignItems: 'center',
    width: '100%',
    gap: Dimensions.large
  },
  image: {
    width: '70%',
    height: 400,
    resizeMode: 'cover',
    borderRadius: Dimensions.small
  },
  passiveImage: {
    width: '20%',
    height: 300,
    resizeMode: 'cover',
    borderRadius: Dimensions.small
  },
  button: {
    padding: Dimensions.xSmall,
    backgroundColor: Colors.red.main,
    borderRadius: Dimensions.small
  },
  signIn: {
    position: 'absolute',
    bottom: 20,
    left: 0,
    right: 0,
    alignItems: 'center',
    justifyContent: 'center',
    textAlign: 'center'
  },
  pagination: {
    bottom: 60
  },
  activeDot: {
    backgroundColor: Colors.red.main,
    width: 8,
    height: 8,
    borderRadius: 4,
    margin: 3
  },
  dot: {
    backgroundColor: Colors.lightGray,
    width: 8,
    height: 8,
    borderRadius: 4,
    margin: 3
  }
});

Onboard.displayName = 'Onboard';
export default Onboard;
