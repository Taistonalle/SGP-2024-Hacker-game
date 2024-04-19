


<!-- script that can receive JSON data via a POST request, decode the JSON into an associative array, and then insert the data into a database. -->

<?php
$host = 'localhost';


$db

   = 'database_name';
$user = 'database_user';
$pass = 'database_password';
$charset = 'utf8mb4';

$dsn = "mysql:host=$host;dbname=$db;charset=$charset";
$opt = [
    PDO::ATTR_ERRMODE            => PDO::ERRMODE_EXCEPTION,
    PDO::ATTR_DEFAULT_FETCH_MODE => PDO::FETCH_ASSOC,
    PDO::ATTR_EMULATE_PREPARES   => false,
];
$pdo = new PDO($dsn, $user, $pass, $opt);

// Get the JSON data from the POST request
$json = file_get_contents('php://input');

// Decode the JSON into an associative array
$data = json_decode($json, true);

// Insert the data into the database
$sql = "INSERT INTO PlayerData (email, userName, task_EE_data, correctAttemptAmount_EE, task_EA_data, correctAttemptAmount_EA, task_EI_data, correctAttemptAmount_EI, task_EP_data, correctAttemptAmount_EP, task_HI_data, correctAttemptAmount_HI, task_HE_data, correctAttemptAmount_HE, task_HA_data, correctAttemptAmount_HA, task_HP_data, correctAttemptAmount_HP) VALUES (:email, :userName, :task_EE_data, :correctAttemptAmount_EE, :task_EA_data, :correctAttemptAmount_EA, :task_EI_data, :correctAttemptAmount_EI, :task_EP_data, :correctAttemptAmount_EP, :task_HI_data, :correctAttemptAmount_HI, :task_HE_data, :correctAttemptAmount_HE, :task_HA_data, :correctAttemptAmount_HA, :task_HP_data, :correctAttemptAmount_HP)";
$stmt = $pdo->prepare($sql);
$stmt->execute($data);

//This script assumes that you have a table named `PlayerData` in your database with the columns `email`, `userName`, `task_EE_data`, `correctAttemptAmount_EE`, `task_EA_data`
//,`correctAttemptAmount_EA`, `task_EI_data`, `correctAttemptAmount_EI`, `task_EP_data`, `correctAttemptAmount_EP`, `task_HI_data`, `correctAttemptAmount_HI`, `task_HE_data`, `correctAttemptAmount_HE`, `task_HA_data`, `correctAttemptAmount_HA`, `task_HP_data`, `correctAttemptAmount_HP`. 

//Please replace `'localhost'`, `'database_name'`, `'database_user'`, and `'database_password'` with your actual database host, name, user, and password.