CREATE TABLE traffic_monitor (
	ID int PRIMARY KEY AUTO_INCREMENT,
    device_id varchar(20) NOT NULL,
    base_station varchar(20) NOT NULL,
    device_time varchar(10),
    latitude varchar(10),
    longitude varchar(10),
    mean_speed INT,
    min_speed INT,
    max_speed INT,
    rssi_packet INT,
    rssi INT,
    time TIMESTAMP,
    battery_voltage DOUBLE(10,2)
);

CREATE TABLE `firmware_tasks` (
	ID int PRIMARY KEY AUTO_INCREMENT,
    device_id varchar(20) NOT NULL,
    base_station varchar(20) NOT NULL,
    hex_file varchar(10),
    state varchar(10),
    time TIMESTAMP
);

CREATE TABLE `system_logs` (
	ID int PRIMARY KEY AUTO_INCREMENT,
    base_id varchar(20),
    device_id varchar(20),
    message text,
    message_type varchar(10),
    time TIMESTAMP
);