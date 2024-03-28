import { StyleSheet, Text, TextInput, TextInputProps, View } from 'react-native';
import { globalStyles } from '../styles';
import React from 'react';
import { colors } from '../utils';
import { FontAwesomeIcon } from '@fortawesome/react-native-fontawesome';
import { IconProp } from '@fortawesome/fontawesome-svg-core';

type CustomTextInputProps = TextInputProps & {
  onChange?: (text: string) => void;
  icon?: IconProp;
  iconPosition?: 'left' | 'right';
  iconSize?: number;
  iconColor?: string;
  iconPress?: () => void;
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
    <View style={{
      position: 'relative',
      width: '100%',
    }}>
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
      {
        props.icon && 
        <Text onPress={props.iconPress} style={props.iconPosition === 'left' ? styles.leftIcon : styles.rightIcon}>
          <FontAwesomeIcon size={props.iconSize || 18} color={props.iconColor} icon={props.icon} />
        </Text>
      }

    </View>

  );
};

const styles = StyleSheet.create({
  leftIcon: {
    left: 0,
    top: 10,
    padding: 10,
    position: 'absolute',
    zIndex: 1,
  },
  rightIcon: {
    right: 10,
    top: 10,
    padding: 10,
    position: 'absolute',
    zIndex: 1,
  }
});

export default CustomTextInput;
