#pragma once
#include <codecvt>
#include <Windows.h>


class Utils {
public:
    static bool ReadFFXIVMemory(void* buffer, size_t offset, size_t size) {
        HANDLE hProc = GetCurrentProcess();
        return (ReadProcessMemory(hProc, reinterpret_cast<LPVOID>(offset), buffer, size, NULL) != 0);
    }

    template <typename T>
    static T GetTypedObject(size_t base, size_t offset) {
        return *reinterpret_cast<T *>(base + offset);
    }
    
    static std::wstring utf8_to_utf16(const std::string& str) {
        std::wstring_convert<std::codecvt_utf8<wchar_t>> conv;
        return conv.from_bytes(str);
    }
    static std::string utf16_to_utf8(const std::wstring& wstr) {
        std::wstring_convert<std::codecvt_utf8<wchar_t>> conv;
        return conv.to_bytes(wstr);
    }
};