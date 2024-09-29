import React, { useState } from 'react';
import {
  View,
  Text,
  StyleSheet,
  TouchableOpacity,
  ScrollView,
  Alert,
} from 'react-native';
import { useWizard } from 'react-use-wizard';
import { FontAwesomeIcon } from '@fortawesome/react-native-fontawesome';
import {
  faChevronLeft,
  faCamera,
  faShoppingBag,
  faMicrophone,
  faYinYang,
  faUtensils,
  faTableTennis,
  faRunning,
  faSwimmer,
  faPalette,
  faPlane,
  faMountain,
  faMusic,
  faGlassWhiskey,
  faGamepad,
} from '@fortawesome/free-solid-svg-icons';
import { Colors, Dimensions } from '@utils';
import { Button } from '@components';
import { BackButton, Title } from './components';

const interests = [
  { name: 'Photography', icon: faCamera },
  { name: 'Shopping', icon: faShoppingBag },
  { name: 'Karaoke', icon: faMicrophone },
  { name: 'Yoga', icon: faYinYang },
  { name: 'Cooking', icon: faUtensils },
  { name: 'Tennis', icon: faTableTennis },
  { name: 'Run', icon: faRunning },
  { name: 'Swimming', icon: faSwimmer },
  { name: 'Art', icon: faPalette },
  { name: 'Traveling', icon: faPlane },
  { name: 'Extreme', icon: faMountain },
  { name: 'Music', icon: faMusic },
  { name: 'Drink', icon: faGlassWhiskey },
  { name: 'Video games', icon: faGamepad },
];

const Interests = () => {
  const { previousStep, nextStep } = useWizard();
  const [selectedInterests, setSelectedInterests] = useState<string[]>([]);

  const toggleInterest = (interest: string) => {
    if (selectedInterests.includes(interest)) {
      setSelectedInterests(selectedInterests.filter(item => item !== interest));
    } else {
      setSelectedInterests([...selectedInterests, interest]);
    }
  };

  const handleContinue = () => {
    if (selectedInterests.length > 0) {
      nextStep();
    } else {
      Alert.alert('Error', 'Please select at least one interest');
    }
  };

  return (
    <View style={styles.container}>
      <BackButton onPress={previousStep} />
      <Title title='Your interests' subTitle="Select a few of your interests and let everyone know what you're passionate about." />
      <ScrollView contentContainerStyle={styles.interestsContainer}>
        {interests.map((interest, index) => (
          <TouchableOpacity
            key={interest.name}
            style={[
              styles.interestButton,
              selectedInterests.includes(interest.name) &&
              styles.selectedInterest,
              index % 2 === 0 ? { marginRight: '2%' } : { marginLeft: '2%' },
            ]}
            onPress={() => toggleInterest(interest.name)}>
            <FontAwesomeIcon
              icon={interest.icon}
              size={20}
              color={
                selectedInterests.includes(interest.name)
                  ? Colors.white
                  : Colors.red.main
              }
            />
            <Text
              style={[
                styles.interestText,
                selectedInterests.includes(interest.name) &&
                styles.selectedInterestText,
              ]}>
              {interest.name}
            </Text>
          </TouchableOpacity>
        ))}
      </ScrollView>
      <Button style={styles.continueButton} onPress={() => handleContinue()}>
        <Text style={styles.buttonText}>Continue</Text>
      </Button>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: Colors.white,
  },
  interestsContainer: {
    flexDirection: 'row',
    flexWrap: 'wrap',
    justifyContent: 'flex-start',
  },
  interestButton: {
    flexDirection: 'row',
    alignItems: 'center',
    borderWidth: 1,
    borderColor: Colors.lightGray,
    borderRadius: Dimensions.normal,
    padding: Dimensions.small,
    marginBottom: Dimensions.small,
    width: '48%',
  },
  selectedInterest: {
    backgroundColor: Colors.red.main,
    borderColor: Colors.red.main,
    shadowColor: Colors.red.main,
    shadowOffset: {
      width: 0,
      height: 3,
    },
    shadowOpacity: 0.27,
    shadowRadius: 4.65,
    elevation: 6,
  },
  interestText: {
    fontSize: Dimensions.normal,
    color: Colors.black,
    fontWeight: 'bold',
    marginLeft: Dimensions.xSmall,
  },
  selectedInterestText: {
    color: Colors.white,
    fontWeight: 'bold',
  },
  continueButton: {
    backgroundColor: Colors.red.main,
    borderRadius: Dimensions.xSmall,
    padding: Dimensions.normal,
    alignItems: 'center',
  },
  buttonText: {
    color: Colors.white,
    fontSize: Dimensions.medium,
    fontWeight: 'bold',
  },
});

Interests.displayName = 'Interests';
export default Interests;
