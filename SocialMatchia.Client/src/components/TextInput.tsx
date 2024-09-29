import React from 'react';
import { TextInput as PaperTextInput, TextInputProps as PaperTextInputProps } from 'react-native-paper';
import { StyleSheet } from 'react-native';
import { Colors } from '@utils';

interface CustomTextInputProps extends PaperTextInputProps {
  backgroundColor?: string;
  secureTextEntry?: boolean;
  errorMessage?: string;
  touched?: boolean;
}

const CustomTextInput: React.FC<CustomTextInputProps> = ({ backgroundColor, mode = 'outlined', style, errorMessage, touched, ...props }) => {
  return (
    <PaperTextInput
      mode={mode}
      secureTextEntry={props.secureTextEntry ?? false}
      style={[styles.input, { backgroundColor: backgroundColor || Colors.white }, style]}
      theme={{
        colors: {
          primary: Colors.lightBlue,
          error: Colors.red.main,
        },
      }}
      error={!!errorMessage && touched}
      {...props}
    />
  );
};

const styles = StyleSheet.create({
  input: {
    backgroundColor: Colors.white,
  },
});

CustomTextInput.displayName = 'CustomTextInput';
export default CustomTextInput;
