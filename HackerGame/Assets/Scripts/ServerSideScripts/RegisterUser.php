<?php
$servername = 'localhost';
$DbUsername = 'root';
$DbPassword = 'root';
$dbname = 'mysqlyoutube';

// Create connection
$conn = new mysqli($servername, $DbUsername, $DbPassword, $dbname);

// Check connection
if ($conn->connect_error) {
    die("1"); // Error code 1 = connection to database failed
}

// Query to check if user email is already in the database
$UserEmail = $_POST["email"];
$UserPassword = MD5($_POST["password"]); // Using MD5 for password hashing (not recommended for security)
$Username = $_POST["username"];

$RegisterUserQuery = "INSERT INTO users VALUES('" . $UserEmail . "', '" . $Username . "', '" . $UserPassword . "');";

try {
    $RegisterUserResult = $conn->query($RegisterUserQuery);

    if ($RegisterUserResult === false) {
        die("22"); // Error code 22 = query failed
        //-- Primary Key May Have Been Violated
    }
} catch (Exception $e) {
    die("30"); // Error code 30 = query failed
}

// Success
echo "success";
$conn->close();
?>
