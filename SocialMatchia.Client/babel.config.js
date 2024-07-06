module.exports = {
  presets: ['module:@react-native/babel-preset'],
  plugins: [
    [
      require.resolve('babel-plugin-module-resolver'),
      {
        alias: {
          src: './src',
          '@components': './src/components',
          '@screens': './src/screens',
          '@assets': './src/assets',
          '@utils': './src/utils',
          '@services': './src/services'
        }
      }
    ],
    'react-native-paper/babel'
  ]
};
