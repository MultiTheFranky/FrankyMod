/*
* Author: Franky
* Function to clear an string from all special characters
*
* Arguments:
* 0: <STRING> Text to clear
*
* Return Value:
* <STRING> Cleared text
*
* Example:
* ["Text%·"] call fr4_common_fnc_minifyString;
*
* Public: No
*/

private _text = ((toLower _this) splitString "-");
((_text select ((count _text) - 1)) splitString (toString [32, 9, 13, 10]) joinString "")