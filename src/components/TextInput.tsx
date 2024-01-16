import { TextInput, TextInputProps } from 'react-native';
import { globalStyles } from '../styles';
import React from 'react';
import { colors } from '../utils';

type CustomTextInputProps = TextInputProps & {
  onChange?: (text: string) => void;
};

const CustomTextInput = (props: CustomTextInputProps) => {
  const [isFocused, setIsFocused] = React.useState(false);

  const handleFocus = () => {
    setIsFocused(true);
  };
  const handleBlur = () => {
    setIsFocused(false);
  };

  return (
    <TextInput
      {...props}
      onFocus={handleFocus}
      onBlur={handleBlur}
      placeholder={props.placeholder}
      placeholderTextColor={colors.frenchGray}
      style={[
        globalStyles.textInput,
        props.style,
        {
          borderColor: isFocused ? colors.royalBlue : colors.frenchGray,
          borderWidth: isFocused ? 1.8 : 1,
        },
      ]}
      value={props.value}
      onChangeText={(text) => props.onChange && props.onChange(text)}
    />
  );
};

export default CustomTextInput;
