import React from 'react';
import { View, Button, StyleSheet, NativeModules } from 'react-native';

const {FileLauncher} = NativeModules;

const launchFile = () => {
  FileLauncher.launch("example.txt");
}

const App = () => {
  return (
    <View style={styles.container}>
      <Button
        title="Press me"
        onPress={() => launchFile()}
      />
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center'
  }
});


export default App;
