import React from 'react';
import { Text, TouchableOpacity, TouchableOpacityProps, ViewStyle } from 'react-native';
import { colors } from '../utils';

type ButtonProps = TouchableOpacityProps & {
  text: string;
  size: 'small' | 'medium' | 'large';
  icon?: React.ReactNode;
  wFull?: boolean;
  textColor?: string;
};



const Button = (props: ButtonProps) => {

  const buttonStyle: ViewStyle = {
    backgroundColor: colors.royalBlue,
    padding: 10,
    borderRadius: 8,
    marginTop: 10,
    width: props.wFull == true ? '100%' : props.size === 'small' ? 100 : props.size === 'medium' ? 150 : 200,
    alignItems: 'center',
    height: props.size === 'small' ? 40 : props.size === 'medium' ? 50 : 60,
    justifyContent: 'center',
  };

  const mergedStyle: ViewStyle = props.style ? { ...buttonStyle, ...(props.style as ViewStyle) } : buttonStyle;

  return (
    <TouchableOpacity
      activeOpacity={0.8}
      {...props}
      style={mergedStyle}>
      <Text style={{
        color: props.textColor ? props.textColor : colors.ghostWhite,
        fontSize: props.size === 'small' ? 12 : props.size === 'medium' ? 16 : 20,
      }}>{props.text}</Text>

    </TouchableOpacity>
  );
};

export default Button;
