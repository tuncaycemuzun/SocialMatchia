import { Text, TextProps } from 'react-native'
import React from 'react'
import { Colors, Dimensions } from '@utils';

interface CustomTextProps extends TextProps {
  children: React.ReactNode;
  fontWeight?: 'bold' | 'normal';
  fontSize?: number;
  textAlign?: 'center' | 'left' | 'right';
  color?: string;
  padding? : number;
}

const CustomText = ({
  children,
  fontWeight = 'normal',
  fontSize = Dimensions.normal,
  style,
  textAlign,
  color = Colors.black,
  padding,
  ...restProps
}: CustomTextProps) => {
  return (
    <Text style={[{ fontSize, fontWeight, textAlign, color, padding }, style]} {...restProps}>
      {children}
    </Text>
  );
};

export default CustomText