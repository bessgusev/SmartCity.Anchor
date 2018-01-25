<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Main.aspx.vb" Inherits="SmartCity.Web.Main" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Server state</title>
    <meta charset="utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no"/>
    <meta name="description" content=""/>
    <meta name="author" content=""/>
    <meta charset="utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no"/>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    <script src="content/bootstrap.js"></script>
    <script src="content/bootstrap.min.js"></script>
    <script src="content/npm.js"></script>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-beta.2/css/bootstrap.min.css" integrity="sha384-PsH8R72JQ3SOdhVi3uxftmaW6Vc51MKb0q5P2rRUpPvrszuE4W1povHYgTpBfshb" crossorigin="anonymous"/>
</head>
<body>
      <script src="/content/script.js"></script>  
      <header style="margin-bottom:80px;">
      <nav class="navbar navbar-expand-md navbar-dark fixed-top bg-dark">
        <a class="navbar-brand" href="#">Панель управления</a>
        <button class="navbar-toggler d-lg-none" type="button" data-toggle="collapse" data-target="#navbarsExampleDefault" aria-controls="navbarsExampleDefault" aria-expanded="false" aria-label="Toggle navigation">
          <span class="navbar-toggler-icon"></span>
        </button>

        <div class="collapse navbar-collapse" id="header_navbar">
          <ul class="navbar-nav mr-auto">
            <li class="nav-item active">
              <a class="nav-link" href="#smart_devices">Конечные устройства<span class="sr-only">(current)</span></a>
            </li>
            <li class="nav-item">
              <a class="nav-link" href="#access_points">Базовые станции</a>
            </li>
            <li class="nav-item">
              <a class="nav-link" href="#firmware_update_tasks">Обновления устройств</a>
            </li>
            <li class="nav-item">
              <a class="nav-link" href="#logs">Логирование</a>
            </li>
          </ul>
        </div>
      </nav>
    </header>

    <div class="container-fluid">
      <div class="row">
          <nav class="col-sm-3 col-md-2 d-none d-sm-block bg-light sidebar">

          <ul class="nav nav-pills flex-column">
            <li class="nav-item">
              <a class="nav-link active" href="#">Транспортные потоки<span class="sr-only">(current)</span></a>
            </li>

            <li class="nav-item">
              <a class="nav-link" href="#">Светофоры</a>
            </li>

            <li class="nav-item">
              <a class="nav-link" href="#">Спецтранспорт</a>
            </li>

            <li class="nav-item">
              <a class="nav-link" href="#">ЖКХ</a>
            </li>

            <li class="nav-item">
              <a class="nav-link" href="#">Освещение</a>
            </li>

            <li class="nav-item">
              <a class="nav-link" href="#">Датчик среды</a>
            </li>
          </ul>
        </nav> 


        <div class="tab-content   col-sm-9 ml-sm-auto col-md-10 pt-3">
            <div class="tab-pane active" id="smart_devices" role="tabpanel">
                  <main role="main">
                      
                    <h1>Датчики транспортного потока</h1>
                    <div class="form-row">                
                        <div class="col-sm-2">
                            <input type="text" class="form-control" id="id_filter" placeholder="ID"/>
                        </div>
                        <button id="button_clear_traffic_monitor_base" type="button" class="btn btn-danger" style>Очистить базу данных</button>
                    </div>          
                      <div class="table-responsive">
                        <table class="table table-striped" id="traffic_monitor_table">
                          <thead>
                            <tr id="traffic_monitor_row">
                              <th>#ID</th>
                              <th>Адрес</th>
                              <th>Скорость</th>
                              <th>Широта</th>
                              <th>Долгота</th>
                              <th>Время</th>
                              <th>Питание</th>
                              <th>Аккумулятор</th>
                              <th>RX RSSI</th>
                              <th>RXP RSSI</th>
                            </tr>
                          </thead>
                          <tbody>              
                          </tbody>
                        </table>
                      </div>
                   </main>
            </div>
            <div class="tab-pane" id="access_points" role="tabpanel">
                <h1>Точки доступа LoRa</h1>       
                    <div class="table-responsive">
                    <table class="table table-striped">
                        <thead>
                        <tr id="access_points_row">
                            <th>ID</th>
                            <th>Номер точки</th>
                            <th>Последняя активность</th>
                            <th>Версия ПО</th>                    
                        </tr>
                        </thead>
                        <tbody id="access_points_table"> 
                                         
                        </tbody>
                    </table>
                    </div>
            </div>

            <div class="tab-pane" id="firmware_update_tasks" role="tabpanel">
                   <h1>Текущие задачи планировщика</h1>       
                    <div class="table-responsive">
                    <table class="table table-striped">
                        <thead>
                        <tr>
                            <th>ID</th>
                            <th>Номер устройства</th>
                            <th>Базовая станция</th>
                            <th>HEX файл</th>      
                            <th>Статус</th> 
                            <th>Время выполнения</th>       
                            <th>Создано</th>              
                        </tr>
                        </thead>
                        <tbody id="firmware_update_table"> 
                                         
                        </tbody>
                    </table>
                    </div>
            </div>
            <div class="tab-pane" id="logs" role="tabpanel">
                <h1>Системный лог</h1>  
                <div class="custom-control custom-radio">
                    <input type="radio" id="system_log_load_updated" name="customRadio" class="custom-control-input" checked>
                    <label class="custom-control-label" for="system_log_load_updated">Только обновления</label>
                </div>
                <div class="custom-control custom-radio">
                    <input type="radio" id="system_log_load_all" name="customRadio" class="custom-control-input"/>
                    <label class="custom-control-label" for="system_log_load_all">Все записи</label>
                </div>   
                <button id="system_log_clear" type="button" class="btn btn-danger">Очистить</button>  
                    <div class="table-responsive">
                    <table class="table table-striped">
                        <thead>
                        <tr id="system_log_row">
                            <th>ID</th>
                            <th>Базовая станция</th>
                            <th>Конечное устройство</th>
                            <th>Информация</th>      
                            <th>Тип</th>     
                            <th>Время</th>              
                        </tr>
                        </thead>
                        <tbody id="logs_table"> 
                                         
                        </tbody>
                    </table>
                    </div>
            </div>
        </div>    

      </div>
    </div>      
</body>
</html>