import * as React from 'react';
import { NavigationContainer } from '@react-navigation/native';
import { createNativeStackNavigator } from '@react-navigation/native-stack';

import { SignIn, SignUp, Onboard } from './screens';

const Stack = createNativeStackNavigator();

export default function Router() {
	return (
		<NavigationContainer>
			<Stack.Navigator
				screenOptions={{
					headerShown: false,
					animation: 'slide_from_right',
				}}
				initialRouteName='Onboard'>
				<Stack.Screen name="SignUp" component={SignUp} />
				<Stack.Screen name="SignIn" component={SignIn} />
				<Stack.Screen name="Onboard" component={Onboard} />
			</Stack.Navigator>
		</NavigationContainer>
	);
}