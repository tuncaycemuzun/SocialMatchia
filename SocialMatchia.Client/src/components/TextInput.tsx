import React from 'react';
import { TextInput as PaperTextInput, TextInputProps as PaperTextInputProps } from 'react-native-paper';
import { StyleSheet } from 'react-native';
import { Colors } from '@utils';

interface CustomTextInputProps extends PaperTextInputProps {
  backgroundColor?: string;
  secureTextEntry?: boolean;
}

const CustomTextInput: React.FC<CustomTextInputProps> = ({ backgroundColor, mode = 'outlined', style, ...props }) => {
  return (
    <PaperTextInput
      mode={mode}
      secureTextEntry={props.secureTextEntry ?? false}
      style={[styles.input, { backgroundColor: backgroundColor || Colors.white }, style]}
      {...props}
    />
  );
};

const styles = StyleSheet.create({
  input: {
    backgroundColor: 'white',
  },
});

export default CustomTextInput;
