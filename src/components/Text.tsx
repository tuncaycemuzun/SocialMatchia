import React from 'react';
import { Text } from 'react-native';
import { globalStyles } from '../styles';

interface CustomTextProps {
  children: React.ReactNode;
}

const CustomText = ({ children }: CustomTextProps) => {
  return (
    <Text style={globalStyles.textColor}>{children}</Text>
  );
}

export default CustomText;
