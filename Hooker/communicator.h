#pragma once
#include <Windows.h>
#include "log.h"
#include "worker.h"


class Communicator
{
private:
    static HANDLE hPipe;

public:
    static void Connect(wchar_t* lpszPipename);
    static void Disconnect();
    static void Send(void* structure, DWORD size);
    static void Send(const char* buffer, DWORD buflen);
};
