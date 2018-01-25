#include "stdint.h"

#ifndef __BOOTLOADER_H
#define __BOOTLOADER_H

void     Bootloader_start(void);
uint8_t  Bootloader_write(uint32_t address, uint8_t *data, uint8_t datalength);
void     Bootloader_erase(void);
uint32_t Bootloader_get_application_crc(void);
uint8_t  Bootloader_validate_application(void);
void     Bootloader_save_checksum(uint32_t crc);
#endif /* __BOOTLOADER_H */
