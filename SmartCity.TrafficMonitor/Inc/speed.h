#include <stdint.h>

typedef struct
{
	uint16_t SENSOR_ONE_AVERAGE;
	uint16_t SENSOR_ONE_MIN;
	uint16_t SENSOR_ONE_MAX;
	uint16_t SENSOR_ONE_SPEED;	
	uint16_t SENSOR_TWO_AVERAGE;
	uint16_t SENSOR_TWO_MIN;
	uint16_t SENSOR_TWO_MAX;
	uint16_t SENSOR_TWO_SPEED;	
} Doppler_t;

void TIM5_IRQHandler(void);
void doppler_state_set(char sensor, char state);
