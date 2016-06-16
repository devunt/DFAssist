#include "hooker.h"

HWND Hooker::hWnd;
Hooker::TypeInflatePacket Hooker::fpInflatePacket;


void Hooker::Initialise() {
    Communicator::Connect(L"\\\\.\\pipe\\FFXIV_DX11_DFH_PIPE");

    if (MH_Initialize() != MH_OK) {
        return;
    }

    size_t base = reinterpret_cast<size_t>(GetModuleHandleA("ffxiv_dx11.exe"));

    size_t InflatePacket = base + 0x102FC70;
    hWnd = *reinterpret_cast<HWND *>(base + 0x1441508l);

    Log::d(L"hWnd = %" PRIx64, hWnd);
    Log::d(L"InflatePacket Offset = %" PRIx64, InflatePacket);
    
    MH_CreateHookEx(reinterpret_cast<void *>(InflatePacket), &Hooker::HkInflatePacket, &Hooker::fpInflatePacket);
    MH_EnableHook(reinterpret_cast<void *>(InflatePacket));

    Log::d(L"RDF have been initialised.");
}

void Hooker::Uninitialise() {
    MH_DisableHook(fpInflatePacket);
    MH_Uninitialize();

    Log::d(L"RDF have been unintialised.");
}

int __fastcall Hooker::HkInflatePacket(int64_t out, int *p_outlen, int64_t in, int a4) {
    int ret = fpInflatePacket(out, p_outlen, in, a4);
    size_t total_length = *p_outlen;

    if (total_length < 4096) {
        Worker::Start();

        uint8_t messagebuffer[4096];
        Utils::ReadFFXIVMemory(messagebuffer, out, total_length);

        std::vector<uint8_t> buffer;
        buffer.insert(buffer.end(), &messagebuffer[0], &messagebuffer[total_length]);

        Worker::Push(buffer);
    }

    return ret;
}