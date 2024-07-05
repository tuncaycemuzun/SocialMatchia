import React from 'react';
import { TouchableOpacity, TouchableOpacityProps } from 'react-native';

interface ButtonProps extends TouchableOpacityProps {
  children: React.ReactNode;
}

const Button: React.FC<ButtonProps> = ({ children, ...props }) => {
  return (
    <TouchableOpacity activeOpacity={0.8} {...props}>
      {children}
    </TouchableOpacity>
  );
};


export default Button;
