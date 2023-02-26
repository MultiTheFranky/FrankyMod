// COMPONENT should be defined in the script_component.hpp and included BEFORE this hpp

#define MAINPREFIX z
#define PREFIX fr4

#include "script_version.hpp"

#define VERSION MAJOR.MINOR
#define VERSION_STR MAJOR.MINOR.PATCHLVL.BUILD
#define VERSION_AR MAJOR,MINOR,PATCHLVL,BUILD

#define FR4_TAG FR4

// MINIMAL required version for the Mod. Components can specify others..
#define REQUIRED_VERSION 1.56

#ifdef COMPONENT_BEAUTIFIED
    #define COMPONENT_NAME QUOTE(fr4 - COMPONENT_BEAUTIFIED)
#else
    #define COMPONENT_NAME QUOTE(fr4 - COMPONENT)
#endif

#define REQUIRED_CBA_VERSION { 3, 5, 0 }
#define REQUIRED_ACE_VERSION { 3, 11, 0 }