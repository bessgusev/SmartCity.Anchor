#include "speed.h"
#include "stm32f1xx_hal.h"
#include "stdlib.h"
extern TIM_HandleTypeDef htim5;
#define BUFFER_SIZE 1024

uint8_t sensor_buffer[BUFFER_SIZE];

Doppler_t SpeedSensors;

char current_speed_sensor = 0;
char speed_value_buffer[5];


unsigned char cursor = 0;

char speed_status_line[18] = "Speed(km/h):    00";

uint8_t temp_buffer[5];
uint8_t temp_cursor = 0;
extern uint8_t lora_data[8];

uint16_t last_value = 0;
void sensors_proccess()
{
		sensor_buffer[cursor++] = 4000*0.03*3.6/__HAL_TIM_GetCompare(&htim5, TIM_CHANNEL_1);
}

unsigned char seconds = 0;

void speed_proccess(void)
{		
		if(seconds++>10){			
					uint8_t max = 0;
					uint8_t min = 255;
					uint32_t sum = 0;
					for(int i=0;i<cursor;i++)
					{
							sum += sensor_buffer[i];
							if(max<sensor_buffer[i])max = sensor_buffer[i];
							if(min>sensor_buffer[i])min = sensor_buffer[i];
					}
					if(cursor!=0)SpeedSensors.SENSOR_ONE_AVERAGE = sum/cursor; else SpeedSensors.SENSOR_ONE_AVERAGE = 0;
					SpeedSensors.SENSOR_ONE_MAX = max;
					SpeedSensors.SENSOR_ONE_MIN = min;
					current_speed_sensor = 2;		
					lora_data[8] = SpeedSensors.SENSOR_ONE_AVERAGE;
					speed_status_line[12] = SpeedSensors.SENSOR_ONE_AVERAGE/10+0x30;
					speed_status_line[13] = SpeedSensors.SENSOR_ONE_AVERAGE%10+0x30;
					
					speed_status_line[16] = max/10+0x30;
					speed_status_line[17] = max%10+0x30;
					cursor = 0;
		}	
}

void TIM5_IRQHandler(void)
{
	if(__HAL_TIM_GET_IT_SOURCE(&htim5, TIM_IT_CC1) == SET){
		sensors_proccess();
		
	}	
	HAL_GPIO_TogglePin(LED1_GPIO_Port, LED1_Pin);
	__HAL_TIM_SetCounter(&htim5, 0);
  HAL_TIM_IRQHandler(&htim5);
}
