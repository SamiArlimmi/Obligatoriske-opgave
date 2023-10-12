import json
from socket import *

# Serverkonfiguration
serverName = "127.0.0.1"  
serverPort = 12000  
client = socket(AF_INET, SOCK_STREAM)  
client.connect((serverName, serverPort))  

# Velkomstbesked
print("Welcome to my calculator\n")

while True:
    oprnd1 = input("Enter the first operand: ")
    operation = input("Enter operationen (+, -, *, /): ")
    oprnd2 = input("Enter the second operand: ")

    # Opret en JSON-anmodningsobjekt
    request = {
        "method": operation,
        "Tal1": int(oprnd1),
        "Tal2": int(oprnd2)
    }

    client.send(json.dumps(request).encode()) # Kode og send JSON-anmodningen til serveren
    respons = client.recv(1024)  #

    try:
        respons_data = json.loads(respons.decode())  # Decode den modtagne JSON-respons til en Python-dictionary
        if "error" in respons_data:
            print("Error:", respons_data["error"])
        else:
            print("Answer is", respons_data["result"])
    except json.JSONDecodeError:
        print("Invalid JSON response from server")

    exit_choice = input("Type 'Exit' to terminate or press Enter to continue: ")
    if exit_choice.lower() == "exit":
        break

# Luk klientsocketen når færdig
client.close()
