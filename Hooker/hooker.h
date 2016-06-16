#pragma once
#include <inttypes.h>
#include <vector>
#include "communicator.h"
#include "log.h"
#include "MinHook.h"
//#include "pattern.h"
#include "structure.h"
#include "worker.h"
#include "utils.h"

#if defined _M_X64
#pragma comment(lib, "libMinHook.x64.lib")
#elif defined _M_IX86
#pragma comment(lib, "libMinHook.x86.lib")
#endif


template <typename T>
inline MH_STATUS MH_CreateHookEx(LPVOID pTarget, LPVOID pDetour, T** ppOriginal)
{
    return MH_CreateHook(pTarget, pDetour, reinterpret_cast<LPVOID*>(ppOriginal));
}

class Hooker
{
private:
    typedef int(__fastcall *TypeInflatePacket)(int64_t, int *, int64_t, int);
    static TypeInflatePacket fpInflatePacket;
    static int __fastcall HkInflatePacket(int64_t, int *, int64_t, int);

public:
    static HWND hWnd;

public:
    static void Initialise();
    static void Uninitialise();
};
