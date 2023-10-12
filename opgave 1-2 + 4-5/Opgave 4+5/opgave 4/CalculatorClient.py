from socket import *

# Serverkonfiguration
serverName = "127.0.0.1"
serverPort = 12000  
client = socket(AF_INET, SOCK_STREAM)  # Opret en TCP-socket
client.connect((serverName, serverPort))  # Opret forbindelse til serveren

# Velkomstbesked
print("Welcome to my calculator")
print(" ")

while True:
    oprnd1 = input("Enter the first operand: ")
    operation = input("Enter operationen (+, -, *, /): ")
    oprnd2 = input("Enter the second operand: ")

    # Kombiner brugerens input til en samlet string
    inp = f"{oprnd1} {operation} {oprnd2}"

    
    client.send(inp.encode()) 

    
    answer = client.recv(1024) 
    print("Answer is " + answer.decode()) 

    # Bed brugeren om at fortsætte eller afslutte
    exit_choice = input("Type 'Exit' to terminate or press Enter to continue: ")
    if exit_choice == "Exit":
        break

# Luk klientsocketen når færdig
client.close()
