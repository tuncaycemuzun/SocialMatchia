import { View, Text, StyleSheet } from 'react-native'
import React from 'react'
import { BackButton } from '@components'
import { Dimensions } from '@utils'

const Header = () => {
  return (
    <View style={styles.container}>
      <BackButton onPress={() => { }} />
      <Text>
        Discover
      </Text>
    </View>
  )
}

const styles = StyleSheet.create({
  container: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
  }
})


export default Header