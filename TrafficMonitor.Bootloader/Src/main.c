#include "stm32f1xx_hal.h"
#include "lora.h"
#include "bootloader.h"

CRC_HandleTypeDef hcrc;
SPI_HandleTypeDef hspi2;

void SystemClock_Config(void);
void Error_Handler(void);
static void MX_GPIO_Init(void);
static void MX_SPI2_Init(void);
static void MX_CRC_Init(void);
static void MX_GPIO_DeInit(void);

int sec_to_boot    = 600;
uint16_t offset    = 0;
uint8_t rx_present = 0;
uint32_t last_address = 0;	

uint32_t HW_CRC32(const uint8_t* pData, size_t count) {
        uint32_t crc;
        uint32_t *p32 = (uint32_t*) pData;
        size_t count32 = count >> 2;
	      CRC->CR |= CRC_CR_RESET;
        while (count32--) {
                CRC->DR = __RBIT(*p32++);
        }
        crc = __RBIT(CRC->DR);
        count = count % 4;
        if (count) {
                CRC->DR = __RBIT(crc);
                switch (count) {
                case 1:
                        CRC->DR = __RBIT((*p32 & 0x000000FF) ^ crc) >> 24;
                        crc = (crc >> 8) ^ __RBIT(CRC->DR);
                        break;
                case 2:
                        CRC->DR = (__RBIT((*p32 & 0x0000FFFF) ^ crc) >> 16);
                        crc = (crc >> 16) ^ __RBIT(CRC->DR);
                        break;
                case 3:
                        CRC->DR = __RBIT((*p32 & 0x00FFFFFF) ^ crc) >> 8;
                        crc = (crc >> 24) ^ __RBIT(CRC->DR);
                        break;
                }
        }
        return ~crc;
}

uint32_t temp_c = 0;
uint32_t hw_crc = 0;
void Lora_Rx_Handler(uint8_t *data, uint8_t data_length)
{			
		sec_to_boot = 600;

	  uint8_t buffer[256];
		if(data[0] == 26 && data[1] == 126 && data[2] == 76){
					offset = data[3]*256 + data[4];	
					buffer[0] = 128;
					buffer[1] = 185;
					buffer[2] = 27;
					buffer[3] = data[3];
					buffer[4] = data[4];
					Rfm_Send(buffer,5);
		}
		
		if(data[0] == 0x55 && data[1] == 0xAA && data[2] == 0x55){
				  Bootloader_erase();
					buffer[0] = 0xAA;
					buffer[1] = 0x55;
					buffer[2] = 0xAA;
					Rfm_Send(buffer,3);
		}
		
		if(data[0] == 76 && data[1] == 74 && data[2] == 98){
			    uint8_t  data_count = data[3];
					uint32_t address    = data[4]<<24 | data[5] << 16 | data[6] << 8 | data[7];		
			
					for(int i=0;i<data_count;i++)
					{
							buffer[i] = data[i+8];
					}
					hw_crc   = HW_CRC32(buffer, data_count);
					temp_c = data[8+data_count]<<24 | data[9+data_count] << 16 | data[10+data_count] << 8 | data[11+data_count];
					if(hw_crc == temp_c){
							if(last_address != address){
									if(Bootloader_write(address, buffer, data_count))last_address = address;
							}
							buffer[0] = 24;
							buffer[1] = 42;
							buffer[2] = 64;
							buffer[3] = address >> 24;
							buffer[4] = address >> 16;
							buffer[5] = address >> 8;
							buffer[6] = address & 0xFF;
							Rfm_Send(buffer,8);
					}
		}
					
		if(data[0] == 1 && data[1] == 241 && data[2] == 38){
					uint32_t crc = data[3]<<24 | data[4] << 16 | data[5] << 8 | data[6];
			    Bootloader_save_checksum(crc);
				  buffer[0] = 87;
					buffer[1] = 24;
					buffer[2] = 73;
					crc = Bootloader_get_application_crc();
					buffer[3] = crc >> 24;
					buffer[4] = crc >> 16;
					buffer[5] = crc >> 8;
					buffer[6] = crc & 0xFF;
					HAL_GPIO_WritePin(LED2_GPIO_Port, LED2_Pin, GPIO_PIN_SET);
					for (int i=0;i<10;i++){
							Rfm_Send(buffer,7);
							HAL_Delay(1000);
					}		
					if(Bootloader_validate_application()){
						MX_GPIO_DeInit();
						HAL_SPI_DeInit(&hspi2);
						HAL_CRC_DeInit(&hcrc); 
						HAL_RCC_DeInit();
						Bootloader_start();
				  }
		}
}		

int main(void)
{

  HAL_Init();
  SystemClock_Config();
  MX_GPIO_Init();
  MX_SPI2_Init();
  MX_CRC_Init();
	HAL_GPIO_WritePin(LED2_GPIO_Port, LED2_Pin, GPIO_PIN_SET);
	Lora_Init();
	unsigned long delay = 0;
  while (1)
  {
		Lora_Polling();
		if(delay++>24000){
			HAL_GPIO_WritePin(LED2_GPIO_Port, LED2_Pin, GPIO_PIN_SET);
			delay = 0;
			if(sec_to_boot-->0){
					if(rx_present == 0){
							uint8_t buffer[5];
							buffer[0] = 48;
							buffer[1] = 85;
							buffer[2] = 127;
							Rfm_Send(buffer, 5);		
					}
				HAL_GPIO_WritePin(LED2_GPIO_Port, LED2_Pin, GPIO_PIN_RESET);
			}else{
					sec_to_boot = 600;
				  if(Bootloader_validate_application()){
							MX_GPIO_DeInit();
							HAL_SPI_DeInit(&hspi2);
							HAL_CRC_DeInit(&hcrc); 
							HAL_RCC_DeInit();
							Bootloader_start();
					}
			}
		}
  }
}


void SystemClock_Config(void)
{
  RCC_OscInitTypeDef RCC_OscInitStruct;
  RCC_ClkInitTypeDef RCC_ClkInitStruct;

  RCC_OscInitStruct.OscillatorType = RCC_OSCILLATORTYPE_HSE;
  RCC_OscInitStruct.HSEState = RCC_HSE_ON;
  RCC_OscInitStruct.HSEPredivValue = RCC_HSE_PREDIV_DIV1;
  RCC_OscInitStruct.PLL.PLLState = RCC_PLL_ON;
  RCC_OscInitStruct.PLL.PLLSource = RCC_PLLSOURCE_HSE;
  RCC_OscInitStruct.PLL.PLLMUL = RCC_PLL_MUL3;
  if (HAL_RCC_OscConfig(&RCC_OscInitStruct) != HAL_OK)
  {
    Error_Handler();
  }
  RCC_ClkInitStruct.ClockType = RCC_CLOCKTYPE_HCLK|RCC_CLOCKTYPE_SYSCLK
                              |RCC_CLOCKTYPE_PCLK1|RCC_CLOCKTYPE_PCLK2;
  RCC_ClkInitStruct.SYSCLKSource = RCC_SYSCLKSOURCE_PLLCLK;
  RCC_ClkInitStruct.AHBCLKDivider = RCC_SYSCLK_DIV1;
  RCC_ClkInitStruct.APB1CLKDivider = RCC_HCLK_DIV2;
  RCC_ClkInitStruct.APB2CLKDivider = RCC_HCLK_DIV4;
  if (HAL_RCC_ClockConfig(&RCC_ClkInitStruct, FLASH_LATENCY_1) != HAL_OK)
  {
    Error_Handler();
  }
  HAL_SYSTICK_Config(HAL_RCC_GetHCLKFreq()/1000);
  HAL_SYSTICK_CLKSourceConfig(SYSTICK_CLKSOURCE_HCLK);
  HAL_NVIC_SetPriority(SysTick_IRQn, 0, 0);
}

static void MX_CRC_Init(void)
{

  hcrc.Instance = CRC;
  if (HAL_CRC_Init(&hcrc) != HAL_OK)
  {
    Error_Handler();
  }

}

/* SPI2 init function */
static void MX_SPI2_Init(void)
{
	hspi2.Instance = SPI2;
  hspi2.Init.Mode = SPI_MODE_MASTER;
  hspi2.Init.Direction = SPI_DIRECTION_2LINES;
  hspi2.Init.DataSize = SPI_DATASIZE_8BIT;
  hspi2.Init.CLKPolarity = SPI_POLARITY_LOW;
  hspi2.Init.CLKPhase = SPI_PHASE_1EDGE;
  hspi2.Init.NSS = SPI_NSS_SOFT;
  hspi2.Init.BaudRatePrescaler = SPI_BAUDRATEPRESCALER_128;
  hspi2.Init.FirstBit = SPI_FIRSTBIT_MSB;
  hspi2.Init.TIMode = SPI_TIMODE_DISABLE;
  hspi2.Init.CRCCalculation = SPI_CRCCALCULATION_DISABLE;
  hspi2.Init.CRCPolynomial = 10;
  if (HAL_SPI_Init(&hspi2) != HAL_OK)
  {
    Error_Handler();
  }
}

static void MX_GPIO_Init(void)
{

  GPIO_InitTypeDef GPIO_InitStruct;
  __HAL_RCC_GPIOD_CLK_ENABLE();
  __HAL_RCC_GPIOC_CLK_ENABLE();
  __HAL_RCC_GPIOB_CLK_ENABLE();
  __HAL_RCC_GPIOA_CLK_ENABLE();
  HAL_GPIO_WritePin(GPIOC, LED2_Pin, GPIO_PIN_RESET);
  HAL_GPIO_WritePin(GPIOB, RFM_RESET_Pin|GPIO_PIN_12, GPIO_PIN_RESET);
  GPIO_InitStruct.Pin = LED2_Pin;
  GPIO_InitStruct.Mode = GPIO_MODE_OUTPUT_PP;
  GPIO_InitStruct.Speed = GPIO_SPEED_FREQ_LOW;
  HAL_GPIO_Init(GPIOC, &GPIO_InitStruct);

  GPIO_InitStruct.Pin = GPIO_PIN_0;
  GPIO_InitStruct.Mode = GPIO_MODE_IT_RISING;
  GPIO_InitStruct.Pull = GPIO_NOPULL;
  HAL_GPIO_Init(GPIOB, &GPIO_InitStruct);

  GPIO_InitStruct.Pin = RFM_IO1_Pin|GPIO_PIN_2|RFM_IO3_Pin|RFM_IO4_Pin 
                          |RFM_IO5_Pin;
  GPIO_InitStruct.Mode = GPIO_MODE_INPUT;
  GPIO_InitStruct.Pull = GPIO_NOPULL;
  HAL_GPIO_Init(GPIOB, &GPIO_InitStruct);

  GPIO_InitStruct.Pin = RFM_RESET_Pin|GPIO_PIN_12;
  GPIO_InitStruct.Mode = GPIO_MODE_OUTPUT_PP;
  GPIO_InitStruct.Speed = GPIO_SPEED_FREQ_LOW;
  HAL_GPIO_Init(GPIOB, &GPIO_InitStruct);

  HAL_NVIC_SetPriority(EXTI0_IRQn, 0, 0);
  HAL_NVIC_EnableIRQ(EXTI0_IRQn);

}

static void MX_GPIO_DeInit(void)
{
  HAL_GPIO_DeInit(GPIOC, LED2_Pin);
  HAL_GPIO_DeInit(GPIOB, GPIO_PIN_0);
  HAL_GPIO_DeInit(GPIOB, RFM_IO1_Pin|GPIO_PIN_2|RFM_IO3_Pin|RFM_IO4_Pin |RFM_IO5_Pin);
  HAL_GPIO_DeInit(GPIOB, RFM_RESET_Pin|GPIO_PIN_12);
  HAL_NVIC_DisableIRQ(EXTI0_IRQn);

}

void Error_Handler(void)
{
  while(1) 
  {
  }
}

#ifdef USE_FULL_ASSERT


void assert_failed(uint8_t* file, uint32_t line)
{

}

#endif

