import * as React from 'react';
import { NavigationContainer } from '@react-navigation/native';
import { createNativeStackNavigator } from '@react-navigation/native-stack';

import { Login, Register } from './screens';

const Stack = createNativeStackNavigator();

export default function Router() {
	return (
		<NavigationContainer>
			<Stack.Navigator
				screenOptions={{
					headerShown: false
				}}
				initialRouteName='Register'>
				<Stack.Screen name="Register" component={Register} />
				<Stack.Screen name="Login" component={Login} />
			</Stack.Navigator>
		</NavigationContainer>
	);
}