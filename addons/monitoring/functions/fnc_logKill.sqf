#include "script_component.hpp"
/*
	* Author: Franky
	* Add a kill to the Prometheus Data
	*
	* Arguments:
	* 0: <STRING> name of the killer
	* 1: <NUMBER> Number of kills
	*
	* Return Value:
	* None
	*
	* Example:
	* [] call fr4_monitoring_fnc_logKill
	*
	* Public: No
*/

// Check the data
if (!params [
	["_killer", "", [""]],
	["_kills", 0, [0]]
	]) exitWith {
	ERROR_2("fr4_monitoring_fnc_logKill | Missing parameter [%1, %2]", _killer, _kills);
};

// Add the data
"prometheus" callExtension
["data",
	[
		[format ["fr4_kill_%1", _killer], format ["Number of kills from %1", _killer], _kills] joinstring ":"
	]
];