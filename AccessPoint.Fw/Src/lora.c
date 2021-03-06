#include "stm32f0xx_hal.h"
#include "lora.h"

extern SPI_HandleTypeDef hspi2;

void Rfm_Write(uint8_t reg, uint8_t data)
{
		uint8_t buffer[4];
		buffer[0] = reg | 0x80;
		buffer[1] = data;
		HAL_GPIO_WritePin(GPIOB, GPIO_PIN_12, GPIO_PIN_RESET);
		HAL_SPI_Transmit(&hspi2, buffer, 2, 100);
		HAL_GPIO_WritePin(GPIOB, GPIO_PIN_12, GPIO_PIN_SET);
}

uint8_t Rfm_Read(uint8_t reg)
{
		HAL_SPIEx_FlushRxFifo(&hspi2);
		uint8_t bb[3]={0,0,0};
		uint8_t bt[3]={0,0,0};		
		bb[0] = reg;	
	  HAL_GPIO_WritePin(GPIOB, GPIO_PIN_12, GPIO_PIN_RESET);
	  HAL_StatusTypeDef status = HAL_SPI_TransmitReceive(&hspi2, bb ,bt, 2, 0x1000);		
	  HAL_GPIO_WritePin(GPIOB, GPIO_PIN_12, GPIO_PIN_SET);
		return bt[1];
}

void Rfm_Send(uint8_t *data, uint8_t length)
{
		Rfm_Write(0x40, 0x40);
    Rfm_Write(0x22, length);
		Rfm_Write(0xD, Rfm_Read(0xE));
		for(int i=0; i<length;i++)Rfm_Write(0, data[i]);
		Rfm_Write(0x1, 0x83);
}

void Lora_Init(void)
{
		HAL_GPIO_WritePin(GPIOB, GPIO_PIN_8, GPIO_PIN_SET);
		Rfm_Write(0x01, 0);
		while(Rfm_Read(0x01) != 0)
		{
				HAL_Delay(1);
		}
		Rfm_Write(0x1, 0x80);
		Rfm_Write(0x1, 0x81);
		Rfm_Write(0x6, 0xD9);
		Rfm_Write(0x7, 0x6);
		Rfm_Write(0x8, 0x8B);
		Rfm_Write(0x9, 0xFF);
		Rfm_Write(0x1D, 0x72);
		Rfm_Write(0x1E, 0x74);
		Rfm_Write(0x20, 0x0);
		Rfm_Write(0x21, 0x8);
		Rfm_Write(0x39, 0x34);
		Rfm_Write(0x33, 0x27);
		Rfm_Write(0x3B, 0x1D);
		Rfm_Write(0xE, 0x80);
		Rfm_Write(0xF, 0x0);
		Rfm_Write(0x1, 0x85);
		HAL_GPIO_WritePin(GPIOB, GPIO_PIN_8, GPIO_PIN_RESET);
}

void Lora_Polling(unsigned char *buffer, unsigned int *dlen)
{
		buffer[0] = Rfm_Read(0x18);
		buffer[1] = Rfm_Read(0x12);
		buffer[2] = Rfm_Read(0x1A);
		buffer[3] = Rfm_Read(0x1B);
	  Rfm_Write(0x12, buffer[1]);
		
		if ((buffer[1] & 0x40)){
				uint8_t data_len = Rfm_Read(0x13);
				Rfm_Write(0xD, Rfm_Read(0x10));
				for(int i=0;i<data_len;i++){
						buffer[4+i] = Rfm_Read(0);
				}
				*dlen = 4+data_len;
		}else{			
				*dlen = 2;
		}
		Rfm_Write(0x1, 0x85);
}
