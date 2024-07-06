import React from 'react';
import { SafeAreaView } from 'react-native';
import Toast from 'react-native-toast-message';
import Router from './router';

function App(): React.JSX.Element {
  return (
    <SafeAreaView style={{ flex: 1 }}>
      <Router />
      <Toast/>
    </SafeAreaView>
  );
}



export default App;
