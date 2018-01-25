#include "stm32f1xx_hal.h"
#include "nmea_decoder.h"
#include <string.h>
#include <math.h>
#include <stdint.h>
char          line[255];
unsigned char cursor_pointer = 0;
extern UART_HandleTypeDef huart4;

GPS_t GPS;
int ps;
char gps_state_line[20] = "Sat: 0 | 00:00:00";
char gps_coordinates[20]= "                 ";
extern uint8_t lora_data[19];
extern void planned_tasks();
void nmea_second_process(void)
{
	if(HAL_GPIO_ReadPin(GPS_EN_GPIO_Port, GPS_EN_Pin)==GPIO_PIN_SET){
			gps_state_line[4] = (char)(GPS.sats/10 +0x30);
			gps_state_line[5] = (char)(GPS.sats%10 +0x30);
	} else {
			GPS.ss += 1;
			if(GPS.ss>59){GPS.ss = 0; GPS.mm+=1;}
			if(GPS.mm>59){GPS.mm = 0; GPS.hh+=1;}
			if(GPS.hh>23){GPS.hh = 0;}
			gps_state_line[4] = '-';
		  gps_state_line[5] = '-';
	}	
	gps_state_line[9]  = (char)(GPS.hh/10 +0x30);
	gps_state_line[10] = (char)(GPS.hh%10 +0x30);
	gps_state_line[12] = (char)(GPS.mm/10 +0x30);
	gps_state_line[13] = (char)(GPS.mm%10 +0x30);
	gps_state_line[15] = (char)(GPS.ss/10 +0x30);
	gps_state_line[16] = (char)(GPS.ss%10 +0x30);
	lora_data[4] = GPS.hh;
	lora_data[5] = GPS.mm;
	lora_data[6] = GPS.ss;
	if(GPS.latitude_string[0]>0){
			for(int i=0;i<9;i++)
			{
					gps_coordinates[i] = GPS.longitude_string[i];
					gps_coordinates[i+10] = GPS.latitude_string[i];
			}
			lora_data[8] = (GPS.latitude_string[0]-0x30)*10 + GPS.latitude_string[1]-0x30;
			lora_data[9] = (GPS.latitude_string[2]-0x30)*10 + GPS.latitude_string[3]-0x30;;
			lora_data[10] = (GPS.latitude_string[5]-0x30)*10 + GPS.latitude_string[6]-0x30;;
			lora_data[11] = (GPS.latitude_string[7]-0x30)*10 + GPS.latitude_string[8]-0x30;;	
			lora_data[12] = (GPS.longitude_string[1]-0x30)*10 + GPS.longitude_string[2]-0x30;
			lora_data[13] = (GPS.longitude_string[3]-0x30)*10 + GPS.longitude_string[4]-0x30;;
			lora_data[14] = (GPS.longitude_string[6]-0x30)*10 + GPS.longitude_string[7]-0x30;;
			lora_data[15] = (GPS.longitude_string[8]-0x30)*10 + GPS.longitude_string[9]-0x30;;
	}
}

void nmea_append(char ch)
{
	if(cursor_pointer>255)cursor_pointer = 0;
	if(ch == '\n'){
			if(strstr (line,"$GPGGA")){
					for(int i=0;i<12;i++){
							GPS.latitude_string[i] = 0x30;
							GPS.longitude_string[i] = 0x30;
					}
					for(int i=0;i<cursor_pointer;i++){
							if(line[i]==71 && line[i+1]==80 && line[i+2]==71 && line[i+3]==71 && line[i+4]==65){
									unsigned char p = 0;
									for(int j=i; j<cursor_pointer;j++){
											if(line[j]==','){
													switch(p++){
														case 0:
																GPS.hh = (line[j+1]-0x30)*10+(line[j+2]-0x30);
																GPS.mm = (line[j+3]-0x30)*10+(line[j+4]-0x30);
																GPS.ss = (line[j+5]-0x30)*10+(line[j+6]-0x30);
														break;
														
														case 1:
																for(int x=1;x<15;x++){
																		if(line[j+x]==',') x = 20; else
																		GPS.latitude_string[x-1] = line[j+x];
																}
														break;
														case 2:
															GPS.isNorth = (line[j+1]==78)?1:0;
														break;
														case 3:
																for(int x=1;x<15;x++){
																		if(line[j+x]==',') x = 20; else
																		GPS.longitude_string[x-1] = line[j+x];
																}
														break;
														case 4:
															GPS.isEast = (line[j+1]==69)?1:0;
														break;
														case 6:
																GPS.sats =(line[j+1]-0x30)*10+(line[j+2]-0x30);
														break;
														
														default:
															
														break;
													}
											}
									}
									break;
							}
					}
			}
			cursor_pointer = 0;
	}else{
			line[cursor_pointer++] = ch;
	}
}

void UART4_IRQHandler(void)
{
	nmea_append(UART4->DR);
  HAL_UART_IRQHandler(&huart4);
}
