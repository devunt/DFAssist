#pragma once
#include <inttypes.h>

#pragma pack(push, 1)
struct df_ping_t {
    uint8_t type = 1;
};

#pragma pack(push, 1)
struct df_started_t {
    uint8_t type = 2;
    uint16_t instances[5];
};

#pragma pack(push, 1)
struct df_updated_t {
    uint8_t type = 3;
    uint16_t instance;
    uint8_t tank;
    uint8_t dps;
    uint8_t healer;
};

#pragma pack(push, 1)
struct df_matched_t {
    uint8_t type = 4;
    uint16_t instance;
};

#pragma pack(push, 1)
struct df_aborted_t {
    uint8_t type = 5;
};