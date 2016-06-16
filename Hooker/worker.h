#pragma once
#include <atomic>
#include <thread>
#include <queue>
#include "communicator.h"
#include "hooker.h"
#include "log.h"
#include "structure.h"
#include "utils.h"


class Worker
{
private:
    static std::thread* task;
    static std::queue<std::vector<uint8_t>> buffer_queue;
    static std::atomic_bool stopSignal;

private:
    static void Task();

public:
    static void Start();
    static void Stop();
    static void Push(std::vector<uint8_t> buffer);
};

