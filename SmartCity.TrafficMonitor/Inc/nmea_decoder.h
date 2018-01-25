#ifndef __NMEA_DECODER_H
#define __NMEA_DECODER_H
typedef struct
{
		unsigned int  hh;
		unsigned int  mm; 
		unsigned int  ss;
		unsigned int  sats;
		unsigned char latitude_string[12];
		unsigned char longitude_string[12];
		unsigned char isEast;
		unsigned char isNorth;        
} GPS_t;
void nmea_second_process(void);
void UART4_IRQHandler(void);
#endif
