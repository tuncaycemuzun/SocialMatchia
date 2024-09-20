import React from 'react';
import { TextInput as PaperTextInput, TextInputProps as PaperTextInputProps } from 'react-native-paper';
import { StyleSheet } from 'react-native';
import { Colors } from '@utils';

interface CustomTextInputProps extends PaperTextInputProps {
  backgroundColor?: string;
  secureTextEntry?: boolean;
  error?: boolean | string;
}

const CustomTextInput: React.FC<CustomTextInputProps> = ({ backgroundColor, mode = 'outlined', style, error, ...props }) => {
  return (
    <PaperTextInput
      mode={mode}
      secureTextEntry={props.secureTextEntry ?? false}
      style={[styles.input, { backgroundColor: backgroundColor || Colors.white }, style]}
      theme={{
        colors: {
          primary: Colors.lightBlue,
          error: Colors.red.main,
          underlineColor: 'transparent',
        },
      }}
      error={!!error}
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
