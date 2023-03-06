private _category = format ["FR4 %1", localize LSTRING(DisplayName)];

[QGVAR(enabled), "CHECKBOX", [LSTRING(enabled_DisplayName), LSTRING(enabled_Description)], _category, false, 1] call CBA_fnc_addSetting;

[QGVAR(endpoint), "EDITBOX", [LSTRING(endpoint_DisplayName), LSTRING(endpoint_Description)], _category, "localhost", 1] call CBA_fnc_addSetting;

[QGVAR(port), "EDITBOX", [LSTRING(port_DisplayName), LSTRING(port_Description)], _category, "9091", 1] call CBA_fnc_addSetting;