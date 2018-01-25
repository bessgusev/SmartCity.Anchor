#include "stm32f1xx_hal.h"
#include "bootloader.h"

#define APPLICATION_ADDRESS         (uint32_t)0x08004000  
#define APPLICATION_LENGTH          (uint32_t)0x00050000 
//CHANGE "VECT_TAB_OFFSET" IN APP
//Jump to Bootloader: NVIC_SystemReset(); 
typedef void (*pFunction)(void);

uint32_t saved_crc = 0;
uint32_t flash_crc = 0;
uint8_t Bootloader_validate_application(void)
{
	  saved_crc = *(__IO uint32_t*)(APPLICATION_LENGTH+APPLICATION_ADDRESS-4);
		flash_crc = Bootloader_get_application_crc();
		if(saved_crc == flash_crc)return 1;
		return 0;
}

void Bootloader_start(void)
{		
		HAL_FLASH_Lock();
		uint32_t  JumpAddress = *(__IO uint32_t*)(APPLICATION_ADDRESS + 4);
		pFunction Jump = (pFunction)JumpAddress;    
		HAL_RCC_DeInit();
		HAL_DeInit();   
		SysTick->CTRL = 0;
		SysTick->LOAD = 0;
		SysTick->VAL  = 0;    
		SCB->VTOR = APPLICATION_ADDRESS;
		__set_MSP(*(__IO uint32_t*)APPLICATION_ADDRESS);
		Jump();
}


uint32_t crc32_calculate(const uint32_t* p32, size_t count) {
        uint32_t crc;
        CRC->CR |= CRC_CR_RESET;
        while (count--) {
                CRC->DR = __RBIT(*p32++);
        }
        crc = __RBIT(CRC->DR);      
        return ~crc;
}

uint32_t temp_crc = 0;
uint32_t Bootloader_get_application_crc(void) 
{
   temp_crc = crc32_calculate((uint32_t*)APPLICATION_ADDRESS, ((APPLICATION_LENGTH)/4)-1);
   return temp_crc;
}

uint8_t Bootloader_write(uint32_t address, uint8_t *data, uint8_t datalength)
{		   
			for(int i = 0; i<datalength;i+=2){
					 HAL_StatusTypeDef	flash_ok = HAL_ERROR;
					 while(flash_ok != HAL_OK){
						 if((FLASH->CR & 0x80) > 0){
								flash_ok = HAL_FLASH_Unlock();
						 }else{
								flash_ok = HAL_OK;
						 }
				   }
					 if(HAL_FLASH_Program(TYPEPROGRAM_HALFWORD, address+i, data[i]+data[1+i]*256) != HAL_OK){ return 0; }
				  __HAL_FLASH_CLEAR_FLAG(FLASH_FLAG_EOP | FLASH_SR_PGERR | FLASH_FLAG_WRPERR | FLASH_FLAG_OPTVERR);
			}
			return 1;
}

void Bootloader_save_checksum(uint32_t crc)
{
		uint8_t buffer[4] = {crc>>24, crc>>16, crc>>8, crc&0xFF};
		while(Bootloader_write(APPLICATION_LENGTH+APPLICATION_ADDRESS-4, buffer,4)==0);
}

void Bootloader_erase(void)
{
		 HAL_StatusTypeDef	flash_ok = HAL_ERROR;
		 while(flash_ok != HAL_OK){
			 if((FLASH->CR & 0x80) > 0){
				 flash_ok = HAL_FLASH_Unlock();
			 }else{
				 flash_ok = HAL_OK;
			 }
	   }
	   flash_ok = HAL_ERROR;
	 	 FLASH_EraseInitTypeDef EraseInitStruct;
		 uint32_t PageError = 0;
     EraseInitStruct.TypeErase = TYPEERASE_PAGES;
     EraseInitStruct.PageAddress = APPLICATION_ADDRESS;
     EraseInitStruct.NbPages =  APPLICATION_LENGTH/FLASH_PAGE_SIZE;
	   HAL_FLASHEx_Erase(&EraseInitStruct, &PageError);
		 CLEAR_BIT (FLASH->CR, (FLASH_CR_PER));
		 __HAL_FLASH_CLEAR_FLAG(FLASH_FLAG_EOP | FLASH_SR_PGERR | FLASH_FLAG_WRPERR | FLASH_FLAG_OPTVERR);
}
