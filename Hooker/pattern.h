#pragma once
#include <vector>
#include <Windows.h>


class Pattern {
public:
    static const std::vector<uint8_t> MAINCLASS;
    static const std::vector<uint8_t> PACKETINFLATER;
    static const std::vector<uint8_t> HWND;

    static size_t Search(std::vector<uint8_t> pattern);
    static size_t Search(std::vector<uint8_t> pattern, int count);
};