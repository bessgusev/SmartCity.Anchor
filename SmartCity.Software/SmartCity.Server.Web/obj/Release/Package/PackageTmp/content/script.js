var _lastDeviceId = 0;
var _lastAccessPointId = 0;
var _lastBootloaderTaskId = 0;
var _lastLoggerTaskId = 0;

var _trafficUrl = "/api/TrafficMonitor.ashx";
var _FrimwareUpdateUrl = "/api/FirmwareUpdate.ashx";
var _AccesPointUpdateUrl = "/api/AccessPointsStates.ashx";
var _SystemLogUpdateUrl = "/api/SystemLogs.ashx";

$(document).ready(function () {
    $("a").click(function (event) {
        $(this).tab('show');
    });
});


$(function () {
    traffic_monitor_update();
    $("#button_clear_traffic_monitor_base").click(function () {
        $.get(_trafficUrl + "?act=clear_base", function (data, status) {
            var table_header = $("#traffic_monitor_row").clone();
            $("#traffic_monitor_table").empty();
            table_header.prependTo("#traffic_monitor_table");
        });
    });

    $("#system_log_clear").click(function () {
        $.get(_SystemLogUpdateUrl + "?act=clear_base", function (data, status) {
            $("#logs_table").empty();
        });
    });

    setInterval(traffic_monitor_update, 3000);
    setInterval(refresh_device_update_page, 3000);
    setInterval(access_points_page_update, 5000);
    setInterval(system_logger_update_page, 2000);
});

function access_point_refresh() {
    $.get(_FrimwareUpdateUrl + "?last_id=" + _lastid, function (data, status) {
        var json = JSON.parse(data);
        var data_count = json.result.length;
        for (var i = 0; i < data_count; i++) {
            if (_lastid < parseInt(json.result[i].ID)) _lastid = parseInt(json.result[i].ID);
            $("#firmware_table_table").prepend("<tr onclick='traffic_monitor_show_map(this)'><td>" + json.result[i].ID + "</td><td>" + json.result[i].device_id + "</td><td>" + json.result[i].base_station + "</td><td>" + json.result[i].hex_file + "</td><td>" + json.result[i].state + "</td><td>" + json.result[i].time + "</td></tr>");
        }
    });
}

function traffic_monitor_show_map(obj) {
    alert(obj);
    $('#message_modal').modal('show');
    $("#message_modal").css("z-index", "1500");
}

function traffic_monitor_update() {
    $.get(_trafficUrl + "?last_id=" + _lastDeviceId, function (data, status) {
        var json = JSON.parse(data);
        var data_count = json.result.length;
        for (var i = 0; i < data_count; i++) {
            if (_lastDeviceId < parseInt(json.result[i].ID)) _lastDeviceId = parseInt(json.result[i].ID);
            $("#traffic_monitor_table").prepend("<tr onclick='traffic_monitor_show_map(this)'><td>" + json.result[i].ID + "</td><td>" + json.result[i].device_id + "</td><td>" + json.result[i].mean_speed + "</td><td>" + json.result[i].latitude + "</td><td>" + json.result[i].longitude + "</td><td>" + json.result[i].device_time + "</td><td>" + json.result[i].input_voltage + "</td><td>" + json.result[i].battery_voltage + "</td><td>" + json.result[i].rssi + "</td><td>" + json.result[i].rssi_packet + "</td></tr>");
        }
    });
}

function refresh_device_update_page() {
    $.get(_FrimwareUpdateUrl + "?last_id=" + _lastBootloaderTaskId, function (data, status) {
        var json = JSON.parse(data);
        var data_count = json.result.length;
        $("#firmware_update_table tr").remove();
        for (var i = 0; i < data_count; i++) {
            if (_lastBootloaderTaskId < parseInt(json.result[i].ID)) _lastBootloaderTaskId = parseInt(json.result[i].ID);
            var color = "#D6DCFF";
            var ap_time = Date.parse(json.result[i].last_active);
            if (json.result[i].state.toLowerCase().indexOf("error") >= 0) {
                color = "#ffcccd";
            }
            if (json.result[i].state.toLowerCase().indexOf("queue") >= 0) {
                color = "#ffffbb";
            }
            if (json.result[i].state.toLowerCase().indexOf("complete") >= 0) {
                color = "#e8fff7";
            }
            $("#firmware_update_table").prepend("<tr style='background-color:" + color + ";'><td>" + json.result[i].ID + "</td><td>" + json.result[i].device_id + "</td><td>" + json.result[i].base_station + "</td><td>" + json.result[i].hex_file + "</td><td>" + json.result[i].state + "</td><td>" + json.result[i].elapsed_time + " сек</td><td>" + json.result[i].time + "</td></tr>");
        }
    });
}

// _AccesPointUpdateUrl
function access_points_page_update() {
    $.get(_AccesPointUpdateUrl, function (data, status) {
        var json = JSON.parse(data);
        var data_count = json.result.length;
        $("#access_points_table tr").remove();
        var server_time = Date.parse(json.time);
        for (var i = 0; i < data_count; i++) {

            var color = "#e8fff7";
            var ap_time = Date.parse(json.result[i].last_active);
            if ((server_time - ap_time) > 200000) {
                color = "#ffcccd";
            }
            $("#access_points_table").prepend("<tr style='background-color:" + color + ";'><td>" + json.result[i].ID + "</td><td>" + json.result[i].device_id + "</td><td>" + json.result[i].last_active + "</td><td>" + json.result[i].current_version + "</td></tr>");
        }
    });
}

function system_logger_update_page() {
    $.get(_SystemLogUpdateUrl + "?last_id=" + _lastLoggerTaskId, function (data, status) {
        var json = JSON.parse(data);
        var data_count = json.result.length;
        for (var i = 0; i < data_count; i++) {
            if (_lastLoggerTaskId < parseInt(json.result[i].ID)) _lastLoggerTaskId = parseInt(json.result[i].ID);
            var color = "#ffffbb";
            var ap_time = Date.parse(json.result[i].last_active);
            if (json.result[i].message_type.toLowerCase().indexOf("error") >= 0) {
                color = "#ffcccd";
            }
            if (json.result[i].message_type.toLowerCase().indexOf("info") >= 0) {
                color = "#e8fff7";
            }
            $("#logs_table").prepend("<tr style='background-color:" + color + ";'><td>" + json.result[i].ID + "</td><td>#" + json.result[i].base_id + "</td><td>#" + json.result[i].device_id + "</td><td>" + json.result[i].message + "</td><td>" + json.result[i].message_type + "</td><td>" + json.result[i].time + "</td></tr>");
        }
    });
}