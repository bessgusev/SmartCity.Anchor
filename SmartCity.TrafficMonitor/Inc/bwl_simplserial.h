/*
 * Bwl SimplSerial Lib
 *
 * Author: Igor Koshelev 
 * Licensed: open-source Apache license
 *
 * Version: 01.05.2016 V1.5.0
 */ 
#ifndef BWL_GAPUART_H_
#define BWL_GAPUART_H_
#include <stdint.h>

typedef unsigned char byte;

static byte sserial_devguid[16];
static byte sserial_devname[32];
static byte sserial_bootname[16];
static byte sserial_bootloader_present;
static byte sserial_portindex;
static uint16_t sserial_address;

#define SSERIAL_VERSION "V1.5.0"
#define CATUART_MAX_PACKET_LENGTH 256

typedef struct
{
	uint16_t		 address_to;
	unsigned	char command;
	unsigned	char data[CATUART_MAX_PACKET_LENGTH];
	unsigned	int datalength;	
} sserial_request_t;



typedef struct
{
	unsigned	char result;
	unsigned	char data[CATUART_MAX_PACKET_LENGTH];
	unsigned	int datalength;
} sserial_response_t;

extern sserial_response_t sserial_response;
extern sserial_request_t sserial_request;

void sserial_process_request(unsigned char port);
void sserial_poll_uart(unsigned char portindex);
void sserial_send_response(unsigned char port);
void sserial_append_devname(byte startIndex, byte length, char* newname);
static char sserial_send_request_wait_response(unsigned char portindex, int wait_ms );

static unsigned char int_to_byte(int val);
static unsigned char int_to_low_byte(int val);
static unsigned char int_to_high_byte(int val);

#endif /* BWL_GAPUART_H_ */


