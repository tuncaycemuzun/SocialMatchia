import React from 'react';
import { Text, TouchableOpacity, TouchableOpacityProps, ViewStyle } from 'react-native';
import { colors } from '../utils';
import { FontAwesomeIcon } from '@fortawesome/react-native-fontawesome';
import { IconProp } from '@fortawesome/fontawesome-svg-core';

type ButtonProps = TouchableOpacityProps & {
  text: string;
  size: 'small' | 'medium' | 'large';
  icon?: IconProp;
  iconPosition?: 'left' | 'right';
  iconSize?: number;
  iconColor?: string;
  wFull?: boolean;
  textColor?: string;
};

const Button = (props: ButtonProps) => {


  return (
    <TouchableOpacity
      activeOpacity={0.8}
      {...props}
      style={[
        {
          backgroundColor: colors.royalBlue,
          padding: 10,
          borderRadius: 8,
          marginTop: 10,
          width: props.wFull == true ? '100%' : props.size === 'small' ? 100 : props.size === 'medium' ? 150 : 200,
          alignItems: 'center',
          height: props.size === 'small' ? 40 : props.size === 'medium' ? 50 : 60,
          justifyContent: 'center',
        },
        props.style
      ]}>
      <Text style={{
        color: props.textColor ? props.textColor : colors.ghostWhite,
        fontSize: props.size === 'small' ? 12 : props.size === 'medium' ? 16 : 20,
        display: 'flex',
        alignItems: 'center',
        justifyContent: 'center',
        gap: 10
      }}>
        {props.icon && props.iconPosition === 'left' && <FontAwesomeIcon size={props.iconSize || 18} color={props.iconColor} icon={props.icon} />}
        <Text>{props.text}</Text>
        {props.icon && props.iconPosition === 'right' && <FontAwesomeIcon size={props.iconSize || 18} color={props.iconColor} icon={props.icon} />}
      </Text>

    </TouchableOpacity>
  );
};

export default Button;
