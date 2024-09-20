import React, { useState } from 'react';
import { View, Text, StyleSheet, TouchableOpacity } from 'react-native';
import { useWizard } from 'react-use-wizard';
import { FontAwesomeIcon } from '@fortawesome/react-native-fontawesome';
import { faChevronLeft } from '@fortawesome/free-solid-svg-icons';
import { Colors, Fonts } from '@utils';
import { Button } from '@components';

const GenderSelection = () => {
  const { previousStep, nextStep } = useWizard();
  const [selectedGender, setSelectedGender] = useState<string | null>(null);

  const genderOptions = [
    { label: 'Kadın', value: 'woman' },
    { label: 'Erkek', value: 'man' },
    { label: 'Başka bir seçenek', value: 'other' },
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
      <TouchableOpacity onPress={previousStep} style={styles.backButton}>
        <FontAwesomeIcon icon={faChevronLeft} size={20} color={Colors.black} />
      </TouchableOpacity>
      <Text style={styles.title}>Ben bir</Text>
      <View style={styles.optionsContainer}>
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
    padding: 20,
  },
  backButton: {
    marginBottom: 20,
  },
  title: {
    fontSize: 24,
    fontWeight: 'bold',
    marginBottom: 30,
    fontFamily: Fonts.bold,
  },
  optionsContainer: {
    marginBottom: 30,
  },
  optionButton: {
    borderWidth: 1,
    borderColor: Colors.lightGray,
    borderRadius: 8,
    padding: 15,
    marginBottom: 10,
  },
  selectedOption: {
    backgroundColor: Colors.red.main,
    borderColor: Colors.red.main,
  },
  optionText: {
    fontSize: 16,
    color: Colors.black,
    fontFamily: Fonts.regular,
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
    borderRadius: 8,
    padding: 15,
    alignItems: 'center',
  },
  buttonText: {
    color: Colors.white,
    fontSize: 16,
    fontWeight: 'bold',
    fontFamily: Fonts.bold,
  },
});

GenderSelection.displayName = "GenderSelection";
export default GenderSelection;