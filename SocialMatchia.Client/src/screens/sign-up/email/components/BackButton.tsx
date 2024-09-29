import { faChevronLeft } from '@fortawesome/free-solid-svg-icons'
import { FontAwesomeIcon } from '@fortawesome/react-native-fontawesome'
import { Colors } from '@utils'
import React from 'react'
import { StyleSheet, TouchableOpacity } from 'react-native'

interface BackButtonProps {
  onPress: () => void;
}

const BackButton = (props: BackButtonProps) => {
  return (
    <TouchableOpacity onPress={props.onPress} style={styles.backButton}>
      <FontAwesomeIcon icon={faChevronLeft} size={20} color={Colors.red.main} />
    </TouchableOpacity>
  )
}

const styles = StyleSheet.create({
  backButton: {
    position: 'absolute',
    borderStyle: 'solid',
    borderWidth: 1,
    padding: 10,
    borderRadius: 10,
    borderColor: Colors.lightGray,
    zIndex:99
  },
})

export default BackButton