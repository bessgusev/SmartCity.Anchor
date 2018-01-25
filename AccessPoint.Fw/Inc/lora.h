#include <stdint.h>
void Lora_Init(void);
void Lora_Polling(unsigned char *buffer, unsigned int *dlen);
void Rfm_Send(uint8_t *data, uint8_t length);
uint8_t Rfm_Read(uint8_t reg);
void Rfm_Write(uint8_t reg, uint8_t data);
