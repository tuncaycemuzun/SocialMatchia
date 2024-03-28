import React from 'react';
import { Text, TextProps } from 'react-native';
import { globalStyles } from '../styles';

interface CustomTextProps extends TextProps {
  children: React.ReactNode;
  size: 'small' | 'medium' | 'large';
}

const CustomText = (props: CustomTextProps) => {
  return (
    <Text {...props} style={[globalStyles.textColor, props.style, {
      fontSize: props.size === 'small' ? 12 : props.size === 'medium' ? 16 : 20,
    }]}>{props.children}</Text>
  );
}


export default CustomText;
