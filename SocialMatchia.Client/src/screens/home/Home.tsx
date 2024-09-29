import { Colors, Dimensions } from '@utils';
import React from 'react'
import { View, Text, StyleSheet } from 'react-native'
import { Header } from './components';

const Home = () => {
  return (
    <View style={styles.container}>
      <Header>

      </Header>
    </View>
  )
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: Colors.white,
    padding: Dimensions.large
  },
});

Home.displayName = 'Home'
export default Home