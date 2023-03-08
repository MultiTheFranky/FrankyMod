class CfgPatches {
    class ADDON {
        name = COMPONENT_NAME;
        units[] = {};
        weapons[] = {};
        requiredVersion = REQUIRED_VERSION;
        requiredAddons[] = {"fr4_main", "cba_settings"};
        author = "";
        authors[] = {""};
        VERSION_CONFIG;
    };
};

class CfgPatches {
    class cba {
        author = "$STR_CBA_Author";
        name = "$STR_CBA_Settings_Component";
        url = "$STR_CBA_URL";
        units[] = {};
        weapons[] = {};
        requiredVersion = 1.0;
        requiredAddons[] = {"cba_settings"};
        version = 1.0;
        authors[] = {"Franky"};
    };
};