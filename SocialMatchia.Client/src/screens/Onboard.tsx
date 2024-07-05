import React, { useState, useRef } from 'react';
import { View, Text, StyleSheet, TouchableOpacity, Image } from 'react-native';
import Swiper from 'react-native-swiper';
import { colors } from '../utils';

export enum OnboardSteps {
  Algorithm = 'ALGORITHM',
  Matches = 'MATCHES',
  Premium = 'PREMIUM'
}


const images = [
  require('../assets/images/onboard/girl1.png'),
  require('../assets/images/onboard/girl2.png'),
  require('../assets/images/onboard/girl3.png')
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


const Onboard = ({ navigation }: any) => {
  const [currentStep, setCurrentStep] = useState(0);
  const swiperRef = useRef<Swiper>(null);

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
            <Text style={styles.title}>{item.title}</Text>
            <Text style={styles.description}>{item.description}</Text>
            <TouchableOpacity style={styles.button} onPress={handleNext}>
              <Text style={styles.buttonText}>{data.length == i + 1 ? 'Create an account' : 'Next'}</Text>
            </TouchableOpacity>
          </View>
        ))}
      </Swiper>
      <TouchableOpacity style={styles.signIn} onPress={() => navigation.navigate('SignIn')}>
        <Text style={styles.signInText}>Already have an account? <Text style={styles.signInLink}>Sign In</Text></Text>
      </TouchableOpacity>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: colors.white
  },
  slide: {
    paddingTop: 50,
    flex: 1,
    justifyContent: 'flex-start',
    alignItems: 'center',
    padding: 20,
    gap: 10
  },
  imageContainer: {
    flexDirection: 'row',
    justifyContent: 'center',
    alignItems: 'center',
    width: '100%',
    gap: 15
  },
  image: {
    width: '70%',
    height: 400,
    resizeMode: 'cover',
    borderRadius: 10
  },
  passiveImage: {
    width: '20%',
    height: 300,
    resizeMode: 'cover',
    opacity: 0.5,
    borderRadius: 10
  },
  title: {
    fontSize: 24,
    fontWeight: 'bold',
    marginVertical: 10
  },
  description: {
    fontSize: 16,
    textAlign: 'center',
    marginBottom: 20
  },
  button: {
    backgroundColor: colors.red,
    paddingHorizontal: 20,
    paddingVertical: 10,
    borderRadius: 5
  },
  buttonText: {
    color: colors.white
  },
  signIn: {
    position: 'absolute',
    bottom: 20,
    left: 0,
    right: 0,
    alignItems: 'center'
  },
  signInText: {
    fontSize: 14
  },
  signInLink: {
    color: colors.red,
    fontWeight: 'bold'
  },
  pagination: {
    bottom: 60
  },
  activeDot: {
    backgroundColor: colors.red,
    width: 8,
    height: 8,
    borderRadius: 4,
    margin: 3
  },
  dot: {
    backgroundColor: colors.lightGray,
    width: 8,
    height: 8,
    borderRadius: 4,
    margin: 3
  }
});

export default Onboard;