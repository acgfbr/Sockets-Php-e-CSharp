<?php
error_reporting(E_ALL);

echo "<h2>Conexão Socket com C# e PHP</h2>\n";

$Buffer = 1024;
$Porta = 1818;
$Ip = '127.0.0.1';

$socket = socket_create(AF_INET, SOCK_STREAM, SOL_TCP); // cria o socket

//debugzim caso de pau.
/*
if ($socket === false) {
    echo "\nsocket_create() failed: reason: " . socket_strerror(socket_last_error()) . "\n";
} else {
    echo "\nOK.\n";
}*/

$result = socket_connect($socket, $Ip, $Porta); // conecta

// debugzim caso de pau.
/*
if ($result === false) {
    echo "socket_connect() failed.\nReason: ($result) " . socket_strerror(socket_last_error($socket)) . "\n";
} else {
    echo "OK.\n";
}
*/

$Enviando = "Oi, escrevi no console pelo php ;) !";
$Recebendo = '';

// Envia..
socket_write($socket, $Enviando, strlen($Enviando));

// Recebe resposta..
while ($Recebendo = socket_read($socket, $Buffer)) {
    echo $Recebendo;
}

// Fecha a conexao.
socket_close($socket);
?>