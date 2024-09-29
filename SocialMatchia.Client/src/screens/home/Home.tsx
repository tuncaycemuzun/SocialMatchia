import { Colors, Dimensions } from '@utils';
import React from 'react'
import { View,  StyleSheet } from 'react-native'
import { Header, Swipe } from './components';

const Home = () => {
  return (
    <View style={styles.container}>
      <Header/>
      <Swipe/>
    </View>
  )
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: Colors.white,
    padding: Dimensions.large,
    gap: Dimensions.large,
  },
});

Home.displayName = 'Home'
export default Home