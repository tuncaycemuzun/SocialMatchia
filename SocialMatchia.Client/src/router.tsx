import * as React from 'react';
import { NavigationContainer } from '@react-navigation/native';
import { createNativeStackNavigator } from '@react-navigation/native-stack';

import { Login, Register, Onboard } from './screens';

const Stack = createNativeStackNavigator();

export default function Router() {
	return (
		<NavigationContainer>
			<Stack.Navigator
				screenOptions={{
					headerShown: false
				}}
				initialRouteName='Onboard'>
				<Stack.Screen name="Register" component={Register} />
				<Stack.Screen name="Login" component={Login} />
				<Stack.Screen name="Onboard" component={Onboard} />
			</Stack.Navigator>
		</NavigationContainer>
	);
}