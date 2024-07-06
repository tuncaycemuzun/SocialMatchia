import React from 'react';
import { TouchableOpacity, TouchableOpacityProps } from 'react-native';

interface ButtonProps extends TouchableOpacityProps {
  children: React.ReactNode;
}

const Button: React.FC<ButtonProps> = ({
  children,
  ...props
}) => {

  return (
    <TouchableOpacity {...props} style={props.style} activeOpacity={0.8} >
      {children}
    </TouchableOpacity>
  );
};

Button.displayName = 'Button';
export default Button;