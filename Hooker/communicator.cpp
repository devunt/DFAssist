#include "communicator.h"

HANDLE Communicator::hPipe;


void Communicator::Connect(wchar_t* lpszPipename) {

    hPipe = CreateFile(lpszPipename, GENERIC_WRITE, NULL, 0, OPEN_EXISTING, 0, NULL);

    if (hPipe != INVALID_HANDLE_VALUE) {

    }
    else {
        if (GetLastError() != ERROR_PIPE_BUSY)
        {
            Log::d(L"Could not open pipe. GLE=%d", GetLastError());
            return;
        }

        if (!WaitNamedPipe(lpszPipename, 20000))
        {
            Log::d(L"Could not open pipe: 20 second wait timed out.");
            return;
        }
    }
    Log::d(L"Pipe connection established");
}

void Communicator::Disconnect() {
    CloseHandle(hPipe);
    Log::d(L"Pipe connection terminated.");
}

void Communicator::Send(void* structure, DWORD size) {
    char newbuf[32];

    memcpy_s(newbuf, 4, &size, 4);
    memcpy_s(newbuf + 4, 32 - 4, structure, size);

    Send(newbuf, size + 4);
}

void Communicator::Send(const char* buf, DWORD buflen) {
    DWORD bytesWritten = 0;
    if (!WriteFile(hPipe, buf, buflen, &bytesWritten, NULL)) {
        Log::d(L"GLE = %d", GetLastError());
        Worker::Stop();
    }
}