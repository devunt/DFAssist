#include "hooker.h"


BOOL APIENTRY DllMain(HANDLE hModule, DWORD ul_reason_for_call, LPVOID lpReserved) {
    switch (ul_reason_for_call) {
    case DLL_PROCESS_ATTACH:
        Hooker::Initialise();
        break;
    case DLL_PROCESS_DETACH: 
        Hooker::Uninitialise();
        break;
    }

    return TRUE;
}