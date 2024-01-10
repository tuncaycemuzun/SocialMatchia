import { StyleSheet } from "react-native";
import { colors } from "../utils";

const globalStyles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: "#fff",
    alignItems: "center",
    paddingVertical: 20,
    paddingHorizontal:10
  },
  textColor: {
    color: "black",
  },
  textInput: {
    borderWidth: 1,
    borderColor: colors.frenchGray,
    padding: 10,
    margin: 10,
    width: "100%",
    borderRadius: 8,
    color:colors.black
  }
});

export default globalStyles;
