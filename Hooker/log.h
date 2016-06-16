#pragma once
#include <stdio.h>
#include <Windows.h>

class Log {
public:
    static void d(const wchar_t *fmt, ...) {
#ifndef NDEBUG
        va_list args;
        va_start(args, fmt);

        wchar_t wbuf1[256], wbuf2[256];
        vswprintf(wbuf1, 256, fmt, args);
        swprintf(wbuf2, 256, L"[RDF] %s", wbuf1);

        OutputDebugString(wbuf2);
        va_end(args);
#else
        return;
#endif
    }
};