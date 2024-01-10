import React from 'react';
import {
  SafeAreaView, Text,
} from 'react-native';
import Router from './router';

function App(): React.JSX.Element {
  return (
    <SafeAreaView style={{ flex: 1 }}>
      <Router />
    </SafeAreaView>
  );
}



export default App;
