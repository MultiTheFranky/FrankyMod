#include "script_component.hpp"
/*
   * Author: Franky
   * Start Prometheus info loop
   *
   * Arguments:
   * None
   *
   * Return Value:
   * None
   *
   * Example:
   * [] call fr4_monitoring_fnc_prometheusLoop
   *
   * Public: No
*/

if (!isServer) exitWith {};

private _serverName = serverName call EFUNC(common,minifyString);

private _dataToWrite = [
  [format ["fr4_%1_players", _serverName], "Number of players", count allplayers] joinstring ":",
  [format ["fr4_%1_server_fps", _serverName], "Server FPS", floor diag_fps] joinstring ":",
  [format ["fr4_%1_ai", _serverName], "Number of AI", {
    !isPlayer _x
  } count allunits] joinstring ":",
  [format ["fr4_%1_objects", _serverName], "Number of objects", count allMissionObjects "All"] joinstring ":",
  [format ["fr4_%1_blufor", _serverName], "Number of units blufor", blufor countSide allUnits] joinstring ":",
  [format ["fr4_%1_opfor", _serverName], "Number of units opfor", opfor countSide allUnits] joinstring ":",
  [format ["fr4_%1_ind", _serverName], "Number of units ind", independent countSide allUnits] joinstring ":"
];

"prometheus" callExtension ["data", _dataToWrite];

[FUNC(prometheusLoop), [], 5] call CBA_fnc_waitAndExecute;