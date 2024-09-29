import React, { useState } from 'react';
import { View, Text, StyleSheet, TouchableOpacity } from 'react-native';
import { useWizard } from 'react-use-wizard';
import { Colors, Dimensions } from '@utils';
import { Button } from '@components';
import { BackButton } from './components';

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
      <Text style={styles.title}>I'm a</Text>
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
            <Text style={[
              styles.optionText,
              selectedGender === option.value && styles.selectedOptionText,
            ]}>
              {option.label}
            </Text>
          </TouchableOpacity>
        ))}
      </View>
      <View style={styles.buttonContainer}>
        <Button
          onPress={handleContinue}
          disabled={!selectedGender}
          style={styles.continueButton}
        >
          <Text style={styles.buttonText}>Devam Et</Text>
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
  optionText: {
    fontSize: Dimensions.medium,
    color: Colors.black,
    fontWeight: 'bold',
  },
  selectedOptionText: {
    color: Colors.white,
    fontWeight: 'bold',
  },
  buttonContainer: {
    marginTop: 'auto',
    width: '100%',
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

GenderSelection.displayName = "GenderSelection";
export default GenderSelection;