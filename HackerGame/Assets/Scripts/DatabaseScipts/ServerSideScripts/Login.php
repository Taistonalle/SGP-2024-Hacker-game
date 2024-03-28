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

$UserEmail = $_POST["email"];
$UserPassword = MD5($_POST["password"]);
$Username = $_POST["username"];

// Query to check if user email is already in the database
$LoginQuery = "SELECT Username FROM users WHERE Email = ? AND Password = ?";
$stmt = $conn->prepare($LoginQuery);
$stmt->bind_param("ss", $UserEmail, $UserPassword);
$stmt->execute();
$LoginResult = $stmt->get_result();

if ($LoginResult === false) {
    die("22"); // Error code 22 = query failed
}

if($LoginResult->num_rows > 0)
{
    //echo org name
    $row = $LoginResult->fetch_assoc();
    echo($row["Username"]);
}
else
{
    die("55"); //55 = User has not registered
}

// Success
echo "success";
$conn->close();
?>
