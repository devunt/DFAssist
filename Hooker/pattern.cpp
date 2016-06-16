#include "pattern.h"

// 0xff -> wildcard

const std::vector<uint8_t> Pattern::MAINCLASS = {
    0x47, 0x61, 0x6D, 0x65, 0x4D, 0x61, 0x69, 0x6E,
    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
};

const std::vector<uint8_t> Pattern::PACKETINFLATER = {
//  0x48, 0x8D, 0x54, 0x24, 0x20,   // lea rdx, qword ptr ss:[rsp+20]
    0x41, 0x83, 0xE9, 0x28,         // sub r9d, 28h
    0x48, 0x8B, 0xCF,               // mov rcx, rdi
    0xE8,                           // call PacketInflater
};

const std::vector<uint8_t> Pattern::HWND = {
    0xFF, 0x15, 0xFF, 0xFF, 0xFF, 0xFF, // call qword ptr ds:[<&UpdateWindow>]
    0x48, 0x8D, 0x54, 0x24, 0x68,       // lea rdx,qword ptr ss:[rsp + 68]
    0x48, 0x8B, 0xCB,                   // mov rcx, rdi
    0x48, 0x89, 0x1D,                   // mov qword ptr ds:[&hWnd],rbx
};

size_t Pattern::Search(const std::vector<uint8_t> pattern) {
    return Pattern::Search(pattern, 1);
}

size_t Pattern::Search(const std::vector<uint8_t> pattern, int count) {
    int matchcount = 0;

    HMODULE module = GetModuleHandleA("ffxiv_dx11.exe");
    size_t base = reinterpret_cast<size_t>(module);

    for (size_t offset = 0; offset < 0x1280000; offset++)
    {
        int m = 0;
        for (size_t i = 0; i < pattern.size(); i++) {
            if ((pattern[i] != 0xff) && (pattern[i] != *(uint8_t *)(base + offset + i))) {
                break;
            }
            if (++m == pattern.size() && ++matchcount == count) {
                return (base + offset + i + 1);
            }
        }
    }
    return NULL;
}