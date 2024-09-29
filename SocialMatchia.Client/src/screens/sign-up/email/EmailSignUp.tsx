import React, { useState } from 'react';
import { StyleSheet, View } from 'react-native';

import { Wizard } from 'react-use-wizard';

import { Colors } from '@utils';
import { ProfileDetail, Gender, EmailAndPassword, Interests } from './index';


//create interface values user info values
interface User {
  firstName: string;
  lastName: string;
  birthday: Date;
  images: string[];
  gender: string;
  interests: string[];
  email: string;
  password: string;
  rePassword: string;
}

const EmailSignUp = () => {
  const [user, setUser] = useState<User | null>(null);
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
    padding: 30,
    backgroundColor: Colors.white
  }
})

EmailSignUp.displayName = 'EmailSignUp';
export default EmailSignUp;
