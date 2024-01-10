import { StyleProp, StyleSheet, TextInput, ViewStyle } from 'react-native';
import { globalStyles } from '../styles';
import React from 'react';
import { colors } from '../utils';

interface TextInputProps {
  value?: string;
  onChange?: (text: string) => void;
  style?: StyleProp<ViewStyle>;
  placeholder?: string;
}

const CustomTextInput = ({ value, onChange, style, placeholder }: TextInputProps) => {
  const [isFocused, setIsFocused] = React.useState(false);

  const handleFocus = () => {
    setIsFocused(true)
  }
  const handleBlur = () => {
    setIsFocused(false)
  }

  return (
    <TextInput
      onFocus={handleFocus}
      onBlur={handleBlur}
      placeholder={placeholder}
      style={[globalStyles.textInput, style, {
        borderColor: isFocused ? colors.royalBlue : colors.frenchGray,
        borderWidth: isFocused ? 2 : 1,
      }]}
      value={value}
      onChangeText={(text) => onChange && onChange(text)}
    />
  );
};

export default CustomTextInput;