import React, { useState } from 'react';
import { View, StyleSheet, TouchableOpacity } from 'react-native';
import { useWizard } from 'react-use-wizard';
import { Colors, Dimensions } from '@utils';
import { Button, BackButton, Text } from '@components';
import { Title } from './components';

const GenderSelection = () => {
  const { previousStep, nextStep } = useWizard();
  const [selectedGender, setSelectedGender] = useState<string | null>(null);

  const genderOptions = [
    { label: 'Man', value: 'woman' },
    { label: 'Woman', value: 'man' },
    { label: 'Other', value: 'other' },
  ];

  const handleGenderSelect = (value: string) => {
    setSelectedGender(value);
  };

  const handleContinue = () => {
    if (selectedGender) {
      nextStep();
    }
  };

  return (
    <View style={styles.container}>
      <BackButton onPress={previousStep} />
      <Title title="I'm a" />
      <View>
        {genderOptions.map((option) => (
          <TouchableOpacity
            key={option.value}
            style={[
              styles.optionButton,
              selectedGender === option.value && styles.selectedOption,
            ]}
            onPress={() => handleGenderSelect(option.value)}
          >
            <Text fontWeight='bold' style={[selectedGender === option.value && styles.selectedOptionText]}>
              {option.label}
            </Text>
          </TouchableOpacity>
        ))}
      </View>
      <View style={styles.buttonContainer}>
        <Button
          onPress={handleContinue}
          disabled={!selectedGender}>
          <Text fontSize={Dimensions.medium} color={Colors.white} fontWeight='bold'>Continue</Text>
        </Button>
      </View>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: Colors.white,
  },
  title: {
    marginTop: 70,
    fontSize: Dimensions.large,
    fontWeight: 'bold',
    marginBottom: Dimensions.medium,
    fontFamily: 'bold',
    color: Colors.black,
  },
  optionButton: {
    borderWidth: 1,
    borderColor: Colors.lightGray,
    borderRadius: Dimensions.xSmall,
    padding: Dimensions.normal,
    marginBottom: Dimensions.medium,
  },
  selectedOption: {
    backgroundColor: Colors.red.main,
    borderColor: Colors.red.main,
  },
  selectedOptionText: {
    color: Colors.white,
    fontWeight: 'bold',
  },
  buttonContainer: {
    marginTop: 'auto',
    width: '100%',
  },
});

GenderSelection.displayName = "GenderSelection";
export default GenderSelection;