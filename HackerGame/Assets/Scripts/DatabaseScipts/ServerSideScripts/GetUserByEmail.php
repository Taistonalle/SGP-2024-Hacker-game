<?php
// Database connection parameters
$servername = 'localhost';  // Server name
$DbUsername = 'root';       // Database username
$DbPassword = 'root';       // Database password
$dbname = 'mysqlyoutube';   // Database name

// Create connection
$conn = new mysqli($servername, $DbUsername, $DbPassword, $dbname);

// Check connection
if ($conn->connect_error) {
    die("1"); // Error code 1 = connection to database failed
}

// Retrieve user email from the POST request
$UserEmail = $_POST["email"];

// Query to retrieve user information by email
$getUserQuery = "SELECT Username, Email FROM users WHERE Email = ?";
$stmt = $conn->prepare($getUserQuery);
$stmt->bind_param("s", $UserEmail);
$stmt->execute();
$getUserResult = $stmt->get_result();

if ($getUserResult === false) {
    die("22"); // Error code 22 = query failed
}

// Check if user exists with the provided email
if($getUserResult->num_rows > 0)
{
    // Fetch user information
    $row = $getUserResult->fetch_assoc();
    
    // Prepare user information as JSON
    $userInfo = array(
        "username" => $row["Username"],
        "email" => $row["Email"]
    );

    // Encode user information as JSON and echo it
    echo json_encode($userInfo);
}
else
{
    die("55"); //55 = User not found
}

// Close database connection
$conn->close();
?>
