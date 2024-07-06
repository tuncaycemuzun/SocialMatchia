import React from 'react';
import { SafeAreaView } from 'react-native';
import { PaperProvider} from 'react-native-paper';
import Toast from 'react-native-toast-message';
import Router from './router';



function App(): React.JSX.Element {
  return (
    <PaperProvider>
      <SafeAreaView style={{ flex: 1 }}>
        <Router />
        <Toast />
      </SafeAreaView>
    </PaperProvider>
  );
}



export default App;
