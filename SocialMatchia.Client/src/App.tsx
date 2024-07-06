import React from 'react';
import { SafeAreaView } from 'react-native';
import { PaperProvider, MD3LightTheme as DefaultTheme, } from 'react-native-paper';
import Toast from 'react-native-toast-message';
import Router from './router';

const theme = {
  ...DefaultTheme,
  colors: {
    ...DefaultTheme.colors,
  },
};

function App(): React.JSX.Element {
  return (
    <PaperProvider theme={theme}>
      <SafeAreaView style={{ flex: 1 }}>
        <Router />
        <Toast />
      </SafeAreaView>
    </PaperProvider>
  );
}



export default App;
