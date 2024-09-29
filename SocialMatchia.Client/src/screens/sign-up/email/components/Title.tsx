import { Colors, Dimensions } from '@utils';
import React from 'react'
import { StyleSheet, Text } from 'react-native';

interface TitleProps {
  title: string;
  subTitle?: string;
}

const Title = (props: TitleProps) => {
  return (
    <>
      <Text style={styles.title}>{props.title}</Text>
      {props.subTitle ? <Text style={styles.subtitle}>{props.subTitle}</Text> : <></>}
    </>
  )
}

const styles = StyleSheet.create({
  title: {
    marginTop: Dimensions.medium,
    fontSize: Dimensions.large,
    fontWeight: 'bold',
    marginBottom: Dimensions.medium,
    fontFamily: 'bold',
    color: Colors.black,
  },
  subtitle: {
    fontSize: Dimensions.medium,
    color: Colors.black,
    marginBottom: Dimensions.medium,
  },
});

export default Title