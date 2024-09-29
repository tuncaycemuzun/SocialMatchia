import { StyleSheet, View } from 'react-native'
import React from 'react'
import { Colors, Dimensions } from '@utils'
import { FontAwesomeIcon } from '@fortawesome/react-native-fontawesome'
import { faCoins, faPlus } from '@fortawesome/free-solid-svg-icons'
import { Button, Text } from '@components'

const CoinInfo = () => {
  return (
    <Button onPress={() => {
      console.log('add coins')
    }} style={styles.container}>
      <FontAwesomeIcon icon={faCoins} size={Dimensions.ml} color={Colors.red.main} />
      <Text fontWeight='bold'>20</Text>
      <View style={styles.addCoinButton}>
        <FontAwesomeIcon icon={faPlus} size={Dimensions.normal} color={Colors.red.main} />
      </View>
    </Button>
  )
}

const styles = StyleSheet.create({
  container: {
    flexDirection: 'row',
    justifyContent: 'space-around',
    alignItems: 'center',
    gap: 15,
    borderWidth: 1,
    height: '100%',
    borderRadius: 10,
    paddingHorizontal: 25,
    borderColor: Colors.lightGray
  },
  addCoinButton: {
    position: 'absolute',
    right: -10,
    top: -10,
    borderWidth: 1,
    borderRadius: Dimensions.xSmall,
    borderColor: Colors.red.main,
    backgroundColor: Colors.white,
    padding: 5,
  }
})

export default CoinInfo