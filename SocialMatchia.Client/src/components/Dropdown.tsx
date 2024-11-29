import { Colors, Dimensions } from '@utils';
import React, { useState } from 'react';
import { StyleSheet, Text, View } from 'react-native';
import { Dropdown } from 'react-native-element-dropdown';

const data = [
  { label: 'Item 1', value: '1' },
  { label: 'Item 2', value: '2' },
  { label: 'Item 3', value: '3' },
  { label: 'Item 4', value: '4' },
  { label: 'Item 5', value: '5' },
  { label: 'Item 6', value: '6' },
  { label: 'Item 7', value: '7' },
  { label: 'Item 8', value: '8' },
];

interface DropdownComponentProps {
  onChange: (item: any) => void;
  search: boolean;
  label: string;
  onFocus: () => void;
  onBlur: () => void;
  errorMessage?: string;
  touched?: boolean;
  value?: any;
}

const DropdownComponent = (props: DropdownComponentProps) => {
  const [value, setValue] = useState(null);
  const [isFocus, setIsFocus] = useState(false);
  const [hasError, setHasError] = useState(props.touched || (props.errorMessage && props.errorMessage?.length > 0) ? true : false);

  console.log(props)

  const renderLabel = () => {
    return (
      <Text style={[styles.label, isFocus && { color: Colors.lightBlue }]}>
        {props.label}
      </Text>
    );
  };

  return (
    <View style={styles.container}>
      {renderLabel()}
      <Dropdown
        style={[styles.dropdown, hasError == true && { borderColor: 'red' }, isFocus && { borderColor: Colors.lightBlue }]}
        placeholderStyle={styles.placeholderStyle}
        selectedTextStyle={styles.selectedTextStyle}
        inputSearchStyle={styles.inputSearchStyle}
        iconStyle={styles.iconStyle}
        data={data}
        search={props.search}
        maxHeight={300}
        labelField="label"
        valueField="value"
        placeholder={!isFocus ? 'Lütfen Seçin' : ''}
        searchPlaceholder="Ara..."
        value={props.value}
        onFocus={() => {
          setIsFocus(true);
          props.onFocus();
        }}
        onBlur={() => {
          setIsFocus(false);
          props.onBlur();
        }}
        onChange={(item) => {
          props.onChange(item);
        }}
      />
      {props.touched && props.errorMessage ? (
        <Text style={styles.errorText}>{props.errorMessage}</Text>
      ) : null}
    </View>
  );
};

export default DropdownComponent;

const styles = StyleSheet.create({
  container: {
    backgroundColor: 'white',
    marginBottom: Dimensions.small,
  },
  dropdown: {
    height: 50,
    borderColor: Colors.lightGray,
    borderWidth: 1,
    borderRadius: Dimensions.xSmall,
    paddingHorizontal: 8,
  },
  label: {
    position: 'absolute',
    backgroundColor: 'white',
    left: 5,
    top: -10,
    zIndex: 999,
    paddingHorizontal: 8,
    fontSize: 14,
  },
  placeholderStyle: {
    fontSize: 16,
    color: Colors.lightGray,
  },
  selectedTextStyle: {
    fontSize: 16,
  },
  iconStyle: {
    width: 20,
    height: 20,
  },
  inputSearchStyle: {
    height: 40,
    fontSize: 16,
  },
  errorText: {
    color: Colors.red.main,
    fontSize: 12,
    marginTop: 5,
  },
});
