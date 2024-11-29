import React from 'react'
import { View, StyleSheet } from 'react-native'

import { FontAwesomeIcon } from '@fortawesome/react-native-fontawesome'
import { faSliders, faRotate } from '@fortawesome/free-solid-svg-icons'
import { NavigationProp, ParamListBase, useNavigation } from '@react-navigation/native'

import { Button } from '@components'
import { Colors, Dimensions } from '@utils'
import CoinInfo from './CoinInfo'

const Header = () => {
  const navigation = useNavigation<NavigationProp<ParamListBase>>();
  return (
    <View style={styles.container}>
      <Button style={{ width: 'auto' }}>
        <FontAwesomeIcon icon={faRotate} size={Dimensions.large} color={Colors.red.main} />
      </Button>
      <View style={{
        flexDirection: 'column',
        justifyContent: 'space-between',
        alignItems: 'center',
      }}>
        <CoinInfo />
      </View>
      <Button
        onPress={() => {
          navigation.navigate('Settings')
        }}
        style={{
          width: 'auto',
        }}>
        <FontAwesomeIcon icon={faSliders} size={Dimensions.large} color={Colors.red.main} />
      </Button>
    </View>
  )
}

const styles = StyleSheet.create({
  container: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    height: 50,
  }
})


export default Header