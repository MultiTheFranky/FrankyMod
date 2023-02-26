class CfgSettings {
    class CBA {
        class Versioning {
            class FR4 {
                class dependencies {
                    //FR4 will hard exit if this is missing
                    CBA[] = {"cba_main", REQUIRED_CBA_VERSION, "(true)"};
                    ACE[] = {"ace_main", REQUIRED_ACE_VERSION, "(true)"};
                };
            };
        };
    };
};