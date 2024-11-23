import * as React from 'react';
import { NavigationContainer } from '@react-navigation/native';
import { createNativeStackNavigator } from '@react-navigation/native-stack';
import { createBottomTabNavigator } from '@react-navigation/bottom-tabs';
import { FontAwesomeIcon } from '@fortawesome/react-native-fontawesome';
import { faHome, faUser } from '@fortawesome/free-solid-svg-icons';

import { Colors } from '@utils';

import { SignIn, SignUp, Onboard, Home, EmailSignUp, Profile } from '@screens';

const Stack = createNativeStackNavigator();
const Tab = createBottomTabNavigator();

function MainTabNavigator() {
  return (
    <Tab.Navigator
      screenOptions={({ route }) => ({
        tabBarIcon: ({ focused, color, size }) => {
          let icon : any = faHome; 

          if (route.name === 'Home') {
            icon = faHome;
          } else if (route.name === 'Profile') {
            icon = faUser;
          }
          size = focused ? 28 : 24;
          color = focused ? Colors.red.main : Colors.lightGray; 

          return <FontAwesomeIcon  icon={icon} size={size} color={color} />;
        },
        tabBarActiveTintColor: Colors.red.main, 
        tabBarInactiveTintColor: '#8e8e93', 
        tabBarStyle: {
          height: 60,
          paddingBottom: 5,
        },
        headerShown: false,
      })}
    >
      <Tab.Screen name="Home" component={Home} />
      <Tab.Screen name="Profile" component={Profile} />
    </Tab.Navigator>
  );
}


export default function Router() {
  return (
    <NavigationContainer>
      <Stack.Navigator
        screenOptions={{
          headerShown: false,
          animation: 'slide_from_right',
        }}
        initialRouteName="HomeTabs" // Adjust initial route if needed
      >
        <Stack.Screen name="Onboard" component={Onboard} />
        <Stack.Screen name="SignUp" component={SignUp} />
        <Stack.Screen name="SignIn" component={SignIn} />
        <Stack.Screen name="EmailSignUp" component={EmailSignUp} />
        <Stack.Screen name="HomeTabs" component={MainTabNavigator} />
      </Stack.Navigator>
    </NavigationContainer>
  );
}
