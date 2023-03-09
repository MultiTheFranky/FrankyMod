#include "script_component.hpp"
/*
 * Author: Franky
 * Start Prometheus server
 *
 * Arguments:
 * None
 *
 * Return Value:
 * None
 *
 * Example:
 * [] call fr4_monitoring_fnc_initPrometheus
 *
 * Public: No
*/

if (!isServer) exitWith {};

diag_log str("prometheus" callExtension ["start", [GVAR(endpoint), GVAR(port)]]);