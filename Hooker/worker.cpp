#include "worker.h"

std::queue<std::vector<uint8_t>> Worker::buffer_queue;
std::atomic_bool Worker::stopSignal(false);
std::thread* Worker::task;


void Worker::Start()
{
    if (!task) {
        task = new std::thread(&Worker::Task);
    }
}

void Worker::Stop() {
    stopSignal.store(true);
}

void Worker::Push(std::vector<uint8_t> buffer) {
    buffer_queue.push(buffer);
}

void Worker::Task() {
    Log::d(L"Task started.");
    while (!stopSignal.load()) {
        df_ping_t ping;

        if (buffer_queue.empty()) {
            Communicator::Send(&ping, sizeof(ping));
            std::this_thread::sleep_for(std::chrono::seconds(5));
            continue;
        }

        auto messagebuffer = buffer_queue.front();

        size_t offset = 0;
        do {
            auto message = reinterpret_cast<size_t>(&messagebuffer[0] + offset);
            auto length = Utils::GetTypedObject<int>(message, 0);
            offset += length;

            if (length < 48) {
                continue;
            }
            auto opcode = Utils::GetTypedObject<int16_t>(message, 18);

            Log::d(L"opcode = %x / length = %d", opcode, length);
            if (opcode == 0x006C && length == 216) {
                df_started_t structure = {};

                auto code = Utils::GetTypedObject<uint16_t>(message, 32 + 12);
                structure.instances[0] = code;

                Communicator::Send(&structure, sizeof(structure));
            }
            else if (opcode == 0x0074 && length == 240) {
                df_started_t structure = {};

                size_t offset = 192; // 48
                for (int i = 0; i < 5; i++) {
                    auto code = Utils::GetTypedObject<uint16_t>(message, 32 + offset + (i * 2));
                    if (code == 0) {
                        break;
                    }
                    structure.instances[i] = code;
                }

                Communicator::Send(&structure, sizeof(structure));
            }
            else if (opcode == 0x02DE && length == 48) {
                df_updated_t structure = {};

                auto code = Utils::GetTypedObject<uint16_t>(message, 32 + 0);
                auto tank = Utils::GetTypedObject<uint8_t>(message, 32 + 5);
                auto dps = Utils::GetTypedObject<uint8_t>(message, 32 + 6);
                auto healer = Utils::GetTypedObject<uint8_t>(message, 32 + 7);

                structure.instance = code;
                structure.tank = tank;
                structure.dps = dps;
                structure.healer = healer;

                Communicator::Send(&structure, sizeof(structure));
            }
            else if (opcode == 0x0338 && length == 56) {
                df_matched_t structure = {};

                auto code = Utils::GetTypedObject<uint16_t>(message, 32 + 4);

                structure.instance = code;

                Communicator::Send(&structure, sizeof(structure));

                if (GetForegroundWindow() != Hooker::hWnd) {
                    FLASHWINFO flashwinfo;
                    flashwinfo.cbSize = sizeof(flashwinfo);
                    flashwinfo.hwnd = Hooker::hWnd;
                    flashwinfo.dwFlags = FLASHW_TRAY | FLASHW_TIMERNOFG;
                    flashwinfo.uCount = 1;
                    flashwinfo.dwTimeout = 0;

                    FlashWindowEx(&flashwinfo);
                }
            }
            else if (opcode == 0x006F && length == 56) {
                df_aborted_t structure = {};

                Communicator::Send(&structure, sizeof(structure));
            }
            else if (opcode == 0x0070 && length == 64) {
                df_aborted_t structure = {};

                Communicator::Send(&structure, sizeof(structure));
            }
        } while (offset < messagebuffer.size());

        buffer_queue.pop();
    }
    Log::d(L"Task terminated.");

    Communicator::Disconnect();

    HMODULE hModule = GetModuleHandle(L"dfassist");
    FreeLibrary(hModule);
}