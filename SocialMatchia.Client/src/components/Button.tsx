import { Colors } from '@utils';
import React from 'react';
import { TouchableOpacity, TouchableOpacityProps } from 'react-native';
import { Dimensions } from '@utils';

interface ButtonProps extends TouchableOpacityProps {
  children?: React.ReactNode;
}

const Button: React.FC<ButtonProps> = ({
  children,
  ...props
}) => {
  return (
    <TouchableOpacity style={
      [{
        width: '100%',
        backgroundColor: Colors.red.main,
        borderRadius: Dimensions.small,
        padding: Dimensions.medium,
        alignItems: 'center'
      },
      props.style]} {...props} activeOpacity={0.8}>
      {children}
    </TouchableOpacity>
  );
};

Button.displayName = 'Button';
export default Button;