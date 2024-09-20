import React from 'react';
import { StyleSheet, View } from 'react-native';

import { Wizard } from 'react-use-wizard';

import { Colors } from '@utils';
import { ProfileDetail, Gender, EmailAndPassword, Interests } from './index';

const EmailSignUp = () => {
  return (
    <View style={styles.container}>
      <Wizard>
        <ProfileDetail />
        <Gender />
        <Interests />
        <EmailAndPassword />
      </Wizard>
    </View>
  )
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    padding: 20,
    backgroundColor: Colors.white
  }
})

EmailSignUp.displayName = 'EmailSignUp';
export default EmailSignUp;
